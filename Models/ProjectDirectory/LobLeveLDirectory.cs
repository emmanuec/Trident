using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Trident.Models.ProjectDirectory
{
    public class LobLeveLDirectory
    {
        public string LobDirectoryName { get; }
        public List<string> LobDirectories { get; }

        public LobLeveLDirectory(string directory)
        {
            LobDirectoryName = directory;
            SetDirectoryList();
        }

        private void SetDirectoryList()
        {
            if (Directory.Exists(LobDirectoryName))
            {
                foreach (var directory in Directory.GetDirectories(LobDirectoryName))
                {
                    LobDirectories.Add(directory);
                }
            }
        }
    }
}