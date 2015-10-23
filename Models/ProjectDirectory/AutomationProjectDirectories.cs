using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Trident.Models.ProjectDirectory
{
    public class AutomationProjectDirectories
    {
        private static readonly string AutomationFolderRelativePath = @"C:\Automation\Projects\Tellurium\projects";

        public List<ProjectLevelDirectory> ProjectLevelDir { get; }

        public AutomationProjectDirectories()
        {
            SetDirectoryList();
        }

        private void SetDirectoryList()
        {
            if(Directory.Exists(AutomationFolderRelativePath))
            {
                foreach(var directory in Directory.GetDirectories(AutomationFolderRelativePath))
                {
                    ProjectLevelDir.Add(new ProjectLevelDirectory(directory));
                }
            }
        }
    }
}