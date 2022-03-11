using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Zoom_Link_Manager.Models;

namespace Zoom_Link_Manager.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _fachBox = "TestFach";
        private string _linkBox = "https://www.google.com/";
        private string _zeitBox = "12:15";
        private DayOfWeek _selectedTag = DayOfWeek.Monday; 
        private IEnumerable<DayOfWeek> _tage = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
        private BindableCollection<FachModel> _faecherBindable = new(FileAccess.GetFaecher());

        ~ShellViewModel()
        {
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
        }
        public string FachBox 
        {
            get { return _fachBox; }
            set { 
                _fachBox = value;
                NotifyOfPropertyChange();
            }
        }
        public string ZeitBox
        {
            get { return _zeitBox; }
            set {
                _zeitBox = value;
                NotifyOfPropertyChange();
            }
        }
        public string LinkBox
        {
            get { return _linkBox; }
            set 
            {
                _linkBox = value;
                NotifyOfPropertyChange();
            }
        }
        public bool CanAddFach(string fachBox, string zeitBox, string linkBox, DayOfWeek selectedTag)
        {
            string pattern = @"^(?:[01]?\d|2[0-3])(?::[0-5]\d){1,2}$";
            return !String.IsNullOrWhiteSpace(fachBox)
                && !String.IsNullOrWhiteSpace(zeitBox)
                && Uri.IsWellFormedUriString(linkBox, UriKind.Absolute)
                && Regex.IsMatch(zeitBox, pattern)
                && Enum.IsDefined(typeof(DayOfWeek), selectedTag);
        }


        public void UpdateFach(object irgendwas)
        {
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
        }

        //private void UpdateFach_Click(object sender, RoutedEventArgs e)
        //{
        //    FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
        //}




        public void AddFach(string fachBox, string zeitBox, string linkBox, DayOfWeek selectedTag)
        {
            FaecherBindable.Add(new FachModel(linkBox, fachBox, zeitBox, selectedTag));
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
            ZeitBox = "";
            LinkBox = "";
            FachBox = "";
            NotifyOfPropertyChange(() => FaecherBindable);
        }
        public IEnumerable<DayOfWeek> Tage
        {
            get { return _tage ; }
            set { 
                _tage  = value;
                NotifyOfPropertyChange(() => SelectedTag);
            }
        }
        public DayOfWeek SelectedTag
        {
            get { return _selectedTag; }
            set { 
                _selectedTag = value;

            }
        }
        public BindableCollection<FachModel> FaecherBindable
        {
            set { _faecherBindable = value; }
            get { return _faecherBindable; }
        }
    }
}
