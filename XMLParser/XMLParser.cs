using System;
using System.Text;
using System.IO;
using System.Xml;

namespace XMLParser
{
    class XMLParser
    {
        public string DirectoryPath { get; set; }
        public string ParentNodeName { get; set; }
        public bool IncludeParentNode { get; set; }
        public string OutputFileName { get; set; }

        public XMLParser(string directoryPath, string parentNodeName, bool includeParentNode, string outputFileName)
        {
            DirectoryPath = directoryPath;
            ParentNodeName = parentNodeName;
            IncludeParentNode = includeParentNode;
            OutputFileName = outputFileName;
        }

        public void ProcessFilesInDirectory()
        {
            StringBuilder resultXML = new StringBuilder();
            try
            {
                var xmlFiles = Directory.EnumerateFiles(DirectoryPath, "*.xml");

                foreach (string currentFile in xmlFiles)
                {
                    resultXML.Append(FindXMLNode(currentFile).Trim()).Append("\n");
                }
                SaveXMLToFile(resultXML.ToString());
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
            string result = "";

            using (XmlReader reader = XmlReader.Create(fullPath, settings))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement(ParentNodeName))
                    {
                        result = IncludeParentNode ? reader.ReadOuterXml() : reader.ReadInnerXml();
                    }
                }
            }
            return result;
        }

        private void SaveXMLToFile(string xml)
        {
            
            File.WriteAllText(OutputFileName, xml, Encoding.UTF8);
        }
    }
}
