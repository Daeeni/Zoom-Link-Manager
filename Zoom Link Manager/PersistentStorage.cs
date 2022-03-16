using System.Collections.Generic;
using System.IO;
using Zoom_Link_Manager.Models;
using System.Text.Json;
using Zoom_Link_Manager.ViewModels;

namespace Zoom_Link_Manager
{
    public class PersistentStorage
    {
        private static readonly string _saveTo = Directory.GetCurrentDirectory()+@"\faecher.json";
        static public List<FachModel> GetFaecherListModel()
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

        static public List<FachViewModel> GetFaecher()
        {
            List<FachModel> listModel = GetFaecherListModel();
            List<FachViewModel> listViewModel = new List<FachViewModel>();
            foreach (FachModel model in listModel)
            {
                listViewModel.Add(new FachViewModel(model));
            }
            return listViewModel;
        }
        static public void WriteFaecherList(List<FachModel> toSave)
        {
            string toWrite = JsonSerializer.Serialize(toSave);
            System.Diagnostics.Debug.WriteLine(toWrite);
            File.WriteAllText(_saveTo, toWrite);
        }
        static public void WriteFaecher(List<FachViewModel> toSave)
        {
            List<FachModel> listModel = new List<FachModel>();
            foreach (FachViewModel model in toSave)
            {
                listModel.Add(model.Model);
            }
            WriteFaecherList(listModel);
        }
    }
}
