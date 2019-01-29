using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamMngtWS.MemoryCache;

namespace TeamMngtWS
{
    public static class ExtensionMethods
    {
        public static string ToJSON(this Team team)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"{{\"name\": \"{team.Name}\", ");

            result.Append($"\"members\": [");

            foreach (TeamMember member in team.GetMembers())
            {
                result.AppendFormat("{{\"{0}\": \"{1}\"}},", nameof(TeamMember.Name), member.Name);
            }

            // remove the last comma
            if(result[result.Length - 1]==',')
                result.Remove(result.Length - 1, 1);

            result.Append("]}");

            return result.ToString();
        }
    }
}
