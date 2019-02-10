# Manage Teams - C# .NET Core API example

This C# .NET CORE API exposes two services: Team and Info.

## Host parameters
* --port: the hosting port of the service
* --url: the url of the hosting service. This parameter supersedes the "port" parameter 
* --logpath: setting the local path of the log file
* --logfilename: specifying the name pf the log file

## The API

### Info API
This API exposes some information about the running host. Its path is the root.

* /About - returns the running process's path and ID.
* /ListDir - returns all the directories underneath the running directory
* /ListFiles - returns all the files underneath the running directory
* /ReadLog/{rows} - return the log messages of the first {rows} rows

### Team API 
This service manages teams with the basic operations of creating a team, adding a member to a team, and review the teams' members. The teams data is stored in the service's memory in an object named MemRepository (see below)
The API commands are:

* /Team/{name} (GET) - returns a team's details
* /Team/ShowCache (GET) - returns all teams and their members in JSON format
* /Team?team={TeamName}&name={MemberName} (POST) - adds a member to a team. if the team does not exists then the system creates it

## Cache objects

The memory cached is composed of the following items:
* MemRepository
* CacheItem
* Team
* TeamMember
