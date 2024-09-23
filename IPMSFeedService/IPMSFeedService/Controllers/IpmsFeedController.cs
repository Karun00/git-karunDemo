using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Text.RegularExpressions;
using System.Web.Http;
using IPMSFeedService.Filters;
using IPMSFeedService.Models;
using IPMSFeedService.Repository;
using IPMSFeedService.ValueObjects;
using log4net;
using Microsoft.Security.Application;

namespace IPMSFeedService.Controllers
{
    [Authorize]
    [ValidateAntiForgeryTokenFilter]
    public class IpmsFeedController : ApiController
    {
        private readonly IIpmsFeedRepository _ipmsFeedRepository;
        public static readonly ILog log = LogManager.GetLogger(typeof(IpmsFeedController));

        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {

            HttpResponseMessage response = null;
            try
            {
                if (codeToExecute != null)
                {
                    if (!IsValidInput(request))
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotAcceptable, GlobalConstants.InvalidInput);
                        return response;
                    }

                    response = codeToExecute.Invoke();


                }
            }
            catch (SecurityException ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message, Configuration.Formatters.XmlFormatter);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Business Validation : "))
                {
                    response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.Replace("Business Validation : ", "").ToString(), Configuration.Formatters.XmlFormatter);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.InternalServerError, GlobalConstants.InternalServerErrorMessage, Configuration.Formatters.XmlFormatter);
                }
                log.Error(LogErrorMessage(GlobalConstants.InternalServerErrorMessage, ex));

            }

            return response;
        }
        protected T ExecuteWithException<T>(Func<T> codeToExecute)
        {
            try
            {

                T response = default(T);
                if (codeToExecute != null)
                {
                    response = codeToExecute.Invoke();
                }
                return response;

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, GlobalConstants.InternalServerErrorMessage));
            }
        }
        public IpmsFeedController()
        {
            _ipmsFeedRepository = new IpmsFeedRepository();
            log4net.Config.XmlConfigurator.Configure();

        }

        //TODO: Front end UI Interface to be developed, if required
        [Route("GetServiceData")]
        public HttpResponseMessage GetServiceData(HttpRequestMessage request, string portCode = null, string movementFromDate = null, string movementToDate = null, string vcn = null, string imono = null, string vesselname = null)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var data = _ipmsFeedRepository.GetIpmsServiceFeedDetails(portCode, movementFromDate, movementToDate, vcn, imono, vesselname);
                response = request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.XmlFormatter);
                return response;
            });
        }


        //TODO: Front end UI Interface to be developed, if required
        [Route("GetArrivalNotificationData")]
        public HttpResponseMessage GetArrivalNotificationData(HttpRequestMessage request, string portCode = null, string etaFromDate = null, string etaToDate = null, string vcn = null, string imono = null, string vesselname = null)
        {

            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                var data = _ipmsFeedRepository.GetIpmsANFeedDetails(portCode, etaFromDate, etaToDate, vcn, imono, vesselname);
                response = request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.XmlFormatter);
                return response;
            });
        }

        //TODO: Front end UI Interface to be developed, if required
        [Route("GetLocationData")]
        public HttpResponseMessage GetLocationData(HttpRequestMessage request, string portCode = null, string portLimitInFromDate = null, string portLimitInToDate = null, string vcn = null, string imono = null, string vesselname = null)
        {

            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                var data = _ipmsFeedRepository.GetIpmsFeedLocationDetails(portCode, portLimitInFromDate, portLimitInToDate, vcn, imono, vesselname);
                response = request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.XmlFormatter);

                return response;
            });
        }


        private string LogErrorMessage(string pretext, Exception ex)
        {
            string msg = pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message + (string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace);
                }
            }
            return msg;
        }

        public bool IsValidInput(HttpRequestMessage request)
        {

            bool isValidInput = true;
            HttpResponseMessage response = null;
            foreach (var parameter in request.GetQueryNameValuePairs())
            {
                if (parameter.Value != "")
                {
                    string paramval = Sanitizer.GetSafeHtmlFragment(parameter.Value);
                    if (paramval == "")
                    {
                        log.Info("Incorrect Input Parameter: " + parameter.Value);
                        isValidInput = false;
                        return isValidInput;
                    }
                    else
                    {
                        Match validation = Regex.Match(parameter.Value,
                            GlobalConstants.Validateparameters);
                        if (validation.Success)
                        {
                            log.Info("Incorrect Input Parameter: " + parameter.Value);
                            isValidInput = false;
                            return isValidInput;
                        }
                    }
                }
            }

            return isValidInput;

        }

    }
}
