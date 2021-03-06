﻿using Dasync.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubSonic.Collections
{
    public sealed partial class SubSonicTableTypeCollection<TEntity>
        : IAsyncSubSonicQueryable<TEntity>
    {
        IAsyncSubSonicQueryProvider IAsyncSubSonicQueryable<TEntity>.ProviderAsync
        {
            get
            {
                if (Provider is IAsyncSubSonicQueryProvider provider)
                {
                    return provider;
                }

                return null;
            }
        }
#if NETSTANDARD2_0
        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new AsyncEnumerable<TEntity>(async (yield) =>
            {
                foreach (TEntity entity in this)
                {
                    await yield.ReturnAsync(entity).ConfigureAwait(false);
                }
            }).GetAsyncEnumerator(cancellationToken);
        }
#elif NETSTANDARD2_1
        async IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            await foreach (TEntity entity in this.WithCancellation(cancellationToken))
            {
                yield return entity;
            }
        }
#endif
    }
}
