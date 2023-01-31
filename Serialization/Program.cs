using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class Program
    {


        [Serializable]
        public class MySettings
        {
            public int? screenDx;
            public HashSet<string> recentlyOpenedFiles;
            [NonSerialized]
            public string dummy;
            public MySettings()
            {
                screenDx = 100;
                recentlyOpenedFiles = new HashSet<string>();
                recentlyOpenedFiles.Add("File1");
                recentlyOpenedFiles.Add("File2");
                recentlyOpenedFiles.Add("File3");
            }
        }
        
        public class Settings
        {
            const int VERSION = 1;
            public static void save(MySettings settings, string fileName)
            {
                Stream stream = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(fileName,FileMode.Create,FileAccess.Write,FileShare.None);
                    formatter.Serialize(stream, VERSION);
                    formatter.Serialize(stream, settings);

                }
                catch { }
                finally
                {
                    if(null != stream)
                        stream.Close();
                }
            }

            public static MySettings Load(string fileName)
            {
                Stream stream = null;
                MySettings settings = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    int version = (int)formatter.Deserialize(stream);
                    Debug.Assert(version == VERSION);
                    settings = (MySettings)formatter.Deserialize(stream);
                }
                catch { }
                finally
                {
                    if(null != stream) stream.Close();

                }
                return settings;
            }
        }
               
        
        
        public static void Main(string[] args)
        {
            string fileName = @"C:\temp\serialize.txt";
            MySettings mySettings = new MySettings();       
            Settings.save(mySettings, fileName);
            
            MySettings deserialized = Settings.Load(fileName);
            Console.WriteLine($"{deserialized.screenDx}: {deserialized.recentlyOpenedFiles.Count}");
        }
    }
}
