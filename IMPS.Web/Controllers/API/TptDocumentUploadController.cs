using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class TptDocumentUploadController : ApiControllerBase
    {
        ITptDocumentUploadService _TptDocUploadService;

        public TptDocumentUploadController()
        {
            _TptDocUploadService = new TptDocumentUploadClient();
        }

        #region api/TerminalDelaysDetails
        /// <summary>
        /// To save TerminalDelayDetails
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/TerminalDelayDetails")]
        [HttpPost]
        public HttpResponseMessage PostTerminalDelaysData(HttpRequestMessage request, List<TerminalDelaysVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TerminalDelaysVO> TerminalDelaysCreated = _TptDocUploadService.InsertTerminalDelays(value);
                response = request.CreateResponse<List<TerminalDelaysVO>>(HttpStatusCode.Created, TerminalDelaysCreated);
                return response;
            });
        }
        #endregion

        [Authorize]
        [Route("api/OutTurnVolumeDetails")]
        [HttpPost]
        public HttpResponseMessage PostOutTurnVolumesData(HttpRequestMessage request, List<OutTurnVolumesVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<OutTurnVolumesVO> OutTurnVolumesCreated = _TptDocUploadService.InsertOutTurnVolumes(value);
                response = request.CreateResponse<List<OutTurnVolumesVO>>(HttpStatusCode.Created, OutTurnVolumesCreated);
                return response;
            });
        }

        [Authorize]
        [Route("api/TerminalWeeklyDataDetails")]
        [HttpPost]
        public HttpResponseMessage PostTerminalWeeklyData(HttpRequestMessage request, List<TerminalWeeklyDataVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TerminalWeeklyDataVO> OutTurnVolumesCreated = _TptDocUploadService.InsertTerminalData(value);
                response = request.CreateResponse<List<TerminalWeeklyDataVO>>(HttpStatusCode.Created, OutTurnVolumesCreated);
                return response;
            });
        } 
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _TptDocUploadService.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class TfrDocumentUploadController : ApiControllerBase
    {
        ITptDocumentUploadService _TptDocUploadService;

        public TfrDocumentUploadController()
        {
            _TptDocUploadService = new TptDocumentUploadClient();
        }

        #region api/RailPlanDetails
        /// <summary>
        /// To save RailPlanDetails
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/RailPlanDetails")]
        [HttpPost]
        public HttpResponseMessage PostRailPlanData(HttpRequestMessage request, List<RailPlanVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RailPlanVO> RailPlanCreated = _TptDocUploadService.InsertRailPlan(value);
                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.Created, RailPlanCreated);
                return response;
            });
        }
        #endregion

        [Authorize]
        [Route("api/RailChangeNotifications")]
        [HttpPost]
        public HttpResponseMessage UpdateRailChangeNotifications(HttpRequestMessage request, List<RailPlanVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RailPlanVO> RailChangeUpdates = _TptDocUploadService.UpdateRailChangeNotifications(value);
                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.Created, RailChangeUpdates);
                return response;
            });
        }

        [Authorize]
        [Route("api/ArrivalAndDepartureTimes")]
        [HttpPost]
        public HttpResponseMessage UpdateArrivalAndDepartureTimes(HttpRequestMessage request, List<RailPlanVO> value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RailPlanVO> ArrivalDepartures = _TptDocUploadService.UpdateArrivalAndDepartureTimes(value);
                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.Created, ArrivalDepartures);
                return response;
            });
        }


        #region TrainMonitoringDetails
        /// <summary>
        /// This method is used for fetches the Ports
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Route("api/TrainMonitoringDetails/{plannedDate}/{Corridor}/{movementStatus}")] 
        [Route("api/TrainMonitoringDetails/{FromDate}/{ToDate}")] 
        [HttpGet]
         public HttpResponseMessage GetTrainMonitoringDetails(HttpRequestMessage request, string FromDate, string ToDate)
        //public HttpResponseMessage GetTrainMonitoringDetails(HttpRequestMessage request, string plannedDate, string Corridor, string movementStatus)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<TrainMonitoringVO> serviceTypes = _TptDocUploadService.GetTrainMonitoringDetails(FromDate, ToDate);
                response = request.CreateResponse<List<TrainMonitoringVO>>(HttpStatusCode.OK, serviceTypes);
                return response;
            });
        }

        [Route("api/WagonDetails/{TrainNo}/{origin}")]
        [HttpGet]
        public HttpResponseMessage GetWagonDetails(HttpRequestMessage request, int TrainNo, string Origin,DateTime OriginDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<WagonDetailsVO> serviceTypes = _TptDocUploadService.GetWagonDetailsFromTfrService(TrainNo, Origin, OriginDate);
                response = request.CreateResponse<List<WagonDetailsVO>>(HttpStatusCode.OK, serviceTypes);
                return response;
            });
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _TptDocUploadService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
