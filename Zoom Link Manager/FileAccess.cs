using System.Collections.Generic;
using System.IO;
using Zoom_Link_Manager.Models;
using System.Text.Json;
namespace Zoom_Link_Manager
{
    public class FileAccess
    {
        private static readonly string _saveTo = Directory.GetCurrentDirectory()+@"\faecher.json";
        static public List<FachModel> GetFaecher()
        {
            try
            {
                string jsonResult = File.ReadAllText(_saveTo);
                List<FachModel> returnList = JsonSerializer.Deserialize<List<FachModel>>(jsonResult);
                return returnList;
            }
            catch(FileNotFoundException)
            {
                return new List<FachModel>();
            }
        }
        static public void WriteFaecher(List<FachModel> toSave)
        {
            string toWrite = JsonSerializer.Serialize(toSave);
            File.WriteAllText(_saveTo, toWrite);
        }
    }
}
