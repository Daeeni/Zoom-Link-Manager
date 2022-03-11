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
        public Uri Link { get; set; }
        public string Fach { get; set; }
        public DateTime Time { get; set; }
        public DayOfWeek Day { get; set; }

        [JsonConstructor]
        public FachModel(Uri link, string fach, DateTime time, DayOfWeek day)
        {
            Link = link;
            Fach = fach;
            Day = day;
            Time = time;
        }
        public FachModel(string link, string fach, string time, DayOfWeek day)
        {
            Link = new Uri(link);
            Fach = fach;
            Day = day;
            Time = DateTime.ParseExact(time, "HH:mm", CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
