using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Zoom_Link_Manager.Models;

namespace Zoom_Link_Manager.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            _faecherBindable = new(PersistentStorage.GetFaecher());
            //_faecherBindable.CollectionChanged += ContentCollectionChanged;
            

            System.Timers.Timer timer = TimerBackgroundService.SetTimer();
            timer.Elapsed += OnTimerEnd;
            AttemptingDeactivation += (sender, args) => timer.Dispose();
        }

        private string _fachBox = "Test Fach";
        private string _linkBox = "https://www.google.com/";
        private string _zeitBox = "12:15";
        private DayOfWeek _selectedTag = DayOfWeek.Monday; 
        private IEnumerable<DayOfWeek> _tage = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
        private BindableCollection<FachViewModel> _faecherBindable;

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
        public BindableCollection<FachViewModel> FaecherBindable
        {
            set { 
                _faecherBindable = value;
                PersistentStorage.WriteFaecher(_faecherBindable.ToList<FachViewModel>());
                NotifyOfPropertyChange(() => FaecherBindable);
            }
            get { return _faecherBindable; }
        }
        public void OpenLink(object item)
        {
            try
            {
                OpenBrowser(((FachViewModel)item).Link);
            }
            catch (System.InvalidCastException)
            {
            }
            
        }
        public static void OpenBrowser(String url)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = url;
            System.Diagnostics.Process.Start(psi);
        }
        private void OnTimerEnd(Object source, System.Timers.ElapsedEventArgs e)

        {
            foreach (FachViewModel fach in FaecherBindable.ToList<FachViewModel>())
            {
                TimeSpan fachTimer = TimeSpan.Parse(fach.Time);
                TimeSpan offset = fachTimer - TimeSpan.FromMinutes(5);
                TimeSpan now = DateTime.Now.TimeOfDay;
                if (now <= fachTimer && now > offset && fach.Day == DateTime.Now.DayOfWeek)
                {
                    OpenBrowser(fach.Link.ToString());
                }
            }
        }
        //public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (FachViewModel item in e.OldItems)
        //        {
        //            Removed items
        //            item.PropertyChanged -= EntityViewModelPropertyChanged;
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (FachViewModel item in e.NewItems)
        //        {
        //            Added items
        //            item.PropertyChanged += EntityViewModelPropertyChanged;
        //        }
        //    }
        //}
        //public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    PersistentStorage.WriteFaecher(FaecherBindable.ToList<FachViewModel>());
        //}

    }
}
