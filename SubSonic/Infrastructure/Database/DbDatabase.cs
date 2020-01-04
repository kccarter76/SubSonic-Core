﻿using SubSonic.Infrastructure.Logging;
using System;
using System.Data.Common;

namespace SubSonic.Infrastructure
{
    using Linq;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
#if DB_PROVIDER_NOT_DEFINED
    using Factories;
#endif
    public class DbDatabase
        : IInfrastructure<DbProviderFactory>
    {
        [ThreadStatic]
        private static DbConnection dBSharedConnection;

        private readonly ISubSonicLogger<DbDatabase> logger;
        private readonly DbContext dbContext;
        private readonly DbProviderFactory dbProvider;
        private readonly ISqlQueryProvider queryProvider;

        public DbDatabase(ISubSonicLogger<DbDatabase> logger, DbContext dbContext, DbProviderFactory dbProvider, ISqlQueryProvider queryProvider)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            this.queryProvider = queryProvider ?? throw new ArgumentNullException(nameof(queryProvider));
        }

        public DbProviderFactory Instance => dbProvider;

        #region connections
        internal DbConnection CurrentSharedConnection
        {
            get
            {
                return dBSharedConnection;
            }
            set
            {
                if (value.IsNull())
                {
                    dBSharedConnection.IsNotNull(Con => Con.Dispose());
                    dBSharedConnection = null;
                }
                else
                {
                    dBSharedConnection = value;
                    dBSharedConnection.Disposed += DBSharedConnection_Disposed;
                }
            }
        }

        private static void DBSharedConnection_Disposed(object sender, EventArgs e)
        {
            dBSharedConnection = null;
        }

        internal DbConnection InitializeSharedConnection()
        {
            if (CurrentSharedConnection == null)
                CurrentSharedConnection = CreateConnection();

            return CurrentSharedConnection;
        }

        internal DbConnection CreateConnection()
        {
            DbConnection connection = dbProvider.CreateConnection();
            if (dbContext.GetConnectionString.IsNotNull())
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();

                connection.ConnectionString = dbContext.GetConnectionString(builder, dbContext.Options);
            }
            return connection;
        }

        internal void ResetSharedConnection()
        {
            CurrentSharedConnection.IsNotNull(Con => Con.Dispose());
        }
        #endregion


        public DbDataReader ExecuteReader(string sql, IEnumerable<SubSonicParameter> parameters)
        {
            using (AutomaticConnectionScope Scope = dbContext.Instance.GetService<AutomaticConnectionScope>())
            using (DbCommand cmd = GetCommand(Scope, sql, parameters))
            using (var perf = logger.Start(GetType(), $"{nameof(ExecuteReader)}"))
            {
                return cmd.ExecuteReader();
            }
        }

        public DbDataReader ExecuteReader(IDbQueryObject queryObject)
        {
            if (queryObject is null)
            {
                throw new ArgumentNullException(nameof(queryObject));
            }

            using (AutomaticConnectionScope Scope = dbContext.Instance.GetService<AutomaticConnectionScope>())
            using (DbCommand cmd = GetCommand(Scope, queryObject))
            using (var perf = logger.Start(GetType(), $"{nameof(ExecuteReader)}"))
            {
                return cmd.ExecuteReader(queryObject.Behavior);
            }
        }

        public static DbCommand GetCommand(IConnectionScope scope, IDbQueryObject queryObject)
        {
            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            if (queryObject is null)
            {
                throw new ArgumentNullException(nameof(queryObject));
            }

            return GetCommand(scope, queryObject.Sql, queryObject.Parameters);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Builder paramatizes all user inputs when sql expression tree is visited")]
        public static DbCommand GetCommand(IConnectionScope scope, string sql, IEnumerable<SubSonicParameter> parameters)
        {
            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentException("", nameof(sql));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            DbCommand command = scope.Connection.CreateCommand();

            Debug.Assert(command.CommandType == CommandType.Text);

            foreach (SubSonicParameter parameter in parameters)
            {
                DbParameter dbParameter = command.CreateParameter();

                dbParameter.Map(parameter);

                command.Parameters.Add(dbParameter);
            }

            command.CommandText = sql;

            return command;
        }
    }
}
