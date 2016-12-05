using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;

namespace Outlook.Calander.Import.Harness
{
    public class Program
    {

        static void Main(string[] args)
        {
            var _outlookService = new OutlookService();

            string userName = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            string password = ConfigurationManager.AppSettings["ExchangePassword"];

            try
            {
                _outlookService.CreateConnection(userName, password);

                var appointments = _outlookService.GetAppointments();

                foreach (var app in appointments)
                {
                    Console.WriteLine($"{app.Title}  -  {app.Start}  -  {app.End} {Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
