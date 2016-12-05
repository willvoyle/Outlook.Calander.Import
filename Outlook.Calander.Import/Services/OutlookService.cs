using Microsoft.Exchange.WebServices.Data;
using Outlook.Calander.Import.Extensions;
using Outlook.Calander.Import.Models;
using Outlook.Calander.Import.Mappers;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Outlook.Calander.Import.Services
{
    public class OutlookService
    {
        private readonly ExchangeService _exhcangeService;
        private readonly Mapper<FindItemsResults<Appointment>> _meetingMapper;

        private readonly string _autoDiscoverEmail;

        public OutlookService()
        {
            _meetingMapper = new MeetingMapper();
            _exhcangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            _autoDiscoverEmail = ConfigurationManager.AppSettings["AutoDiscoverEmail"];
        }

        public void CreateConnection(string userName, string password)
        {
            _exhcangeService.Credentials = new WebCredentials(userName, password);
            //_exhcangeService.UseDefaultCredentials = true;

            _exhcangeService.AutodiscoverUrl(_autoDiscoverEmail);
        }

        public IEnumerable<IModel> GetAppointments(DateTime start, DateTime end)
        {
            var startDate = start.StartOfWeek(DayOfWeek.Monday);
            var endDate = end.EndOfWeek(DayOfWeek.Sunday);

            var appointments = FindAppointments(startDate, endDate);

            LoadAppointmentsProperties(appointments);

            return _meetingMapper.Map(appointments);
        }

        private FindItemsResults<Appointment> FindAppointments(DateTime startDate, DateTime endDate)
        {
            CalendarFolder calendar = CalendarFolder.Bind(_exhcangeService, WellKnownFolderName.Calendar, new PropertySet());

            CalendarView calendarView = new CalendarView(startDate, endDate);

            calendarView.PropertySet = new PropertySet(BasePropertySet.IdOnly);

            return calendar.FindAppointments(calendarView);
        }


        private void LoadAppointmentsProperties(FindItemsResults<Appointment> appointments)
        {
            var loadProperties = new PropertySet(ItemSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End, AppointmentSchema.RequiredAttendees, AppointmentSchema.OptionalAttendees);
            _exhcangeService.LoadPropertiesForItems(appointments, loadProperties);
        }
    }
}