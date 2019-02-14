# MongoDB
## MongoDB On Windows
```
1. Download from http://www.mongodb.org/downloads
2. Install .msi file in folder C:\logan\bin\mongodb4
    ## Powershell env
    $env:Path = $env:Path+";C:\logan\bin\mongodb4\bin"
3. create data path: mkdir C:\logan\data\MongoDB4\data
4. create log path: mkdir C:\logan\data\MongoDB4\log
5. create test script dir: mkdri C:\logan\test\mongodb4
6. create a config file:

    copy C:\logan\bin\mongodb4\mongod.cfg C:\logan\test\mongodb4\mongod.cfg
    
    ## Add the following lines in "mongo.config" file
    ## logpath is a file (Not PATH)
    port=27017
    dbpath=C:\logan\data\MongoDB4\data
    logpath=C:\logan\data\MongoDB4\log\mongod.log

7. Start server interactively:

    mongod.exe --config="C:\logan\test\mongodb4\mongod.cfg"
    
    mongod --port 27017 --dbpath "C:\logan\data\MongoDB4\data" --logpath="C:\logan\data\MongoDB4\log\mongod.log"

8. MongoDB as a service on Windows (Run As Administrator)

    # cmd.exe as admin to delete the service if mongo.cfg change
    # 
    C:\WINDOWS\system32>sc delete MongoDB

    # Install as a service
    mongod --config="C:\logan\test\mongodb4\mongod.cfg" --install --serviceName "MongoDB"
    
    # check service
    Get-Service|findstr MongoDB
    
    # Start Service

    net start MongoDB
    
    # Stop Service

    net stop MongoDB

9. Connect to localhost MongoDB server via command line

    mongo --port 27017

```
### [Connect to your remote MongoDB server](https://ianlondon.github.io/blog/mongodb-auth/)
1. [Enable Auth](<https://docs.mongodb.com/manual/tutorial/enable-authentication/>)
```
use admin
db.createUser(
  {
    user: "loganAdmin",
    pwd: "abc123",
    roles: [ { role: "userAdminAnyDatabase", db: "admin" }, "readWriteAnyDatabase" ]
  }
)
```
2. Change Mongo.cfg
```
# 2.1. Comment out bindIP
# network interfaces
  net:
  port: 27017
  #  bindIp: 127.0.0.1  <- comment out this line
  # Or bind another IP server_ip_from_dns
  bindIp: 127.0.0.1,172.16.40.84
  
# 2.2. Uncomment out Security
#security:
security:
   authorization: 'enable'
```
```
# Need to re-create service at step 8 above
# After started, connect from local host
mongo -u "loganAdmin" -p "abc123" --authenticationDatabase "admin" -norc
```
3. Connect from another host
```
    mongo -u loganAdmin -p abc123 -host myhost.mydomain -norc
```
```
PS C:\logan\bin> mongo -u "loganAdmin" -p "abc123" -norc 172.16.40.84/admin
MongoDB shell version v4.0.5
connecting to: mongodb://172.16.40.84:27017/admin?gssapiServiceName=mongodb
Implicit session: session { "id" : UUID("c66a7dbc-de87-47d0-af71-938cfcf15953") }
MongoDB server version: 4.0.6
>
```

4. Add more users
```
use test
db.createUser(
  {
    user: "loganTest",
    pwd: "abc123",
    roles: [ { role: "readWrite", db: "test" },
             { role: "read", db: "reporting" } ]
  }
)

show users

db.dropUser("loganTest")
```
```
mongo -u "loganTest" -p "abc123" --authenticationDatabase "test" -norc
```

```
PS C:\logan\test\mongodb4> mongo -norc
MongoDB shell version v4.0.6
connecting to: mongodb://127.0.0.1:27017/?gssapiServiceName=mongodb
Implicit session: session { "id" : UUID("c88c6b3e-2350-4b3d-b0c4-f2d3f4537b22") }
MongoDB server version: 4.0.6
> db
test
> db.auth("loganTest","abc123")
1
>
```

## [mongoDB.Atlas](https://cloud.mongodb.com/user#/atlas/login)
```
1. Build your first cluster(SANDBOX): logan416
2. Create Database User: Security=>Add New User=>loganuser
3. Whitelist IP address: IP Whitelist=>Add IP Address=>Allow access from anywhere
4. connect to cluster: Sandbox=>connect (Connect to Logan416)
5. Choose a connection method:
    Connect with the Mongo Shell
    Connect You Application
    Connect with MongoDB Compass
l.c@f.com @1y
```

## [MongoDB Docker Image](<https://hub.docker.com/_/mongo>)
```

## Pull image from Docker Hub
docker pull mongo

## Start a mongo server instance
docker run --name firstmongo -d mongo:latest
docker start firstmongo
docker ps
docker logs firstmongo
docker top firstmongo
docker inspect firstmongo|findstr 'IPAddress'

## connect from mongo shell
## https://docs.mongodb.com/manual/mongo/
set-alias mongo C:\logan\bin\mongo.exe
$h=$(docker ps -q)
mongo --host 172.23.23.21:27017
mongo --host $(docker ps -q)
mongo --host $h

# Default Port: By default MongoDB is listening to port 27017.
mongo <HOST>
mongo <HOST>:<PORT>
mongo <HOST>:<PORT>/<DB>

# https://medium.com/mongoaudit/how-to-enable-authentication-on-mongodb-b9e8a924efac
# https://www.mkyong.com/mongodb/mongodb-allow-remote-access/
# https://docs.mongodb.com/manual/administration/configuration/
mongo -u <USER> -p <PASSWORD> <HOST>:<PORT> --authenticationDatabase <AUTH_DB>
mongo -u <USER> -p <PASSWORD> <HOST>:<PORT>/<DB> --authenticationDatabase <AUTH_DB>



## connect to container shell
docker exec -it firstmongo cmd
dir
wmic os get osarchitecture
mongo
```
## [Mongo Shell](<https://docs.mongodb.com/manual/mongo/>)
```
# 1. database
## display current db
db

## switch db
use logandb

## list all dbs
show dbs

# 2. Collection (db -> current, collection -> TestCollection
show collections

db.names.insert({ "name": "Alex" })
db.names.insert({ "name": "Logan" })
db.names.insert({ "name": "Alice" })
db.names.insert({ "name": "Timothy" })
db.names.find().toArray()
db.names.find().pretty()

// ascending
db.names.find().sort({name:1})
// descending
db.names.find().sort({name:-1})


// update
db.names.insert({ "name": "Timothy", "Phone": "647 225-5483" })
db.names.update({"name":"Logan"},{$set:{"Phone":"416 888-4347"}})
db.names.update({"Phone":"647 225-5483"},{$set:{"name" : "Tom"}})

// find docs without a field
db.names.find({"Phone":{"$exists":false}})

// update those without phones (but only update the first doc)
db.names.update({"Phone":{"$exists":false}},{$set:{"Phone":"1800 888-8888"}})

// update multi docs
db.names.update({"Phone":{"$exists":false}},{$set:{"Phone":"1800 888-8888"}},{multi:true})

// delete
db.names.deleteMany({"Phone":"1800 888-8888"})

// drop collection
db.names.drop()

# 3. Customize the Prompt
## Customize Prompt to Display Number of Operations
cmdCount = 1;
prompt = function() {
             return (cmdCount++) + "> ";
         }

## Customize Prompt to Display Database and Hostname
host = db.serverStatus().host;

prompt = function() {
             return db+"@"+host+"$ ";
         }

## Customize Prompt to Display Up Time and Document Count
prompt = function() {
           return "Uptime:"+db.serverStatus().uptime+" Documents:"+db.stats().objects+" > ";
         }

# 4. Help
## 4.1. Database Help
show dbs
## all methods for db object
db.help()
## see the implementation of a method
db.updateUser

## 4.2. Collection Help
show collection
db.YOURCOLLECTION.help()
db.YOURCOLLECTION.METHOD

## 4.3. Cursor Help
db.YOURCOLLECTION.find().help()
db.YOURCOLLECTION.find().toArray

hasNext() which checks whether the cursor has more documents to return.
next() which returns the next document and advances the cursor position forward by one.
forEach(<function>) which iterates the whole cursor and applies the <function> to each document returned by the cursor. The <function> expects a single argument which corresponds to the document from each iteration.

## 4.4. Wrapper Object Help
help misc
```

## [Mongo Shell Script Basic CRUD Operation](https://blog.kevinchisholm.com/javascript/mongodb/getting-started-with-mongo-shell-scripting-basic-crud-operations/)
<https://github.com/kevinchisholm/mongo-shell-scripting-basic-crud-operations>

<https://docs.mongodb.com/manual/crud/>

## [MongoDB in .Net](<https://www.codementor.io/pmbanugo/working-with-mongodb-in-net-1-basics-g4frivcvz>)

