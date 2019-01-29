using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.MemoryCache
{
    /// <summary>
    /// Represents a team member
    /// </summary>
    public class TeamMember
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        // Additional features can be added here.
    }
}
