﻿using SubSonic.Data.DynamicProxies;
using SubSonic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace SubSonic
{
    public partial class DbContext
    {
        internal static DbModel DbModel => ServiceProvider.GetService<DbContext>().IsNotNull(Ctx => Ctx.Model);
        internal static DbContextOptions DbOptions => ServiceProvider.GetService<DbContext>().IsNotNull(Ctx => Ctx.Options);
        internal static IServiceProvider ServiceProvider { get; set; }
        internal Func<DbConnectionStringBuilder, DbContextOptions, string> GetConnectionString { get; set; }

        protected internal static object CreateObject(Type type)
        {
            if (DbOptions.EnableProxyGeneration)
            {
                DynamicProxyWrapper proxy = DynamicProxy.GetProxyWrapper(type);

                return Activator.CreateInstance(proxy.Type, ServiceProvider.GetService<DbContextAccessor>());
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
