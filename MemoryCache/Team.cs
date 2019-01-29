using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.MemoryCache
{
    /// <summary>
    /// Representing a team. A team is composed name and a dictionary of members 
    /// </summary>
    public class Team
    {
        private Dictionary<string, TeamMember> teamMembers = new Dictionary<string, TeamMember>();

        public string Name { get; }

        public TeamMember this[string name]
        {
            get { return teamMembers[name]; }

            set { teamMembers[name] = value; }
        }

        public int Count
        {
            get { return teamMembers.Count; }
        }

        public Team(string name)
        {
            Name = name;
        }

        public List<TeamMember> GetMembers()
        {
            return teamMembers.Values.ToList();
        }

        public override string ToString()
        {
            return this.ToJSON();
        }
    }
}
