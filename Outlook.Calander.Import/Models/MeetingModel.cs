using System;
using System.Collections.Generic;

namespace Outlook.Calander.Import.Models
{
    public class MeetingModel : IModel
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Editable { get;  set; }
        public IEnumerable<AttendeeModel> Attendees { get; set; }
    }
}