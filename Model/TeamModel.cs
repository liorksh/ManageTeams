using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMngtWS.MemoryCache;

namespace TeamMngtWS.Model
{
    public static class TeamModel
    {
        public static void AddMemberToTeam(string team, string name)
        {
            CacheItem<Team> itemTeam = (MemRepository<Team>.Repository.ContainsKey(team) == true) ? MemRepository<Team>.Repository[team] : new CacheItem<Team>() { Value = new Team(team) };

            // add the member to the team
            itemTeam.Value[name] = new TeamMember() { Name = name };

            // save the team
            MemRepository<Team>.Repository[itemTeam.Value.Name] = itemTeam;
        }

        internal static string PrintCache()
        {
            return MemRepository<Team>.Print();
        }
    }
}
