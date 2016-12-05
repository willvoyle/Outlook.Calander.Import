using Microsoft.Exchange.WebServices.Data;
using Outlook.Calander.Import.Extensions;
using Outlook.Calander.Import.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Outlook.Calander.Import.Harness
{
    public class OutlookService
    {
        private readonly ExchangeService _exhcangeService;
        private readonly string _autoDiscoverEmail;

        public OutlookService()
        {
            _exhcangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            _autoDiscoverEmail = ConfigurationManager.AppSettings["AutoDiscoverEmail"];
        }

        public void CreateConnection(string userName, string password)
        {
            _exhcangeService.Credentials = new WebCredentials(userName, password);
            //_exhcangeService.UseDefaultCredentials = true;

            _exhcangeService.AutodiscoverUrl(_autoDiscoverEmail);
        }

        public IEnumerable<MeetingModel> GetAppointments()
        {
            var startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endDate = DateTime.Now.EndOfWeek(DayOfWeek.Sunday);

            var appointments = FindAppointments(startDate, endDate);

            return MapAppointments(appointments);
        }

        private FindItemsResults<Appointment> FindAppointments(DateTime startDate, DateTime endDate)
        {
            CalendarFolder calendar = CalendarFolder.Bind(_exhcangeService, WellKnownFolderName.Calendar, new PropertySet());

            CalendarView calendarView = new CalendarView(startDate, endDate);

            calendarView.PropertySet = new PropertySet(ItemSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

            return calendar.FindAppointments(calendarView);
        }

        private static IEnumerable<MeetingModel> MapAppointments(FindItemsResults<Appointment> appointments)
        {
            var meetings = new List<MeetingModel>();
            foreach (var app in appointments)
            {
                meetings.Add(new MeetingModel
                {
                    Title = app.Subject,
                    Start = app.Start,
                    End = app.End,
                    Editable = false
                });
            }

            return meetings;
        }
    }
}