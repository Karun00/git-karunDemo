using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace IPMS.Web.Controllers.API
{
    public class AgentController : ApiControllerBase
    {
        IAgentService _agentservice;
        IEmailService _emailService;
     

        public AgentController()
        {
            _agentservice = new AgentClient();
            _emailService = new EmailClient();
          
        }

        [HttpPost]
        public HttpResponseMessage PostAgentRegistration(HttpRequestMessage request, Agent applicant)
        {
            return GetHttpResponse(request, () =>
            {
                bool isUpdate = false;
                if (applicant.AgentID > 0)
                {
                    isUpdate = true;
                }

                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated && isUpdate == false)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: true);
                    //var userId = 1;
                    //applicant.CreatedBy = userId;
                    //applicant.RecordStatus = "A";

                    //applicant.ModifiedBy = userId;
                    //applicant.ModifiedDate = DateTime.Now;
                    applicant.AnonymousUserYn = "N";
                }
                else if (User.Identity.IsAuthenticated && isUpdate == true)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: true);
                    //var userId = 1;
                    //applicant.ModifiedBy = userId;
                    //applicant.ModifiedDate = DateTime.Now;
                    //applicant.AnonymousUserYn = "N";

                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    applicant.CreatedBy = anonymousUserId; //1;//This id has to be set to anonymous user id.
                    applicant.ModifiedBy = anonymousUserId; //1; 
                    applicant.ModifiedDate = DateTime.Now;
                    applicant.RecordStatus = "A";
                    applicant.AnonymousUserYn = "Y";

                }
                long agentCreatedId = 0;
                using (IAgentService ls = new AgentClient())
                {
                    agentCreatedId = ls.RegisterAgent(applicant);
                }
               // long agentCreatedId = _agentservice.RegisterAgent(applicant);
                if (agentCreatedId > 0 && isUpdate == false)
                {
                    // _emailService.SendEmail(applicant.AuthorizedContactPerson.EmailID, "Dear " + applicant.RegisteredName + ", Registration successful");
                }

                response = request.CreateResponse<long>(HttpStatusCode.Created, agentCreatedId);
                return response;
            });
        }
        /// <summary>
        /// To Get Agent Registration details in edit and view screens
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetAgent(HttpRequestMessage request, int id)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Agent applicantDetails = _agentservice.GetAgent(id);
                response = request.CreateResponse(HttpStatusCode.OK, applicantDetails);

                return response;
            });

        }
        /// <summary>
        /// To Get Agent Registration details in view screens for Pending Task View Screens of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public HttpResponseMessage GetzAgent(HttpRequestMessage request, string vcn)
        {
            //int id = 0;
            //if (!string.IsNullOrEmpty(vcn))
            //{
            //    id = Convert.ToInt32(vcn);
            //}
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Agent applicantDetails = _agentservice.GetzAgent(vcn);
               response = request.CreateResponse(HttpStatusCode.OK, applicantDetails);
               // response = request.CreateResponse<List<Agent>>(HttpStatusCode.OK, applicantDetails);

                return response;
            });

        }
        /// <summary>
        /// To Get Agent Registration details Agent Master Grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public HttpResponseMessage GetAgents(HttpRequestMessage request, string status)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AgentVO> agents = _agentservice.GetAgents(status);
                response = request.CreateResponse<List<AgentVO>>(HttpStatusCode.OK, agents);
                return response;
            });
        }
        /// <summary>
        /// To Verify Agent Registration details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetVerifyAgent(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Agent applicantDetails = _agentservice.VerifyAgent(id);
                response = request.CreateResponse(HttpStatusCode.OK, applicantDetails);

                return response;
            });
        }
        /// <summary>
        /// To Reject Agent Registration details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetRejectAgent(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool success = _agentservice.PutRejectAgent(id);
                if (success)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, success);

                }

                return response;
            });
        }
        /// <summary>
        /// To Inactive Agent Registration details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage InactiveAgent(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool success = _agentservice.InactiveAgent(id);
                if (success)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, success);

                }

                return response;
            });
        }
        /// <summary>
        /// To Uplaod the files
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> FileUpload(HttpRequestMessage request)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);

            // Read the form data.
            return Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
            {
                //var filedata = new StringBuilder();
                var files = new List<string>();
                foreach (MultipartFileData file in provider.FileData)
                {
                    string prefix = provider.FormData["Prefix"].ToString();
                    if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                    }
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    files.Add(fileName);
                    File.Copy(file.LocalFileName, Path.Combine(root, prefix + "_" + fileName), true);
                    File.Delete(Path.Combine(root, file.LocalFileName));

                }
                var response = Request.CreateResponse(HttpStatusCode.OK, files);
                return response;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        /// <summary>
        /// To Docuemtns types in the agent registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage GetDocumentTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> subCategoryDocTypeDetails = _agentservice.GetDocumentTypes();
                response = request.CreateResponse(HttpStatusCode.OK, subCategoryDocTypeDetails);
                return response;
            });
        }
        /// <summary>
        /// To check Reg. No. whether existed or not for uniqueness
        /// </summary>
        /// <param name="request"></param>
        /// <param name="regNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckForRegCombinationExistance(HttpRequestMessage request, string regNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _agentservice.CheckForRegCombinationExistance(regNo);
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });

        }
        /// <summary>
        /// To check Tax No. whether existed or not for uniqueness
        /// </summary>
        /// <param name="request"></param>
        /// <param name="incTaxNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckForTaxCombinationExistance(HttpRequestMessage request, string incTaxNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _agentservice.CheckForTaxCombinationExistance(incTaxNo);
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });

        }
        /// <summary>
        /// To check Vat No. whether existed or not for uniqueness
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vatNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckForVatCombinationExistance(HttpRequestMessage request, string vatNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _agentservice.CheckForVatCombinationExistance(vatNo);
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });

        }

        [HttpGet]
        public HttpResponseMessage GetAllAgents(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var agents = _agentservice.GetAllAgents();
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, agents);

                return response;
            });
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// To Verify of the Anonymous Agent Registration of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage VerifyAgentRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IAgentService ls = new AgentClient())
                {
                    ls.VerifyAgentRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// To Approval of the Anonymous Agent Registration of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ApproveAgentRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IAgentService ls = new AgentClient())
                {
                    ls.ApproveAgentRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// To Reject of the Anonymous Agent Registration of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RejectAgentRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IAgentService ls = new AgentClient())
                {
                    ls.RejectAgentRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

        [Route("api/GetAgentDetailsInVesselCallByVCN/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetAgentDetailsInVesselCallByVcn(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AgentVO agent = _agentservice.GetAgentDetailsInVesselCallByVcn(vcn);
                response = request.CreateResponse<AgentVO>(HttpStatusCode.OK, agent);

                return response;
            });
        }



        [Route("api/GetAgentAccountport")]
        [HttpGet]
        public HttpResponseMessage Getagentaccountport(HttpRequestMessage request, int agent)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<PortCodeNameVO> agentlist = _agentservice.GetAgentportbasedAccount(agent);
                response = request.CreateResponse<List<PortCodeNameVO>>(HttpStatusCode.OK, agentlist);

                return response;
            });
        }


        [Route("api/GetAgentAccountdetails")]
        [HttpGet]
        public HttpResponseMessage LoadAgentAccountDetails(HttpRequestMessage request, int agent)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<AgentAccountVO> agentlist = _agentservice.GetAgentAccountDetails(agent);
                response = request.CreateResponse<List<AgentAccountVO>>(HttpStatusCode.OK, agentlist);

                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage PostAgentAccountDetails(HttpRequestMessage request, AgentVO agentAccountDetails)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int agentAccountID = _agentservice.AddAgentAccountDetails(agentAccountDetails);
                response = request.CreateResponse<int>(HttpStatusCode.Created, agentAccountID);
                return response;
            });
        }

        /// <summary>
        /// For Dispose of the services
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _agentservice.Dispose();
                _emailService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
