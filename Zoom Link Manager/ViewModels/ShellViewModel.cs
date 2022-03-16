using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Zoom_Link_Manager.Models;

namespace Zoom_Link_Manager.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            _faecherBindable = new(PersistentStorage.GetFaecher());
            System.Timers.Timer timer = TimerBackgroundService.SetTimer();
            timer.Elapsed += OnTimerEnd;
            AttemptingDeactivation += (sender, args) => timer.Dispose();
        }
        private BindableCollection<FachViewModel> _faecherBindable;
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
        public void EditGrid(Object source)
        {
            PersistentStorage.WriteFaecher(_faecherBindable.ToList<FachViewModel>());
        }
    }
}
