using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Trident.Helper
{
    public static class DebugFileHelper
    {
        private static readonly string AutomationFolderBasePath = @"http://localhost/";
        private static readonly string AutomationProjectFile = "tellurium.project";
        private static readonly string LogRelativePath = @"/logs/rolling.txt";
        private static readonly string ExcelRelativePath = @"/results/";
        private static readonly string ScreenshotRelativePath = @"/screenshots/";

        // Directory does not exists
        // Directory exists
        // File does not exists
        // File exists
        public static string GetLogPath(String machineName, String projectName)
        {
            //TODO: need to remove below line later
            machineName = "VMTEST01";
            var projectDir = GetProjectDirectory(machineName, projectName);
            var logPath = projectDir + LogRelativePath;

            return "";
        }

        public static string GetExcelPath(String machineName, String projectName)
        {
            var projectDir = GetProjectDirectory(machineName, projectName);
            var excelPath = projectDir + ExcelRelativePath + ".xls";

            return "";
        }

        public static List<string> GetScreenshotPaths(String machineName, String projectName)
        {
            var projectDir = GetProjectDirectory(machineName, projectName);
            var excelPath = projectDir + ScreenshotRelativePath + ".png";

            return new List<string>() { "" } ;
        }

        private static string GetProjectDirectory(String machineName, String projectName)
        {
            /*  var projectBaseDirectory = AutomationFolderBasePath + machineName;
              var files = Directory.GetFiles(AutomationFolderBasePath, AutomationProjectFile, SearchOption.AllDirectories);
              var patternToMatch = @"(ProjectName\s+\=(\s+)?)(" + projectName + ")";
              var projectDir = "";

              foreach (var file in files)
              {
                  var fileContent = File.ReadAllText(file);
                  if (Regex.IsMatch(fileContent, patternToMatch))
                  {
                      projectDir = Path.GetDirectoryName(file);
                      break;
                  }
              }
              return projectDir;*/

            return "";
        }
    }
}