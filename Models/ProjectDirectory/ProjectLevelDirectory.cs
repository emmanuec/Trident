using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Trident.Models.ProjectDirectory
{
    public class ProjectLevelDirectory
    {
        public string ProjectDirectoryName { get; }
        public List<LobLeveLDirectory> LobLevelDir{ get; }

        public ProjectLevelDirectory(string directory)
        {
            ProjectDirectoryName = directory;
            SetDirectoryList();
        }

        private void SetDirectoryList()
        {
            if(Directory.Exists(ProjectDirectoryName))
            {
                foreach(var directory in Directory.GetDirectories(ProjectDirectoryName))
                {
                    LobLevelDir.Add(new LobLeveLDirectory(directory));
                }
            }
        }
    }
}