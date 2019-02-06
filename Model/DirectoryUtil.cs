using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.Model
{
    public class DirectoryUtil
    {
        public static List<FileInfo> GetAllFileUnderDirectory(string root)
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo info = new DirectoryInfo(root);
            files.AddRange(info.GetFiles());

            foreach (DirectoryInfo directory in info.GetDirectories())
            {
                files.AddRange(GetAllFileUnderDirectory(directory.FullName));
            }

            return files;
        }

        public static List<DirectoryInfo> GetAllDirecrotiesUnderDirectory(string root)
        {
            List<DirectoryInfo> directories = new List<DirectoryInfo>();

            DirectoryInfo info = new DirectoryInfo(root);
            directories.AddRange(info.GetDirectories());

            foreach (DirectoryInfo directory in info.GetDirectories())
            {
                directories.AddRange(GetAllDirecrotiesUnderDirectory(directory.FullName));
            }

            return directories;
        }
    }
}
