﻿using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SubSonic.Tests.DAL
{
    using Extensions.Test;
    using Infrastructure;
    using Infrastructure.Schema;
    using Linq;
    using SubSonic.Data.Caching;
    using Models = Extensions.Test.Models;

    public partial class DbContextTests
    {
        private static IEnumerable<IDbTestCase> UpdateTestCases()
        {
            yield return new DbTestCase<Models.Person>(true, @"UPDATE [T1] SET
	[T1].[FirstName] = [T2].[FirstName],
	[T1].[MiddleInitial] = [T2].[MiddleInitial],
	[T1].[FamilyName] = [T2].[FamilyName]
OUTPUT INSERTED.* INTO @output
FROM [dbo].[Person] AS [T1]
	INNER JOIN @update AS [T2]
		ON ([T2].[ID] = [T1].[ID])", person => person.ID > 2);
            yield return new DbTestCase<Models.Person>(false, @"UPDATE [T1] SET
	[T1].[FirstName] = @FirstName,
	[T1].[MiddleInitial] = @MiddleInitial,
	[T1].[FamilyName] = @FamilyName
OUTPUT INSERTED.* INTO @output
FROM [dbo].[Person] AS [T1]
WHERE ([T1].[ID] = @id_1)", person => person.ID == 3);
            yield return new DbTestCase<Models.Renter>(true, @"UPDATE [T1] SET
	[T1].[PersonID] = [T2].[PersonID],
	[T1].[UnitID] = [T2].[UnitID],
	[T1].[Rent] = [T2].[Rent],
	[T1].[StartDate] = [T2].[StartDate],
	[T1].[EndDate] = [T2].[EndDate]
OUTPUT INSERTED.* INTO @output
FROM [dbo].[Renter] AS [T1]
	INNER JOIN @update AS [T2]
		ON (([T2].[PersonID] = [T1].[PersonID]) AND ([T2].[UnitID] = [T1].[UnitID]))", renter => renter.PersonID == 2 && renter.UnitID == 1);
            yield return new DbTestCase<Models.Renter>(false, @"UPDATE [T1] SET
	[T1].[PersonID] = @PersonID,
	[T1].[UnitID] = @UnitID,
	[T1].[Rent] = @Rent,
	[T1].[StartDate] = @StartDate,
	[T1].[EndDate] = @EndDate
OUTPUT INSERTED.* INTO @output
FROM [dbo].[Renter] AS [T1]
WHERE (([T1].[PersonID] = @personid_1) AND ([T1].[UnitID] = @unitid_2))", renter => renter.PersonID == 1 && renter.UnitID == 3);
        }

        [Test]
        [TestCaseSource(nameof(UpdateTestCases))]
        public void CanUpdateEntities(IDbTestCase dbTest)
        {
            IEnumerable<IEntityProxy>
                expected = dbTest.FetchAll().Select(x =>
                    x as IEntityProxy);

            expected.Count().Should().Be(dbTest.Count());

            DbContext.Database.Instance.AddCommandBehavior(dbTest.Expectation, cmd =>
            {
                if (dbTest.UseDefinedTableType)
                {
                    return UpdateCmdBehaviorForUDTT(cmd, expected);
                }
                else
                {
                    return UpdateCmdBehaviorForInArray(cmd, expected);
                }
            });

            foreach(IEntityProxy proxy in expected)
            {
                if (proxy is Models.Person person)
                {   // we should really set this part up to set random data
                    person.FirstName = "Bob";
                    person.FamilyName = "Walters";
                    person.MiddleInitial = "S";
                }
                else if (proxy is Models.Renter renter)
                {
                    renter.EndDate.Should().NotBe(DateTime.Today);

                    renter.EndDate = DateTime.Today;
                }

                proxy.IsDirty.Should().BeTrue();
            }

            DbContext.ChangeTracking
                .SelectMany(x => x.Value)
                .Count(x => x.IsDirty)
                .Should()
                .Be(expected.Count());

            if (expected.Count() > 0)
            {
                if (dbTest.UseDefinedTableType)
                {
                    using (dbTest.EntityModel.AlteredState<IDbEntityModel, DbEntityModel>(new
                    {
                        DefinedTableType = new DbUserDefinedTableTypeAttribute(dbTest.EntityModel.Name)
                    }).Apply())
                    {
                        DbContext.SaveChanges().Should().BeTrue();
                    }
                }
                else
                {
                    DbContext.SaveChanges().Should().BeTrue();
                }

                FluentActions.Invoking(() =>
                    DbContext.Database.Instance.RecievedCommand(dbTest.Expectation))
                    .Should().NotThrow();

                DbContext.Database.Instance.RecievedCommandCount(dbTest.Expectation)
                    .Should()
                    .Be(dbTest.UseDefinedTableType ? 1 : expected.Count());

                foreach (IEntityProxy proxy in expected)
                {
                    if (proxy is Models.Person person)
                    {
                        person.FullName.Should().Be("Walters, Bob S.");
                    }
                    else if (proxy is Models.Renter renter)
                    {
                        renter.EndDate.Should().Be(DateTime.Today);
                    }

                    proxy.IsDirty.Should().BeFalse();
                }
            }
        }

        private DataTable UpdateCmdBehaviorForInArray(DbCommand cmd, IEnumerable<IEntityProxy> expected)
        {
            IEntityProxy proxy = expected.ElementAt(0);

            if (proxy is Models.Person)
            {
                Models.Person person = People.Single(x => x.ID == cmd.Parameters["@id_1"].GetValue<int>());

                person.FirstName = cmd.Parameters["@FirstName"].GetValue<string>();
                person.MiddleInitial = cmd.Parameters["@MiddleInitial"].GetValue<string>();
                person.FamilyName = cmd.Parameters["@FamilyName"].GetValue<string>();

                person.FullName = String.Format("{0}, {1}{2}",
                    person.FamilyName, person.FirstName,
                    person.MiddleInitial.IsNotNullOrEmpty() ? $" {person.MiddleInitial}." : "");

                return new[] { person }.ToDataTable();
            }
            else if (proxy is Models.Renter)
            {
                Models.Renter renter = Renters.Single(x =>
                    x.PersonID == cmd.Parameters["@personid_1"].GetValue<int>() &&
                    x.UnitID == cmd.Parameters["@unitid_2"].GetValue<int>());

                renter.PersonID = cmd.Parameters[$"@{nameof(Models.Renter.PersonID)}"].GetValue<int>();
                renter.UnitID = cmd.Parameters[$"@{nameof(Models.Renter.UnitID)}"].GetValue<int>();
                renter.Rent = cmd.Parameters[$"@{nameof(Models.Renter.Rent)}"].GetValue<decimal>();
                renter.StartDate = cmd.Parameters[$"@{nameof(Models.Renter.StartDate)}"].GetValue<DateTime>();
                renter.EndDate = cmd.Parameters[$"@{nameof(Models.Renter.EndDate)}"].GetValue<DateTime>();

                return new[] { renter }.ToDataTable();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private DataTable UpdateCmdBehaviorForUDTT(DbCommand cmd, IEnumerable<IEntityProxy> expected)
        {
            using (DataTable table = cmd.Parameters["@update"].GetValue<DataTable>())
            {
                if (expected.ElementAt(0) is Models.Person)
                {
                    List<Models.Person> result = new List<Models.Person>();

                    foreach (DataRow entity in table.Rows)
                    {
                        Models.Person person = People.Single(x => x.ID == (int)entity[nameof(Models.Person.ID)]);

                        person.FirstName = (string)entity[nameof(Models.Person.FirstName)];
                        person.MiddleInitial = (string)entity[nameof(Models.Person.MiddleInitial)];
                        person.FamilyName = (string)entity[nameof(Models.Person.FamilyName)];

                        person.FullName = String.Format("{0}, {1}{2}",
                            person.FamilyName, person.FirstName,
                            person.MiddleInitial.IsNotNullOrEmpty() ? $" {person.MiddleInitial}." : "");

                        result.Add(person);
                    }

                    return result.ToDataTable();
                }
                else if (expected.ElementAt(0) is Models.Renter)
                {
                    List<Models.Renter> result = new List<Models.Renter>();

                    foreach (DataRow entity in table.Rows)
                    {
                        Models.Renter renter = Renters.Single(x => 
                            x.PersonID == (int)entity[nameof(Models.Renter.PersonID)] &&
                            x.UnitID == (int)entity[nameof(Models.Renter.UnitID)]);

                        renter.PersonID = (int)entity[nameof(Models.Renter.PersonID)];
                        renter.UnitID = (int)entity[nameof(Models.Renter.UnitID)];
                        renter.Rent = (decimal)entity[nameof(Models.Renter.Rent)];
                        renter.StartDate = (DateTime)entity[nameof(Models.Renter.StartDate)];
                        renter.EndDate = (DateTime?)entity[nameof(Models.Renter.EndDate)];

                        result.Add(renter);
                    }

                    return result.ToDataTable();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
