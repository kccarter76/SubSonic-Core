﻿using SubSonic.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SubSonic.Infrastructure
{
    public interface IDbSqlQueryBuilderProvider
        : IQueryProvider
    {
        Expression BuildLogicalBinary(Expression eBody, DbExpressionType type, string name, object value, DbComparisonOperator op, DbGroupOperator group);
        Expression BuildWherePredicate(Expression collection, Expression logical);
        Expression BuildSelect(IQueryable queryable);
        Expression BuildSelect(IQueryable queryable, Expression eWhere);
        Expression BuildSelect(Expression eSelect, Expression eWhere);
        Expression BuildSelect(Expression eSelect, DbExpressionType eType, IEnumerable<Expression> expressions);
        Expression BuildSelect<TEntity, TColumn>(Expression eSelect, Expression<Func<TEntity, TColumn>> selector);
        Expression BuildWhere(DbTableExpression table, Expression where, Type type, LambdaExpression predicate);
        Expression BuildWhereExists<TEntity>(DbTableExpression dbTableExpression, Type type, Expression<Func<TEntity, IQueryable>> query);
        Expression BuildWhereNotExists<TEntity>(DbTableExpression from, Type type, Expression<Func<TEntity, IQueryable>> query);
        Expression BuildLambda(Expression body, LambdaType callType, params string[] properties);
        Expression BuildCall(string nameOfCallee, Expression collection, Expression lambda);
        IDbQueryObject ToQueryObject(Expression expr);
    }
}