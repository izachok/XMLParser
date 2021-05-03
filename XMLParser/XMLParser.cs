using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XMLParser
{
    class XMLParser
    {
        private string dirPath = "";
        public string DirectoryPath
        {
            get { return dirPath; }
            set { if (value.EndsWith("\\"))
                {
                    dirPath = value.Substring(0, value.Length - 1);
                }
                else
                {
                    dirPath = value;
                }
            }
        }
        public string ParentNodeName { get; set; }
        public bool IncludeParentNode { get; set; }
        public string OutputFileName { get; set; }

        private StringBuilder resultXml = new StringBuilder();

        public XMLParser(string directoryPath, string parentNodeName, bool includeParentNode, string outputFileName)
        {
            DirectoryPath = directoryPath;
            ParentNodeName = parentNodeName;
            IncludeParentNode = includeParentNode;
            OutputFileName = outputFileName;
        }

        public void ProcessDirectories()
        {
            List<string> directories = new List<string>(Directory.EnumerateDirectories(DirectoryPath));
            //search in root directory
            ProcessFilesInDirectory(DirectoryPath);
            //search in 1st level subdirectories
            foreach (var directory in directories)
            {
                ProcessFilesInDirectory(directory);
            }
        }

        public void ProcessFilesInDirectory(string directoryPath)
        {
            //StringBuilder resultXML = new StringBuilder();
            
            try
            {
                var xmlFiles = Directory.EnumerateFiles(directoryPath, "*.xml");

                foreach (string currentFile in xmlFiles)
                {
                    resultXml.Append(FindXMLNode(currentFile).Trim()).Append("\n");
                }
                SaveXMLToFile(resultXml.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string FindXMLNode(string fullPath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            StringBuilder result = new StringBuilder();
            bool notFirst = false;

            using (XmlReader reader = XmlReader.Create(fullPath, settings))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement(ParentNodeName))
                    {
                        if (notFirst)
                        {
                            result.Append("\n");
                        }
                        notFirst = true;
                        result.Append(IncludeParentNode ? reader.ReadOuterXml() : reader.ReadInnerXml().Trim());
                    }
                }
            }
            return result.ToString();
        }

        private void SaveXMLToFile(string xml)
        {
            if (OutputFileName == "")
            {
                OutputFileName = DirectoryPath + "\\output.xml";
            }
            File.WriteAllText(OutputFileName, xml, Encoding.UTF8);
        }
    }
}
