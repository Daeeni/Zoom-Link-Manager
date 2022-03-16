using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zoom_Link_Manager.Models;

namespace Zoom_Link_Manager.ViewModels
{
    public class FachViewModel : PropertyChangedBase
    {
        private readonly FachModel model;
        private readonly IEnumerable<DayOfWeek> _tage = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
        public FachViewModel(FachModel model)
        {
            this.model = model;
        }
        public FachViewModel()
        {
            this.model = new FachModel();
        }
        public DayOfWeek Day
        {
            get => model.Day;
            set
            {
                if(model.Day != value && Enum.IsDefined(typeof(DayOfWeek), value))
                {
                    model.Day = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public string Time
        {
            get => model.Time;
            set
            {
                string pattern = @"^(?:[01]?\d|2[0-3])(?::[0-5]\d){1,2}$";
                if (model.Time != value && Regex.IsMatch(value, pattern))
                {
                    model.Time = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public string Link
        {
            get => model.Link;
            set
            {
                if(model.Link != value && Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    model.Link = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public string Fach
        {
            get => model.Fach;
            set
            {
                if(model.Fach != value )
                {
                    model.Fach = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public IEnumerable<DayOfWeek> Tage
        {
            get
            {
                return _tage;
            }
        }
        public FachModel Model
        {
            get => model;
        }
    }
}
