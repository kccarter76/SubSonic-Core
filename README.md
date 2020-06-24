# SubSonic-Core ![DotNET](https://github.com/kccarter76/SubSonic-Core/workflows/DotNET/badge.svg?branch=master)
The driving force behind this project is the various issues with the current EF DAL available for .net core 2.2 during 2019.
various issues in which prevented minimal manipulation of the data to release projects.
This way I know the DAL from the ground up and speak for its performance and implemenation. 

# Why should you switch to SubSonic
<ul>
   <li>User-Defined table types (UDTT) are performant, used in stored procedures, insert, update and delete database calls reduces the number of parameters needed to pass to the database. This has reduced the time taken to generate an insert statement of three records with three columns of data by 75%. That is a significant savings in computer time.</li>
   <li>User-Defined table types, can define a data contract between the database and your application.</li>
</ul>

# Supported Frameworks
<ul>
   <li>.NETStandard,Version=v2.0</li>
   <li>.NETStandard,Version=v2.1</li>
</ul>

# Supported Database Servers
<ul>
   <li>Sql Server >= 2012</li>
</ul>

# Features
<ul>
   <li>Database Context</li>
   <li>Stored Procedure Support</li>
   <li>Scalar Function Mapping</li>
   <li>Db Model mapping of Table and User Defined Data Type</li>
   <li>Command Query Responsibility Segregation (CQRS) approach to database operations against a DB Model</li>
   <li>Linq Query mapping supports</li>
   <ul>
      <li>WHERE [COLUMN] (NOT) IN ([QUERY] | [VALUE, VALUE])</li>
      <li>WHERE [COLUMN] [OPERATOR] [VALUE] (AND | OR) ...</li>
      <li>WHERE (NOT) EXISTS ([QUERY])</li>
      <li>WHERE [DATE VALUE | COLUMN] (NOT) BETWEEN [DATE VALUE] AND [DATE VALUE]</li>
   </ul>
   <li>Support for paging large datasets</li>
   <li>Supports OrderBy, OrderByDescending, ThenBy, ThenByDescending</li>
   <li>Data Caching</li>
   <li>Proxy Support</li>
   <ul>
      <li>overrides virtual navigation and collection properties.</li>
      <li>when navigation property value changes proxy changes the foreign key property to match.</li>
      <li>proxy implments IEntityProxy<TEntity> which implements Data, KeyData, ModelType, IsDirty, IsNew, IsDeleted</li>
</ul> 

# Project Goals
<ol>
   <li>minimize the references to 3rd party projects which use namespaces that can come into conflict with the .net core library.</li>
   <ol>
      <li>that being said extensions are easy to override, just remember to forward the call if it does not apply.</li>
   </ol>
   <li>expand understanding of the expression tree implemenation, expressions really are a rosetta stone for developers.</li>
   <li>keep the SQL statements generated by the DAL as simple as possible.</li>
   <li>utilize MARS statements when eagerloading data for an object graph.</li>
   <li>implement asynchonous operations at some point.</li>
   <li>support SQL Server, Oracle, MySql, etc. Database engines.</li>
   <li>apply TDD, DRY, YAGNI, and KISS principles where applicable.</li>
</ol>
<br />

[![Crypto Tip Jar](/images/tipjar.png)](https://commerce.coinbase.com/checkout/e234bf33-6611-496f-b816-685fe0dedb66)

# Reference Material
* https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder
* https://www.databasejournal.com/features/mssql/article.php/3766391/SqlCredit-Part-18-Exploring-the-Performance-of-SQL-2005146s-OUTPUT-Clause.htm
* https://sqlperformance.com/2015/01/t-sql-queries/pagination-with-offset-fetch

# Reference Projects
* [MockDbProvider](https://github.com/abeven/MockDbProvider)
* [SubSonic 3.0](https://github.com/subsonic/SubSonic-3.0)
