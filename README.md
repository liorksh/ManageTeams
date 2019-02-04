# ManageTeams

This C# .NET CORE API exposes two services: root and Team

## Team API 
This service manages teams with the basic operations of creating a team, adding a member to a team, and review the teams' members. 
The API commands are:

1. /Team/{name} (GET) - returns a team's details
2. /Team/ShowCache (GET) - returns all teams and their members in JSON format
3. /Team?team={TeamName}&name={MemberName} (POST) - adds a member to a team. if the team does not exists then the system creates it
