﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SubSonic.Collections
{
    using Builders;
    using Linq.Expressions;
    using Logging;
    using Schema;
    using SubSonic;

    public sealed partial class SubSonicCollection<TEntity>
        : SubSonicCollection
        , ISubSonicCollection<TEntity>
    {
        public SubSonicCollection()
            : base(typeof(TEntity))
        {

        }

        public SubSonicCollection(IEnumerable<TEntity> entities)
            : base(typeof(TEntity), SubSonicContext.ServiceProvider.GetService<ISubSonicQueryProvider<TEntity>>(), null, entities)
        {

        }

        public SubSonicCollection(IQueryProvider provider, Expression expression)
            : base(typeof(TEntity), provider, expression)
        {

        }

        public SubSonicCollection(IQueryProvider provider, Expression expression, IEnumerable<TEntity> enumerable)
            : base(typeof(TEntity), provider, expression, enumerable)
        {

        }

        #region ICollection<> Implementation
        public void Clear()
        {
            if (TableData is ICollection<TEntity> data)
            {
                data.Clear();
            }
            else
            {
                throw Error.NotSupported();
            }
        }
        public void Add(TEntity element)
        {
            if (TableData is ICollection<TEntity> data)
            {
                if (!IsReadOnly)
                {
                    data.Add(element);
                }
            }
            else
            {
                throw Error.NotSupported(); ;
            }
        }

        public void AddRange(IEnumerable<TEntity> elements)
        {
            if (!(elements is null))
            {
                foreach (TEntity element in elements)
                {
                    Add(element);
                }
            }
        }

        public bool Remove(TEntity element)
        {
            if (TableData is ICollection<TEntity> data)
            {
                return data.Remove(element);
            }
            else
            {
                throw Error.NotSupported();
            }
        }

        public bool Contains(TEntity element)
        {
            if (TableData is ICollection<TEntity> data)
            {
                return data.Contains(element);
            }
            else
            {
                throw Error.NotSupported();
            }
        }

        public void CopyTo(TEntity[] elements, int startAt)
        {
            if (TableData is ICollection<TEntity> data)
            {
                data.Select(x => x).ToArray().CopyTo(elements, startAt);
            }
            else
            {
                throw Error.NotSupported();
            }
        }

        public override IEnumerable ToArray()
        {
            if (TableData is ICollection<TEntity> data)
            {
                return data.ToArray();
            }

            throw Error.NotSupported();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            if (TableData is ICollection<TEntity> data)
            {
                if (!IsLoaded)
                {
                    Load();
                }

                return data.GetEnumerator();
            }
            else
            {
                throw Error.NotSupported();
            }
        }
        #endregion
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1010:Collections should implement generic interface", Justification = "Generic Class that inherits from this one addresses the generic interface")]
    public class SubSonicCollection
        : ISubSonicCollection
    {
        protected bool IsLoaded { get; set; }

        public SubSonicCollection(Type elementType)
        {
            ElementType = elementType ?? throw new ArgumentNullException(nameof(elementType));
            TableData = (IEnumerable)Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(elementType));

            if (SubSonicContext.DbModel.TryGetEntityModel(elementType, out IDbEntityModel model))
            {
                Model = model;
                Expression = DbExpression.DbSelect(this, GetType(), Model.Table);
                Provider = new DbSqlQueryBuilder(ElementType, SubSonicContext.ServiceProvider.GetService<ISubSonicLogger>());
            }
            else
            {
                Expression = Expression.Constant(this);
            }
        }
        public SubSonicCollection(Type elementType, IQueryProvider provider, Expression expression)
            : this(elementType)
        {
            Expression = expression ?? DbExpression.DbSelect(this, GetType(), Model.Table);
            Provider = provider ?? new DbSqlQueryBuilder(ElementType, SubSonicContext.ServiceProvider.GetService<ISubSonicLogger>());
        }

        public SubSonicCollection(Type elementType, IQueryProvider provider, Expression expression, IEnumerable elements)
            : this(elementType, provider, expression)
        {
            if (elements is ISubSonicCollection _elements)
            {
                TableData = (IEnumerable)Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(elementType), _elements.ToArray());
            }
            else
            {
                TableData = (IEnumerable)Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(elementType), elements);
            }
            
            IsLoaded = IsLoadedCheck(elements);
        }

        protected IDbEntityModel Model { get; }

        protected IEnumerable TableData { get; private set; }

        public Type ElementType { get; }

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public virtual IQueryable Load()
        {
            if (Provider.Execute(Expression) is ISubSonicCollection elements)
            {
                TableData = (IEnumerable)Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(ElementType), elements.ToArray());

                IsLoaded = true;
            }

            return this;
        }

        public virtual IEnumerable ToArray()
        {
            throw Error.NotImplemented();
        }

        private bool IsLoadedCheck(IEnumerable elements)
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            foreach (object ele in (elements ?? Array.Empty<object>()))
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            {
                return true;
            }

            return false;
        }

        #region ICollection<> Implementation
        public int Count => (int)TableData.GetType().GetProperty(nameof(Count)).GetValue(TableData);
        public bool IsReadOnly => false;
        public IEnumerator GetEnumerator()
        {
            if (!IsLoaded)
            {
                Load();
            }

            return TableData.GetEnumerator();
        }
        #endregion

    }
}