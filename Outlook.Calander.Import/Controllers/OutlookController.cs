using System;
using Outlook.Calander.Import.Services;
using System.Configuration;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Security.Principal;
using System.Web.Http.Cors;

namespace Outlook.Calander.Import.Controllers
{
    public class OutlookController : ApiController
    {
        private readonly OutlookService _outlookService;

        public OutlookController()
        {
            _outlookService = new OutlookService();        
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Get(DateTime start, DateTime end)
        {
            string userName = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            string password = ConfigurationManager.AppSettings["ExchangePassword"];

            try
            {
                _outlookService.CreateConnection(userName, password);
                var appointments = _outlookService.GetAppointments(start, end);

                return Request.CreateResponse(HttpStatusCode.OK, appointments);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
