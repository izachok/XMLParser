using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "";
            string parentNodeName = "";
            bool includeParentNode = false;
            string outputFilePath = "";

            Console.WriteLine("Enter full path to directory with xml files:");
            directoryPath = Console.ReadLine();

            Console.WriteLine("Enter parent node's name:");
            parentNodeName = Console.ReadLine();

            Console.WriteLine("Include parent node to output file?(y - yes, n - no):");
            char key = Console.ReadKey().KeyChar;
            includeParentNode = (key == 'y' || key == 'Y') ? true : false;

            Console.WriteLine("\nEnter output file name with full path:");
            outputFilePath = Console.ReadLine();

            //ProcessFilesInDirectory(@"d:\IT\Code\XMLParser\XMLParser\XML", "HotSpots", 
            //    true, @"d:\IT\Code\XMLParser\XMLParser\result.xml");

            //XMLParser parser = new XMLParser(@"d:\IT\Code\XMLParser\XMLParser\XML", "Panoramas", 
            //    true, @"d:\IT\Code\XMLParser\XMLParser\result.xml");
            XMLParser parser = new XMLParser(directoryPath, parentNodeName, includeParentNode, outputFilePath);
            parser.ProcessFilesInDirectory();
            //ProcessFilesInDirectory(directoryPath, parentNodeName, includeParentNode, outputFilePath);
            Console.WriteLine("Press any key...");
            Console.Read();
        }
    }
}