using Microsoft.Exchange.WebServices.Data;
using Outlook.Calander.Import.Models;
using System.Collections.Generic;
using System.Linq;

namespace Outlook.Calander.Import.Mappers
{
    public class MeetingMapper : Mapper<FindItemsResults<Appointment>>
    {
        protected override IEnumerable<IModel> DoMap(FindItemsResults<Appointment> model)
        {
            var meetings = new List<MeetingModel>();
            foreach (var app in model)
            {
                meetings.Add(new MeetingModel
                {
                    Title = app.Subject,
                    Start = app.Start,
                    End = app.End,
                    Editable = false,
                    Attendees = app.RequiredAttendees.Select(a => new AttendeeModel { Name = a.Name, Email = a.Address })
                });
            }

            return meetings;
        }
    }
}