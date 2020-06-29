![DotNET](https://github.com/kccarter76/SubSonic-Core/workflows/DotNET/badge.svg?branch=master)
![Downloads](https://img.shields.io/nuget/dt/SubSonic.Core)

# SubSonic-Core ![Nuget](https://img.shields.io/nuget/v/SubSonic.Core) 
<!---![Nuget](https://img.shields.io/nuget/vpre/SubSonic.Core)--->
Fast Data Access, your database should belong to SubSonic.

# SubSonic.Extensions.SqlServer ![Nuget](https://img.shields.io/nuget/v/SubSonic.Extensions.SqlServer)
SqlServer Db Client and Factory

# SubSonic.Extensions.Test ![Nuget](https://img.shields.io/nuget/v/SubSonic.Extensions.Test)
MockSubSonic Db Client and Factory, primary purpose is for unit testing and faking the database server.

# Why should you switch to SubSonic
<ul>
   <li>User-Defined table types (UDTT) are performant, used in stored procedures, insert, update and delete database calls reduces the number of parameters needed to pass to the database. This has reduced the time taken to generate an insert statement of three records with three columns of data by 75%. That is a significant savings in computer time.</li>
   <li>User-Defined table types, can define a data contract between the database and your application.</li>
   <li>Insert and Update queries make use of change data capture(CDC) this allows Sub Sonic to retrieve data generated by the database. I.E: identity and computed columns</li>
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
   <li>Insert and Update queries use Change Data Capture (CDC) to extract database generated data back to the client.
   <ul>
      <li>It is not required that a User-Defined Table Type should exist, but it is performant to use one.</li>
      <li>The insert DML is executed syncrounously, and SQL optimizer will not use parallelization.</li>
      <li>The update DML is executed syncrounously, and SQL optimizer will use parallelization.</li>
      </ul></li>
   <li>Proxy Support
   <ul>
      <li>overrides virtual navigation and collection properties.</li>
      <li>when navigation property value changes proxy changes the foreign key property to match.</li>
      <li>proxy implments IEntityProxy<TEntity> which implements Data, KeyData, ModelType, IsDirty, IsNew, IsDeleted</li>
   </ul></li>
</ul> 
<br />

[![Crypto Tip Jar](/images/tipjar.png)](https://commerce.coinbase.com/checkout/e234bf33-6611-496f-b816-685fe0dedb66)
