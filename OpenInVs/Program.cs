using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenInVs
{
    class Program
    {
        private enum ProjectFileSearchOption
        {
            All,
            SlnOnly,
            CsprojOnly
        }

        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                string vsPath = args[0];
                string projectPath = args[1];
                ProjectFileSearchOption projectFileSearchOption = ProjectFileSearchOption.All;
                if (args.Length == 3)
                {
                    string searchOptionString = args[2];
                    switch (searchOptionString)
                    {
                        case "SlnOnly":
                            projectFileSearchOption = ProjectFileSearchOption.SlnOnly;
                            break;
                        case "CsprojOnly":
                            projectFileSearchOption = ProjectFileSearchOption.CsprojOnly;
                            break;
                        default:
                            projectFileSearchOption = ProjectFileSearchOption.All;
                            break;
                    }
                }
                var filePathList = FindProjectFile(projectPath, projectFileSearchOption);
                if (filePathList.Count == 1)
                {
                    Open(vsPath, filePathList[0]);
                }
                else
                {
                    GenerateOption(filePathList, projectPath);
                    string indexString = Console.ReadLine();
                    int.TryParse(indexString, out var index);
                    if (index == -1)
                    {
                    }
                    else if (index > 0 && index <= filePathList.Count)
                    {
                        Open(vsPath, filePathList[index - 1]);
                    }
                    else
                    {
                        Console.WriteLine("请输入正确的序号！");
                    }
                }
            }
            else
            {
                Console.WriteLine("请填写正确的参数！\n* 第一个参数为VS路径\n* 第二个参数为项目路径\n* 第三个参数为搜索选项，可选值为All/SlnOnly/CsprojOnly");
            }
        }

        private static void Open(string vsPath, string projectPath)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = vsPath,
                Arguments = projectPath,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            };
            System.Diagnostics.Process process = new System.Diagnostics.Process { StartInfo = startInfo };
            process.Start();
        }

        private static List<string> FindProjectFile(string projectPath, ProjectFileSearchOption projectFileSearchOption)
        {
            var fileList = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(projectPath);
            if (projectFileSearchOption != ProjectFileSearchOption.CsprojOnly)
            {
                var slnFileInfos = directoryInfo.GetFiles("*.sln", SearchOption.AllDirectories);
                fileList.AddRange(slnFileInfos.Select(slnFileInfo => slnFileInfo.FullName));
            }
            if (projectFileSearchOption != ProjectFileSearchOption.SlnOnly)
            {
                var csprojFileInfos = directoryInfo.GetFiles("*.csproj", SearchOption.AllDirectories);
                fileList.AddRange(csprojFileInfos.Select(slnFileInfo => slnFileInfo.FullName));
            }
            return fileList;
        }

        private static void GenerateOption(List<string> filePathList, string projectPath)
        {
            if (filePathList.Count == 0)
            {
                Console.WriteLine("未找到指定文件！");
            }
            else
            {
                Console.WriteLine("请输入序号打开指定解决方案或工程：");
                int index = 0;
                foreach (var filePath in filePathList)
                {
                    Console.WriteLine($"{++index}: {filePath.Replace("\\\\", "\\").Replace(projectPath, string.Empty).Substring(1)}");
                }
            }
        }
    }
}
