using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zoom_Link_Manager.Models;
using System.Text.Json;
namespace Zoom_Link_Manager
{
    public class FileAccess
    {
        private static readonly string _saveTo = @"C:\Users\Daniel\Desktop\tmp\test.json";
        //static string fileName = String.Format(@"{0}\faecher.json", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
        static public List<FachModel> GetFaecher()
        {
            try
            {
                string jsonResult = File.ReadAllText(_saveTo);
                List<FachModel> test = JsonSerializer.Deserialize<List<FachModel>>(jsonResult);
                return test;
            }
            catch(FileNotFoundException)
            {
                return new List<FachModel>();
            }
        }
        static public void WriteFaecher(List<FachModel> toSave)
        {
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            string toWrite = JsonSerializer.Serialize(toSave, options);
            System.Diagnostics.Debug.WriteLine("Test:" + toWrite);
            File.WriteAllText(_saveTo, toWrite);
        }
    }
}
