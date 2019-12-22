﻿using Microsoft.Extensions.Logging;
using SubSonic.Extensions;
using SubSonic.Extensions.Test;
using SubSonic.Infrastructure;
using SubSonic.Tests.DAL.SUT.NUnit;
using Models = SubSonic.Extensions.Test.Models;

namespace SubSonic.Tests.DAL.SUT
{
    public class TestDbContext
        : DbContext
    {
        public TestDbContext()
            : base()
        {

        }

        public DbSetCollection<Models.RealEstateProperty> RealEstateProperties { get; private set; }

        public DbSetCollection<Models.Status> Statuses { get; private set; }

        public DbSetCollection<Models.Unit> Units { get; private set; }

        public DbSetCollection<Models.Renter> Renters { get; private set; }

        public DbSetCollection<Models.Person> People { get; private set; }

        protected override void OnDbConfiguring(DbContextOptionsBuilder config)
        {
            config
                .ConfigureServiceCollection()
                .AddLogging((_config) => _config.AddNUnitLogger<TestDbContext>(LogLevel.Debug))
                .EnableProxyGeneration()
                .UseMockDbClient((builder, options) =>
                {
                    builder
                        .SetDatasource("localhost")
                        .SetInitialCatalog("test")
                        .SetIntegratedSecurity(true);
                });
        }

        protected override void OnDbModeling(DbModelBuilder builder)
        {
            builder
                .AddEntityModel<Models.RealEstateProperty>()
                .AddEntityModel<Models.Status>()
                .AddEntityModel<Models.Unit>()
                .AddEntityModel<Models.Renter>()
                .AddEntityModel<Models.Person>();

            builder.AddRelationshipFor<Models.RealEstateProperty>(() =>
                builder.GetRelationshipFor<Models.RealEstateProperty>()
                    .HasMany(Model => Model.Units)
                    .WithOne(Model => Model.RealEstateProperty));

            builder.AddRelationshipFor<Models.RealEstateProperty>(() =>
                builder.GetRelationshipFor<Models.RealEstateProperty>()
                    .HasOne(Model => Model.Status)
                    .WithOne());

            builder.AddRelationshipFor<Models.Unit>(() =>
                builder.GetRelationshipFor<Models.Unit>()
                    .HasOne(Model => Model.RealEstateProperty)
                    .WithMany(Model => Model.Units));

            builder.AddRelationshipFor<Models.Unit>(() =>
                builder.GetRelationshipFor<Models.Unit>()
                    .HasMany(Model => Model.Renters)
                    .WithMany(Model => Model.Person));

            builder.AddRelationshipFor<Models.Unit>(() =>
                builder.GetRelationshipFor<Models.Unit>()
                    .HasOne(Model => Model.Status)
                    .WithOne());

            builder.AddRelationshipFor<Models.Person>(() =>
                builder.GetRelationshipFor<Models.Person>()
                    .HasMany(Model => Model.Renters)
                    .WithMany(Model => Model.Unit));
        }
    }
}