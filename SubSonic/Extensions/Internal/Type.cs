﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SubSonic
{
    internal static partial class InternalExtensions
    {
        public static SqlDbType GetSqlDbType(this Type type, bool unicode = false)
        {
            SqlDbType result = SqlDbType.Variant;

            if (type == typeof(int))
            {
                result = SqlDbType.Int;
            }
            else if (type == typeof(short))
            {
                result = SqlDbType.SmallInt;
            }
            else if (type == typeof(long))
            {
                result = SqlDbType.BigInt;
            }
            else if (type == typeof(DateTime))
            {
                result = SqlDbType.DateTime;
            }
            else if (type == typeof(float))
            {
                result = SqlDbType.Real;
            }
            else if (type == typeof(decimal))
            {
                result = SqlDbType.Decimal;
            }
            else if (type == typeof(double))
            {
                result = SqlDbType.Float;
            }
            else if (type == typeof(Guid))
            {
                result = SqlDbType.UniqueIdentifier;
            }
            else if (type == typeof(bool))
            {
                result = SqlDbType.Bit;
            }
            else if (type == typeof(byte))
            {
                result = SqlDbType.TinyInt;
            }
            else if (type == typeof(byte[]))
            {
                result = SqlDbType.Binary;
            }
            else if (type == typeof(string))
            {
                result = unicode ? SqlDbType.NVarChar : SqlDbType.VarChar;
            }
            else if (type == typeof(char))
            {
                result = unicode ? SqlDbType.NChar : SqlDbType.Char;
            }
            else if (type.IsSubclassOf(typeof(object)))
            {
                result = SqlDbType.Structured;
            }

            return result;
        }
        public static DbType GetDbType(this Type type, bool unicode = false)
        {
            DbType result;

            if (type == typeof(int))
            {
                result = DbType.Int32;
            }
            else if (type == typeof(short))
            {
                result = DbType.Int16;
            }
            else if (type == typeof(long))
            {
                result = DbType.Int64;
            }
            else if (type == typeof(DateTime))
            {
                result = DbType.DateTime;
            }
            else if (type == typeof(float))
            {
                result = DbType.Single;
            }
            else if (type == typeof(decimal))
            {
                result = DbType.Decimal;
            }
            else if (type == typeof(double))
            {
                result = DbType.Double;
            }
            else if (type == typeof(Guid))
            {
                result = DbType.Guid;
            }
            else if (type == typeof(bool))
            {
                result = DbType.Boolean;
            }
            else if (type == typeof(byte[]))
            {
                result = DbType.Binary;
            }
            else if (type == typeof(char))
            {
                result = unicode ? DbType.StringFixedLength : DbType.AnsiStringFixedLength;
            }
            else
            {
                result = unicode ? DbType.String : DbType.AnsiString;
            }

            return result;
        }
        public static bool IsBoolean(this Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }

        public static bool IsNullableType(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type GetUnderlyingType(this Type type)
        {
            return type.IsNullableType() ? Nullable.GetUnderlyingType(type) : type;
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static Type[] BuildGenericArgumentTypes(this Type type)
        {
            return type.IsGenericType ? type.GetGenericArguments() : new[] { type };
        }

        public static Type GetQualifiedType(this Type type)
        {
            return type.IsGenericType ? type.GetGenericArguments().First() : type;
        }

        public static Type FindIEnumerable(this Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                        return ienum;
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null)
                        return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
                return FindIEnumerable(seqType.BaseType);
            return null;
        }

        public static Type GetElementType(this Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null)
                return seqType;
            return ienum.GetGenericArguments()[0];
        }

        public static bool IsScalar(this Type type)
        {
            type = type.GetUnderlyingType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Empty:
                case TypeCode.DBNull:
                    return false;
                case TypeCode.Object:
                    return
                        type == typeof(DateTime) ||
                        type == typeof(DateTimeOffset) ||
                        type == typeof(decimal) ||
                        type == typeof(Guid) ||
                        type == typeof(byte[]);
                default:
                    return true;
            }
        }

        public static string GetTypeName(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string name = type.Name;
            name = name.Replace('+', '.');
            int iGeneneric = name.IndexOf('`', StringComparison.CurrentCulture);
            if (iGeneneric > 0)
            {
                name = name.Substring(0, iGeneneric);
            }
            if (type.IsGenericType || type.IsGenericTypeDefinition)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(name);
                sb.Append("<");
                var args = type.GetGenericArguments();
                for (int i = 0, n = args.Length; i < n; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    if (type.IsGenericType)
                    {
                        sb.Append(GetTypeName(args[i]));
                    }
                }
                sb.Append(">");
                name = sb.ToString();
            }
            return name;
        }
    }
}
