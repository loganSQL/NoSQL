# MongoDB
## Installation
```
## check OS version
wmic os get osarchitecture
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
docker ps
docker log firstmongo
docker top firstmongo
docker inspect firstmongo|findstr 'IPAddress'

## connect from mongo shell
set-alias mg C:\logan\bin\mongo.exe
mg --host 172.23.23.21:27017

## connect to container shell
docker exec -it firstmongo cmd
dir
wmic os get osarchitecture
mongo
```