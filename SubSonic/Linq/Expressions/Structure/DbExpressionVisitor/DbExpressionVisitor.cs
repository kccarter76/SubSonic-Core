﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SubSonic.Linq.Expressions.Structure
{
    using Infrastructure;

    public abstract partial class DbExpressionVisitor
        : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            if (node.IsNull())
            {
                return node;
            }

            switch ((DbExpressionType)node.NodeType)
            {
                case DbExpressionType.Aggregate:
                    return VisitAggregate((DbAggregateExpression)node);
                case DbExpressionType.AggregateSubQuery:
                    return VisitAggregateSubQuery((DbAggregateSubQueryExpression)node);
                case DbExpressionType.RowCount:
                    return VisitRowNumber((DbRowNumberExpression)node);
                case DbExpressionType.Projection:
                    return VisitProjection((DbProjectionExpression)node);
                case DbExpressionType.ClientJoin:
                    return VisitClientJoin((DbClientJoinExpression)node);
                default:
                    return base.Visit(node);
            }
        }

        protected internal virtual Expression VisitDbConstant(DbConstantExpression constant)
        {
            if (constant.IsNotNull())
            {
                if (constant.Reduce() is ConstantExpression _constant)
                {
                    VisitConstant(_constant);
                }
            }
            return constant;
        }

        protected virtual Expression VisitClientJoin(DbClientJoinExpression join)
        {
            if (join is null)
            {
                return join;
            }

            DbProjectionExpression projection = (DbProjectionExpression)this.Visit(join.Projection);

            var outerKey = this.VisitExpressionList(join.OuterKey);
            var innerKey = this.VisitExpressionList(join.InnerKey);

            if (projection != join.Projection || outerKey != join.OuterKey || innerKey != join.InnerKey)
            {
                return new DbClientJoinExpression(projection, outerKey, innerKey);
            }

            return join;
        }

        protected virtual Expression VisitProjection(DbProjectionExpression projection)
        {
            if(projection is null)
            {
                return projection;
            }

            DbSelectExpression source = (DbSelectExpression)this.Visit(projection.Source);
            Expression projector = this.Visit(projection.Projector);
            if (source != projection.Source || projector != projection.Projector)
            {
                return new DbProjectionExpression(source, projector, projection.Aggregator);
            }
            return projection;
        }

        protected virtual Expression VisitRowNumber(DbRowNumberExpression rowNumber)
        {
            if(rowNumber is null)
            {
                return rowNumber;
            }

            var orderby = this.VisitOrderBy(rowNumber.OrderBy);

            if (orderby != rowNumber.OrderBy)
            {
                return new DbRowNumberExpression(orderby);
            }

            return rowNumber;
        }

        protected internal virtual Expression VisitBetween(DbBetweenExpression between)
        {
            if (between is null)
            {
                return between;
            }

            Expression expr = this.Visit(between.Value);
            Expression lower = this.Visit(between.Lower);
            Expression upper = this.Visit(between.Upper);

            if (expr != between.Value || lower != between.Lower || upper != between.Upper)
            {
                switch ((DbExpressionType)between.NodeType)
                {
                    case DbExpressionType.Between:
                        return DbExpression.DbBetween(expr, lower, upper);
                    case DbExpressionType.NotBetween:
                        return DbExpression.DbNotBetween(expr, lower, upper);
                }
            }

            return between;
        }

        protected virtual Expression VisitAggregateSubQuery(DbAggregateSubQueryExpression aggregate)
        {
            if (aggregate is null)
            {
                return aggregate;
            }

            Expression e = Visit(aggregate.AggregateAsSubQuery);

            switch((DbExpressionType)e.NodeType)
            {
                case DbExpressionType.Scalar:
                    {
                        DbScalarExpression subQuery = (DbScalarExpression)e;

                        if (subQuery != aggregate.AggregateAsSubQuery)
                        {
                            return new DbAggregateSubQueryExpression(aggregate.GroupByAlias, aggregate.AggregateInGroupSelect, subQuery);
                        }
                    }
                    break;
            }
            
            return aggregate;
        }

        protected internal virtual Expression VisitScalar(DbScalarExpression scalar)
        {
            if (scalar is null)
            {
                return scalar;
            }

            DbExpression select = (DbExpression)Visit(scalar.Select);

            if (select != scalar.Select)
            {
                return DbExpression.DbScalar(scalar.Type, select);
            }

            return scalar;
        }

        protected internal virtual Expression VisitIn(DbInExpression inExp)
        {
            if (inExp is null)
            {
                return inExp;
            }

            Expression left = Visit(inExp.Left);

            if (inExp.Inside is DbSelectExpression select)
            {
                select = (DbSelectExpression)Visit(inExp.Inside);

                if (left != inExp.Left || select != inExp.Inside)
                {
                    switch ((DbExpressionType)inExp.NodeType)
                    {
                        case DbExpressionType.In:
                            return DbExpression.DbIn(left, select);
                        case DbExpressionType.NotIn:
                            return DbExpression.DbNotIn(left, select);
                    }
                }
            }
            else if(inExp.Inside is NewArrayExpression array)
            {
                array = (NewArrayExpression)Visit(array);

                if (left != inExp.Left || array != inExp.Inside)
                {
                    switch ((DbExpressionType)inExp.NodeType)
                    {
                        case DbExpressionType.In:
                            return DbExpression.DbIn(left, array);
                        case DbExpressionType.NotIn:
                            return DbExpression.DbNotIn(left, array);
                    }
                }
            }
            return inExp;
        }

        protected internal virtual Expression VisitNull(DbIsNullExpression isnull)
        {
            if(isnull is null)
            {
                return isnull;
            }

            Expression expr = Visit(isnull.Expression);

            if (expr != isnull.Expression)
            {
                return DbExpression.DbIsNull((DbExpressionType)isnull.NodeType, expr);
            }

            return isnull;
        }

        protected virtual Expression VisitAggregate(DbAggregateExpression aggregate)
        {
            if (aggregate is null)
            {
                return aggregate;
            }

            Expression arg = this.Visit(aggregate.Argument);
            if (arg != aggregate.Argument)
            {
                return new DbAggregateExpression(aggregate.Type, aggregate.AggregateType, arg, aggregate.IsDistinct);
            }
            return aggregate;
        }

        protected internal virtual Expression VisitOuterJoined(DbOuterJoinedExpression outer)
        {
            if (outer is null)
            {
                return outer;
            }

            Expression test = this.Visit(outer.Test);
            Expression expression = this.Visit(outer.Expression);
            if (test != outer.Test || expression != outer.Expression)
            {
                return DbExpression.DbOuterJoined(test, expression);
            }
            return outer;
        }

        protected internal virtual Expression VisitJoin(DbJoinExpression join)
        {
            if(join is null)
            {
                return join;
            }

            Expression left = this.VisitSource(join.Left);
            Expression right = this.VisitSource(join.Right);
            Expression condition = this.Visit(join.Condition);

            if (left != join.Left || right != join.Right || condition != join.Condition)
            {
                return DbExpression.DbJoin(join.Join, left, right, condition);
            }

            return join;
        }

        protected internal virtual Expression VisitSelect(DbSelectExpression selectExp)
        {
            if (selectExp.IsNull())
            {
                return selectExp;
            }

            DbTableExpression from = (DbTableExpression)VisitSource(selectExp.From);
            Expression where = selectExp.Where;
            ReadOnlyCollection<DbOrderByDeclaration> orderBy = VisitOrderBy(selectExp.OrderBy);
            ReadOnlyCollection<Expression> groupBy = VisitExpressionList(selectExp.GroupBy);
            Expression skip = Visit(selectExp.Skip);
            Expression take = Visit(selectExp.Take);
            IReadOnlyCollection<DbColumnDeclaration> columns = VisitColumnDeclarations(selectExp.Columns);
            if (from != selectExp.From
                || where != selectExp.Where
                || orderBy != selectExp.OrderBy
                || groupBy != selectExp.GroupBy
                || take != selectExp.Take
                || skip != selectExp.Skip
                || columns != selectExp.Columns
                )
            {
                return new DbSelectExpression(selectExp.QueryObject, from, columns, where, orderBy, groupBy, selectExp.IsDistinct, skip, take);
            }
            return selectExp;
        }

        protected virtual IReadOnlyCollection<DbColumnDeclaration> VisitColumnDeclarations(IReadOnlyCollection<DbColumnDeclaration> columns)
        {
            if (columns is null)
            {
                return columns;
            }

            List<DbColumnDeclaration> alternate = null;
            for (int i = 0, n = columns.Count; i < n; i++)
            {
                DbColumnDeclaration column = columns.ElementAt(i);
                Expression e = this.Visit(column.Expression);
                if (alternate == null && e != column.Expression)
                {
                    alternate = columns.Take(i).ToList();
                }
                if (alternate != null)
                {
                    alternate.Add(new DbColumnDeclaration(column.Property));
                }
            }
            if (alternate != null)
            {
                return alternate.AsReadOnly();
            }
            return columns;
        }

        protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            if (original != null)
            {
                List<Expression> list = null;
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    Expression p = this.Visit(original[i]);
                    if (list != null)
                    {
                        list.Add(p);
                    }
                    else if (p != original[i])
                    {
                        list = new List<Expression>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(p);
                    }
                }
                if (list != null)
                {
                    return list.AsReadOnly();
                }
            }
            return original;
        }

        protected virtual ReadOnlyCollection<DbOrderByDeclaration> VisitOrderBy(ReadOnlyCollection<DbOrderByDeclaration> expressions)
        {
            if (expressions != null)
            {
                List<DbOrderByDeclaration> alternate = null;
                for (int i = 0, n = expressions.Count; i < n; i++)
                {
                    DbOrderByDeclaration expr = expressions[i];
                    Expression e = this.Visit(expr.Expression);
                    if (alternate == null && e != expr.Expression)
                    {
                        alternate = expressions.Take(i).ToList();
                    }
                    if (alternate != null)
                    {
                        alternate.Add(new DbOrderByDeclaration(expr.OrderByType, e));
                    }
                }
                if (alternate != null)
                {
                    return alternate.AsReadOnly();
                }
            }
            return expressions;
        }

        protected virtual Expression VisitSource(Expression source)
        {
            return this.Visit(source);
        }

        protected internal virtual DbExpression VisitExpression(DbExpression expression)
        {
            return expression;
        }
    }
}
