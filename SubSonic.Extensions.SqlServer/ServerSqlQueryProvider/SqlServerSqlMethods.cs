﻿// https://github.com/microsoft/referencesource/blob/master/System.Data.Linq/SqlClient/SqlMethods.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace SubSonic.Extensions.SqlServer
{
    using SqlGenerator;
    using SubSonic.Data.Linq;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>
    /// Sql Method Stubs Used In Linq Queries
    /// </summary>
    public class SqlServerSqlMethods
        : ISqlMethods
    {
        /// <summary>
        /// Counts the number of year boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(YEAR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of year boundaries crossed between the dates.</returns>
        public static int DateDiffYear(DateTime startDate, DateTime endDate)
        {
            return endDate.Year - startDate.Year;
        }


        /// <summary>
        /// Counts the number of year boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(YEAR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of year boundaries crossed between the dates.</returns>
        public static int? DateDiffYear(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffYear(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of year boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(YEAR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of year boundaries crossed between the dates.</returns>
        public static int DateDiffYear(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffYear(startDate.UtcDateTime, endDate.UtcDateTime);
        }


        /// <summary>
        /// Counts the number of year boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(YEAR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of year boundaries crossed between the dates.</returns>
        public static int? DateDiffYear(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffYear(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of month boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MONTH,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of month boundaries crossed between the dates.</returns>
        public static int DateDiffMonth(DateTime startDate, DateTime endDate)
        {
            return 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month;
        }

        /// <summary>
        /// Counts the number of month boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MONTH,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of month boundaries crossed between the dates.</returns>
        public static int? DateDiffMonth(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMonth(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of month boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MONTH,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of month boundaries crossed between the dates.</returns>
        public static int DateDiffMonth(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffMonth(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of month boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MONTH,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of month boundaries crossed between the dates.</returns>

        public static int? DateDiffMonth(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMonth(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of day boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(DAY,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of day boundaries crossed between the dates.</returns>
        public static int DateDiffDay(DateTime startDate, DateTime endDate)
        {
            TimeSpan diff = endDate.Date - startDate.Date;
            return diff.Days;
        }

        /// <summary>
        /// Counts the number of day boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(DAY,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of day boundaries crossed between the dates.</returns>
        public static int? DateDiffDay(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffDay(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of day boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(DAY,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of day boundaries crossed between the dates.</returns>
        public static int DateDiffDay(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffDay(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of day boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(DAY,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of day boundaries crossed between the dates.</returns>
        public static int? DateDiffDay(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffDay(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of hour boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(HOUR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of hour boundaries crossed between the dates.</returns>
        public static int DateDiffHour(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffDay(startDate, endDate) * 24 + endDate.Hour - startDate.Hour;
            }
        }

        /// <summary>
        /// Counts the number of hour boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(HOUR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of hour boundaries crossed between the dates.</returns>
        public static int? DateDiffHour(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffHour(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of hour boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(HOUR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of hour boundaries crossed between the dates.</returns>
        public static int DateDiffHour(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffHour(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of hour boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(HOUR,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of hour boundaries crossed between the dates.</returns>
        public static int? DateDiffHour(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffHour(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of minute boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MINUTE,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of minute boundaries crossed between the dates.</returns>
        public static int DateDiffMinute(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffHour(startDate, endDate) * 60 + endDate.Minute - startDate.Minute;
            }
        }

        /// <summary>
        /// Counts the number of minute boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MINUTE,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of minute boundaries crossed between the dates.</returns>
        public static int? DateDiffMinute(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMinute(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of minute boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MINUTE,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of minute boundaries crossed between the dates.</returns>
        public static int DateDiffMinute(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffMinute(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of minute boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MINUTE,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of minute boundaries crossed between the dates.</returns>

        public static int? DateDiffMinute(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMinute(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of second boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(SECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of second boundaries crossed between the dates.</returns>
        public static int DateDiffSecond(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffMinute(startDate, endDate) * 60 + endDate.Second - startDate.Second;
            }
        }

        /// <summary>
        /// Counts the number of second boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(SECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of second boundaries crossed between the dates.</returns>
        public static int? DateDiffSecond(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffSecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of second boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(SECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of second boundaries crossed between the dates.</returns>
        public static int DateDiffSecond(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffSecond(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of second boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(SECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of second boundaries crossed between the dates.</returns>

        public static int? DateDiffSecond(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffSecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of millisecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MILLISECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of millisecond boundaries crossed between the dates.</returns>
        public static int DateDiffMillisecond(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffSecond(startDate, endDate) * 1000 + endDate.Millisecond - startDate.Millisecond;
            }
        }

        /// <summary>
        /// Counts the number of millisecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MILLISECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of millisecond boundaries crossed between the dates.</returns>
        public static int? DateDiffMillisecond(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMillisecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of millisecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MILLISECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of millisecond boundaries crossed between the dates.</returns>
        public static int DateDiffMillisecond(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffMillisecond(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of millisecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MILLISECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of millisecond boundaries crossed between the dates.</returns>

        public static int? DateDiffMillisecond(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMillisecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of microsecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MICROSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of microsecond boundaries crossed between the dates.</returns>
        public static int DateDiffMicrosecond(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return (int)((endDate.Ticks - startDate.Ticks) / 10);
            }
        }

        /// <summary>
        /// Counts the number of microsecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MICROSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of microsecond boundaries crossed between the dates.</returns>
        public static int? DateDiffMicrosecond(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMicrosecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of microsecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MICROSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of microsecond boundaries crossed between the dates.</returns>
        public static int DateDiffMicrosecond(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffMicrosecond(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of microsecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(MICROSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of microsecond boundaries crossed between the dates.</returns>

        public static int? DateDiffMicrosecond(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffMicrosecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of nanosecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(NANOSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of nanosecond boundaries crossed between the dates.</returns>
        public static int DateDiffNanosecond(DateTime startDate, DateTime endDate)
        {
            checked
            {
                return (int)((endDate.Ticks - startDate.Ticks) * 100);
            }
        }

        /// <summary>
        /// Counts the number of nanosecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(NANOSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of nanosecond boundaries crossed between the dates.</returns>
        public static int? DateDiffNanosecond(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffNanosecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts the number of nanosecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(NANOSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of nanosecond boundaries crossed between the dates.</returns>
        public static int DateDiffNanosecond(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return DateDiffNanosecond(startDate.UtcDateTime, endDate.UtcDateTime);
        }

        /// <summary>
        /// Counts the number of nanosecond boundaries crossed between the startDate and endDate.
        /// Corresponds to SQL Server's DATEDIFF(NANOSECOND,startDate,endDate).
        /// </summary>
        /// <param name="startDate">Starting date for the calculation.</param>
        /// <param name="endDate">Ending date for the calculation.</param>
        /// <returns>Number of nanosecond boundaries crossed between the dates.</returns>
        public static int? DateDiffNanosecond(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return DateDiffNanosecond(startDate.Value, endDate.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This function is translated to Sql Server's LIKE function.
        /// It cannot be used on the client.
        /// </summary>
        /// <param name="match_expression">The string that is to be matched.</param>
        /// <param name="pattern">The pattern which may involve wildcards %,_,[,],^.</param>
        /// <returns>true if there is a match.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "pattern", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "matchExpression", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "IDE0060:Remove unused parameter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        public static bool Like(string matchExpression, string pattern)
        {
            throw SqlMethodOnlyForSql(MethodBase.GetCurrentMethod());
        }

        /// <summary>
        /// This function is translated to Sql Server's LIKE function.
        /// It cannot be used on the client.
        /// </summary>
        /// <param name="match_expression">The string that is to be matched.</param>
        /// <param name="pattern">The pattern which may involve wildcards %,_,[,],^.</param>
        /// <param name="escape_character">The escape character to use in front of %,_,[,],^ if they are not used as wildcards.</param>
        /// <returns>true if there is a match.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "pattern", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "matchExpression", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "escapeCharacter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "IDE0060:Remove unused parameter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        public static bool Like(string matchExpression, string pattern, char escapeCharacter)
        {
            throw SqlMethodOnlyForSql(MethodBase.GetCurrentMethod());
        }

        /// <summary>
        /// This function is translated to Sql Server's DATALENGTH function.  It differs
        /// from LEN in that it includes trailing spaces and will count UNICODE characters
        /// per byte.
        /// It cannot be used on the client.
        /// </summary>
        /// <param name="value">The string to take the length of.</param>
        /// <returns>length of the string</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "IDE0060:Remove unused parameter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        internal static int RawLength(string value)
        {
            throw SqlMethodOnlyForSql(MethodBase.GetCurrentMethod());
        }

        /// <summary>
        /// This function is translated to Sql Server's DATALENGTH function.
        /// It cannot be used on the client.
        /// </summary>
        /// <param name="value">The byte array to take the length of.</param>
        /// <returns>length of the array</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "IDE0060:Remove unused parameter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        internal static int RawLength(byte[] value)
        {
            throw SqlMethodOnlyForSql(MethodBase.GetCurrentMethod());
        }

        /// <summary>
        /// This function is translated to Sql Server's DATALENGTH function.
        /// It cannot be used on the client.
        /// </summary>
        /// <param name="value">The Binary value to take the length of.</param>
        /// <returns>length of the Binary</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        [SuppressMessage("Microsoft.Usage", "IDE0060:Remove unused parameter", Justification = "Microsoft: Method is a placeholder for a server-side method.")]
        internal static int RawLength(Binary value)
        {
            throw SqlMethodOnlyForSql(MethodBase.GetCurrentMethod());
        }

        internal static Exception SqlMethodOnlyForSql(MethodBase method)
        {
            return new NotSupportedException(string.Format(CultureInfo.CurrentCulture, SubSonicErrorMessages.SqlMethodOnlyForSql, $"{method.Name}"));
        }
    }
}
