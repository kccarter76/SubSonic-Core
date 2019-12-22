﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SubSonic.Extensions.Test.MockDbClient
{
    public class MockDbCommand : DbCommand
    {
        IMockCommandExecution _exec;
        MockDbParameterCollection parameters;
        internal MockDbCommand(IMockCommandExecution exec)
        {
            _exec = exec;
            parameters = new MockDbParameterCollection();
        }

        protected static Regex ParameterRegex => new Regex(@"\@([^=<>\s]+)(?:[a-z]|[0-9]|_|\b)", RegexOptions.Multiline | RegexOptions.Compiled);

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override string CommandText
        {
            get;
            set;
        }

        public override int CommandTimeout
        {
            get;
            set;
        }

        public override System.Data.CommandType CommandType
        {
            get;
            set;
        }

        protected override DbParameter CreateDbParameter()
        {
            return new MockDbParameter();
        }

        protected override DbConnection DbConnection
        {
            get;
            set;
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get;
            set;
        }

        public override bool DesignTimeVisible
        {
            get;
            set;
        }

        protected override DbDataReader ExecuteDbDataReader(System.Data.CommandBehavior behavior)
        {
            Prepare();

            return _exec.ExecuteDataReader(this);
        }

        protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            return Task.Run(() => ExecuteDbDataReader(behavior), cancellationToken);
        }

        public override int ExecuteNonQuery()
        {
            Prepare();

            return _exec.ExecuteNonQuery(this);
        }

        public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => ExecuteNonQuery(), cancellationToken);
        }

        public override object ExecuteScalar()
        {
            Prepare();

            return _exec.ExecuteScalar(this);
        }

        public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => ExecuteScalar(), cancellationToken);
        }


        /// <summary>
        /// Prepare the command for execution against the data source
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "prepare is looking for parameters and replacing with real values")]
        public override void Prepare()
        {
            if(ParameterRegex.IsMatch(this.CommandText))
            {
                foreach(Match match in ParameterRegex.Matches(this.CommandText))
                {
                    object value = Parameters[match.Value].Value;

                    CommandText = CommandText.Replace(match.Value, (value is string || value is Guid) ? $"'{value}'" : value.ToString(), StringComparison.CurrentCulture);
                }
            }
        }

        public override System.Data.UpdateRowSource UpdatedRowSource
        {
            get;
            set;
        }
    }
}