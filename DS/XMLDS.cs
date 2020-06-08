using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS
{
   public static class XMLDS
    {
      private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
      private static string filePath = System.IO.Path.Combine(solutionDirectory, "DS", "Data XML");
        private static XElement configurationRoot = null;
        private static XElement traineeRoot = null;
        private static XElement testerRoot = null;
        private static XElement testRoot = null;
        private static string configurationPath = Path.Combine(filePath, "configXml.xml");
        private static string traineePath = Path.Combine(filePath, "TraineeXml.xml");
        private static string testerPath = Path.Combine(filePath, "TesterXml.xml");
        private static string drivingtestPath = Path.Combine(filePath, "TestXml.xml");
       static XMLDS()
        {
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                //Create a file with all the Data source 
                Directory.CreateDirectory(filePath);
            }
            if (!File.Exists(configurationPath))
            {
                // Configuration file
                CreateFile("Configuration", configurationPath);
                //First time file
            }
            if (!File.Exists(traineePath))
            {
                // Create Trainees File root
                CreateFile("Trainees", traineePath);

            }
            traineeRoot = LoadData(traineePath);


            if (!File.Exists(testerPath))
            {
                //Create file With Testrs root
                CreateFile("Testers", testerPath);

            }
            testerRoot = LoadData(testerPath);

            if (!File.Exists(drivingtestPath))
            {
                //Create file with Tests root
                CreateFile("Tests", drivingtestPath);

            }
            testRoot = LoadData(drivingtestPath);

        }
        private static void CreateFile(string typename, string path)
        {
            //Create a new file 
            XElement root = new XElement(typename);
            root.Save(path);
        }
        public static void SaveConfiguration()
        {
            configurationRoot.Save(configurationPath);
        }
        public static void SaveTrainees()
        {
            traineeRoot.Save(traineePath);
        }

        public static void SaveTesters()
        {
            testerRoot.Save(testerPath);
        }

        public static void SaveDrivingtests()
        {
           testRoot.Save(drivingtestPath);
        }
        public static XElement TestNumber
        {
            get
            {
                configurationRoot = LoadData(configurationPath);
                return configurationRoot;
            }
        }
        public static XElement Trainees
        {
            get
            {
                traineeRoot = LoadData(traineePath);
                return traineeRoot;
            }
        }

        public static XElement Testers
        {
            get
            {
                testerRoot = LoadData(testerPath);
                return testerRoot;
            }
        }

        public static XElement DrivingTests
        {
            get
            {
               testRoot = LoadData(drivingtestPath);
                return testRoot;
            }
        }

        private static XElement LoadData(string path)
        {
            XElement root;
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            return root;
        }

    }
}

    
