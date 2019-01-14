# Azure COSMOS DB

Azure Cosmos DB is a globally distributed, multi-model database service that supports document, key-value, wide-column, and graph databases. 

* [Azure Cosmos DB Documentation](<https://docs.microsoft.com/en-us/azure/cosmos-db/>)
* [azure-cosmos-dotnet-v2](https://github.com/Azure/azure-cosmos-dotnet-v2)

* [A DBA's Guide to Azure Cosmos DB (SQL Saturday Oslo 2018)](<https://www.slideshare.net/BobPusateri1/select-stars-a-dbas-guide-to-azure-cosmos-db-sql-saturday-oslo-2018>)

## 1. Resource Model

![COSMOS DB Resource Model](https://static.packt-cdn.com/products/9781789612899/graphics/ac6cfc62-c222-413b-b1ac-7e1edba7c08d.png)
**COSMOS DB Resource Model**

A tenant of the Cosmos DB service starts by provisioning a database account. A database account manages one or more databases. A Cosmos DB database manages users, permissions and containers. A Cosmos DB resource container is a schema-agnostic container of arbitrary user-generated JSON items and JavaScript based stored procedures, triggers and user-defined-functions (UDFs). Entities under the tenant’s database account – databases, users, permissions, containers etc. are referred to as resources. 

Each resource is uniquely identified by a stable and logical URI and represented as a JSON document. The overall resource model of an application using Cosmos DB is a hierarchical overlay of the resources rooted under the database account, and can be navigated using hyperlinks. Except for the item resource, which is used to represent arbitrary user defined JSON content, all other resources have a system-defined schema.

Container and item resources are further projected as reified resource types for a specific type of API interface. For example, while using document-oriented APIs, container and item resources are projected as collection (container) and document (item) resources, respectively; likewise, for graph-oriented API access, the underlying container and item resources are projected as graph (container), node (item) and edge (item) resources respectively; while accessing using a key-value API table (container) and item/row (item) are projected. 

## [2 Getting Started With CosmosDB on Azure](<https://www.c-sharpcorner.com/article/getting-started-with-consmosdb2/>)

DocumentDB is a NoSQL database which is massively scalable and it works with schema-free JSON documents.

## 2.1 Features of CosmosDB

* Schema-free JSON
  * Documents are stored in the form of JSON
  * Documents are easily queryable using SQL and number of APIs
* ACID transactions
  * Allows multi-document transaction processing
* Tunable Performance (many ways ti improve the performance)
  * Throughput: Can easily scale throughput (request units processing per second)
  * Indexing: Can customize indexing policy as per requirement
  * Consistency: Supports multiple consistency policies.
  * Runs on Azure
    * Fully managed PaaS and massively scalable service
  * Multiple data models and API for accessing and querying the data

## 2.2 Multi-model API for Query Data
* [DocumentDB API](<https://docs.microsoft.com/en-us/azure/cosmos-db/introduction>): A schema-less JSON database engine with SQL querying capabilities.
* [MongoDB API](<https://docs.microsoft.com/en-us/azure/cosmos-db/mongodb-introduction>): A MongoDB database service built on top of Cosmos DB.
* [Table API](<https://docs.microsoft.com/en-us/azure/cosmos-db/table-introduction>): A key-value database service built to provide premium capabilities for Azure Table storage applications.
* [Graph (Gremlin) API](<https://docs.microsoft.com/en-us/azure/cosmos-db/graph-introduction>): A graph database service built following the Apache TinkerPop specification.
* [Cassandra API](<https://docs.microsoft.com/en-us/azure/cosmos-db/cassandra-introduction>): A key/value store built on the Apache Cassandra

## 2.3. DocumentDB Resource Model
![DocumentDB Resource Model](https://csharpcorner-mindcrackerinc.netdna-ssl.com/article/temp/62393/Images/image001.png)
**DocumentDB Resource Model**

* Database Account
  * A database account is associated with a set of databases
* Database
  * Database is logic container of documents
  * Database can have users associated with it
  * Users can have permissions for accessing the database
* Collections
  * Collection is container of JSON documents
  * You can write Stored Procedures, Triggers and User-defined functions on all documents within the collection.
  * We use Javascript to write Stored Procedures, Triggers, and User-defined functions.
* Document
  * User-defined JSON content. No schema needs to be defined.
  * A document can have attachments containing references and associated metadata for external blob/media like videos, images etc.

## 2.4 Azure Steps
* Create a database account (Azure Portal->More service->Azure Cosmos DB)
  * database account (ID), API (SQL), Subscriptiion, Resource Group, Location
* Creating new Collection (Data Explorer->New Collection)
  * Database id(dbFamily), collection id(Families), Storage capacity, Throughput
* Add sample data into collection (Documents->collection (dbFamily->Families-Documents)
  * New document  (any JSON data) -> Save and Observe the data
  * System generated properties (Once you save the data, you can observe some system generated properties, prefixed with underscore)

# [3. Use the Azure Cosmos DB Emulator for local development and testing](<https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator>)

## Running on Docker
* Prerequisites: Docker for Windows (Switch to Windows containers)
* [Pull the emulator image from Docker Hub](<https://hub.docker.com/r/microsoft/azure-cosmosdb-emulator/>)
```
docker pull microsoft/azure-cosmosdb-emulator
```
* Start the image in cmd
```
set containerName=azure-cosmosdb-emulator
set hostDirectory=%LOCALAPPDATA%\azure-cosmosdb-emulator.hostd
md %hostDirectory% 2>nul
docker run --name %containerName% --memory 2GB --mount "type=bind,source=%hostDirectory%,destination=C:\CosmosDB.Emulator\bind-mount" -P --interactive --tty microsoft/azure-cosmosdb-emulator
```
* Get the **endpoint** and **master key-in** from the response 
```
Transcript started, output file is C:\CosmosDB.Emulator\bind-mount\Diagnostics\Transcript.log
Key   : Emulator
Value : CosmosDB.Emulator
Name  : Emulator


Key   : Version
Value : 2.1.3.0
Name  : Version


Key   : Key
Value : C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
Name  : Key


Key   : IPAddress
Value : 172.23.28.146
Name  : IPAddress


Key   : MongoDBEndpoint
Value : {mongodb://73cc3aefa437:10255/, mongodb://172.23.28.146:10255/}
Name  : MongoDBEndpoint


Key   : Endpoint
Value : {https://73cc3aefa437:8081/, https://172.23.28.146:8081/}
Name  : Endpoint


Key   : CassandraEndpoint
Value : {tcp://73cc3aefa437:10350/, tcp://172.23.28.146:10350/}
Name  : CassandraEndpoint
```
* To import the SSL certificate, do the following from an admin cmd
```
cd %LOCALAPPDATA%\azure-cosmosdb-emulator.hostd
dir
powershell .\importcert.ps1
```
* To open the Data Explorer, navigate to the following URL in your browser.
```
https://172.23.28.146:8081/_explorer/index.html
```
## 4. Maintenance
```
# List of container
docker ps -a

# Start
docker start azure-cosmosdb-emulator

# To import the SSL certificate after restart, do the following from an admin cmd
cd %LOCALAPPDATA%\azure-cosmosdb-emulator.hostd
dir
powershell .\importcert.ps1

# Get all information (IP)
docker inspect azure-cosmosdb-emulator
docker inspect azure-cosmosdb-emulator|findstr 'IPAddress'

# Browser to 
https://YOUR_CONTAINER_IP:8081/_explorer/index.html
```

