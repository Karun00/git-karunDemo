using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Web.Api
{
    public class PortsController : ApiControllerBase
    {
        IPortService _portservice;

        //  IDashBoardService _dashboardService;

        public PortsController()
        {
            _portservice = new PortClient();
            //   _dashboardService = new DashBoardClient();
        }

        public HttpResponseMessage GetLoginPort(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Port> ports = null;
                using (IPortService ls = new PortClient())
                {
                    ports = ls.GetLoginPort();
                }
                response = request.CreateResponse<List<Port>>(HttpStatusCode.OK, ports);
                return response;
            });
        }

        public HttpResponseMessage GetAllPorts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Port> ports = null;
                using (IPortService ls = new PortClient())
                {
                    ports = ls.GetPorts();
                }
                response = request.CreateResponse<List<Port>>(HttpStatusCode.OK, ports);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostPortData(HttpRequestMessage request, Port value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Port portCreated = null;
                using (IPortService ls = new PortClient())
                {
                    portCreated = ls.AddPort(value);
                }
                response = request.CreateResponse<Port>(HttpStatusCode.Created, portCreated);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage ModifyPortData(HttpRequestMessage request, Port value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Port portCreated = null;
                using (IPortService ls = new PortClient())
                {
                    portCreated = ls.ModifyPort(value);
                }
                response = request.CreateResponse<Port>(HttpStatusCode.Created, portCreated);
                return response;
            });
        }

        public HttpResponseMessage GetPortDtl(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Port port = null;
                using (IPortService ls = new PortClient())
                {
                    port = ls.GetPortId(id);
                }
                response = request.CreateResponse<Port>(HttpStatusCode.OK, port);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage DeletePortData(HttpRequestMessage request, [FromUri]long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Port portCreated = null;
                using (IPortService ls = new PortClient())
                {
                    portCreated = ls.DelPort(id);
                }
                response = request.CreateResponse<Port>(HttpStatusCode.Created, portCreated);
                return response;
            });
        }

        public Port PutDeletePort([FromUri]long id)
        {
            return _portservice.DelPort(id);
        }

        /// <summary>
        /// /////mahesh
        /// </summary>
        /// <param name="disposing"></param>
        [Route("api/Dashboard")]
        [HttpGet]
        public HttpResponseMessage DashBoardDetails(HttpRequestMessage request, string fromDate, string toDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DashBoardVO> SRDtls = null;

                using (IDashBoardService ls = new DashBoardClient())
                {
                    SRDtls = ls.DashBoardDetails(fromDate, toDate);
                }
                response = request.CreateResponse<List<DashBoardVO>>(HttpStatusCode.OK, SRDtls);
                return response;
            });
        }
        [Authorize]
        [AllowAnonymous]
        [Route("api/GetPlannedMovementsCount/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetPlannedMovementsCount(HttpRequestMessage request,string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PlannedMovementsDtlsVO> SRDtls = null;


                using (IDashBoardService ls = new DashBoardClient())
                {
                    SRDtls = ls.GetPlannedMovementsCount(portcode);
                }
                response = request.CreateResponse <List<PlannedMovementsDtlsVO>> (HttpStatusCode.OK, SRDtls);
                return response;
            });
        }
        [Authorize]
        [Route("api/GetAnchorageCount/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetAnchorageCount(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AnchorageDtlsVO SRDtls1 = null;

                using (IDashBoardService ls = new DashBoardClient())
                {
                    SRDtls1 = ls.GetAnchorageCount(portcode);
                }
                response = request.CreateResponse<AnchorageDtlsVO>(HttpStatusCode.OK, SRDtls1);
                return response;
            });
        }

        [Authorize]
        [AllowAnonymous]
        [Route("api/GetPortWiseCount/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetPortWiseCount(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PortWiseCountVO> SRDtls2 = null;


                using (IDashBoardService ls = new DashBoardClient())
                {
                    SRDtls2 = ls.GetPortWiseCount(portcode);
                }
                response = request.CreateResponse<List<PortWiseCountVO>>(HttpStatusCode.OK, SRDtls2);
                return response;
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _portservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

