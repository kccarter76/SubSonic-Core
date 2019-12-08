﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace SubSonic.Infrastructure
{
    public class DbSet<TEntity>
        : IQueryable<TEntity>, IEnumerable<TEntity>, IQueryable, IEnumerable, IListSource
    {
        private readonly IQueryProvider provider;
        private readonly DbEntityModel model;
        private readonly List<TEntity> queryableData;
        
        public DbSet(ISubSonicQueryProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));

            this.queryableData = new List<TEntity>();
            this.Expression = queryableData.AsQueryable().Expression;
            this.model = DbContext.Model.GetEntityModel<TEntity>();
        }

        protected DbContext DbContext => ((ISubSonicQueryProvider)provider).DbContext;

        public DbSet(ISubSonicQueryProvider provider, Expression expression)
            : this(provider)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public Type ElementType => typeof(TEntity);

        public Expression Expression { get; }

        public IQueryProvider Provider => provider;

        public bool ContainsListCollection => true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerator GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IList GetList()
        {
            return queryableData;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return ((IEnumerable<TEntity>)queryableData).GetEnumerator();
        }

        public IQueryable<TEntity> FindByID(params object[] keyData)
        {
            DbExpressionBuilder builder = new DbExpressionBuilder(Expression.Parameter(ElementType, ElementType.Name.ToLower(CultureInfo.CurrentCulture)), (ConstantExpression)Expression);

            for(int i = 0; i < model.PrimaryKey.Length; i++)
            {
                builder.BuildComparisonExpression(model.PrimaryKey[i], keyData[i], EnumComparisonOperator.Equal, EnumGroupOperator.AndAlso);
            }

            return Provider.CreateQuery<TEntity>(
                builder
                    .CallExpression<TEntity>(EnumCallExpression.Where)
                    .ForEachProperty(model.PrimaryKey, property => 
                        builder.CallExpression<TEntity>(EnumCallExpression.OrderBy, property))
                    .ToMethodCallExpression());
        }
    }
}
