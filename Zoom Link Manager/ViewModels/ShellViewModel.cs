using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Zoom_Link_Manager.Models;

namespace Zoom_Link_Manager.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            System.Timers.Timer timer = TimerBackgroundService.SetTimer();
            timer.Elapsed += OnTimerEnd;
        }

        private string _fachBox = "Test Fach";
        private string _linkBox = "https://www.google.com/";
        private string _zeitBox = "12:15";
        private DayOfWeek _selectedTag = DayOfWeek.Monday; 
        private IEnumerable<DayOfWeek> _tage = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
        private BindableCollection<FachModel> _faecherBindable = new(FileAccess.GetFaecher());

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
        public DayOfWeek SelectedTag
        {
            get { return _selectedTag ; }
            set {
                _selectedTag = value;
                NotifyOfPropertyChange();
            }
        }
        public IEnumerable<DayOfWeek> Tage
        {
            get { return _tage ; }
            set {
                _tage  = value;
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<FachModel> FaecherBindable
        {
            set { _faecherBindable = value; }
            get { return _faecherBindable; }
        }
        public void DeleteFach(object item)
        {
            FaecherBindable.Remove((FachModel)item);
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
        }
        public void UpdateFach(object item)
        {
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
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
        public void AddFach(string fachBox, string zeitBox, string linkBox, DayOfWeek selectedTag)
        {
            FaecherBindable.Add(new FachModel(linkBox, fachBox, zeitBox, selectedTag));
            FileAccess.WriteFaecher(FaecherBindable.ToList<FachModel>());
            ZeitBox = "";
            LinkBox = "";
            FachBox = "";
            NotifyOfPropertyChange(() => FaecherBindable);
        }
        public static void OpenLink(String url)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = url;
            System.Diagnostics.Process.Start(psi);
        }
        private void OnTimerEnd(Object source, System.Timers.ElapsedEventArgs e)
        {
             foreach (FachModel fach in FaecherBindable.ToList<FachModel>())
            {
                TimeSpan fachTimer = fach.Time.TimeOfDay;
                TimeSpan offset = fachTimer - TimeSpan.FromMinutes(5);
                TimeSpan now = DateTime.Now.TimeOfDay;
                if(now <= fachTimer && now > offset && fach.Day == DateTime.Now.DayOfWeek)
                {
                    OpenLink(fach.Link.ToString());
                }
            }
        }
    }
}
