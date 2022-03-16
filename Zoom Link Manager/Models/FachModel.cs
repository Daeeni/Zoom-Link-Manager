using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zoom_Link_Manager.Models
{
    
    public class FachModel
    {
        public string Link { get; set; }
        public string Fach { get; set; }
        public string Time { get; set; }
        public DayOfWeek Day { get; set; }

        [JsonConstructor]
        public FachModel(string link, string fach, string time, DayOfWeek day)
        {
            Link = link;
            Fach = fach;
            Day = day;
            Time = time;
        }
        public FachModel() { }
    }
}
