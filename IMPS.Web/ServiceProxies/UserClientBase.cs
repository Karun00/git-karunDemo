using IPMS.Core.Repository.Exceptions;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Diagnostics;
using System.Globalization;
using System.Configuration;

namespace IPMS.Web.ServiceProxies
{
    public class UserClientBase<T> : ClientBase<T>, IDisposable where T : class
    {
        private readonly ILog log;

        public T ClientChannel
        {
            get
            {
                return this.Channel;
            }
        }

        public UserClientBase()
        {
            XmlConfigurator.Configure();
            //Get logger
            log = LogManager.GetLogger(typeof(UserClientBase<T>));
        }



        protected TResult ExecuteCommand<TResult>(Func<T, TResult> command)
        {
            var proxy = new UserClientBase<T>();
            try
            {
                if (command != null)
                {
                }
                var result = command.Invoke(proxy.ClientChannel);

                return result;

            }
            catch (Exception ex)
            {
                proxy.Abort();
                log.Error("Exception = ", ex);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
        }

        protected T WrapOperationWithException<T>(Func<T> codetoExecute)
        {
            T response = default(T);
            try
            {
                AddIpmsContextToHeader();

                if (codetoExecute != null)
                {
                    // Added by Bhoji Reddy
                    Stopwatch wfstopwatch;
                    wfstopwatch = Stopwatch.StartNew();
                    response = codetoExecute.Invoke();
                    wfstopwatch.Stop();
                    string method = codetoExecute.Target.ToString() + " " + codetoExecute.Method.ToString();
                    //log.Debug("Time Taken at UserClientBase for " + method + " : " + wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }


                //this.Close();
            }
            catch (BusinessExceptions e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(e.Message);
            }
            catch (CommunicationException e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(e.Message);
            }
            catch (TimeoutException e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            catch (Exception e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            return response;
        }

        protected void WrapOperationWithException(Action codetoExecute)
        {
            try
            {

                AddIpmsContextToHeader();

                if (codetoExecute != null)
                {
                    // Added by Bhoji Reddy
                    Stopwatch wfstopwatch;
                    wfstopwatch = Stopwatch.StartNew();
                    codetoExecute.Invoke();
                    wfstopwatch.Stop();
                    string method = codetoExecute.Target.ToString() + " " + codetoExecute.Method.ToString();
                    //log.Debug("Time Taken at UserClientBase for " + method + " : " + wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }


                //this.Close();
            }
            catch (BusinessExceptions e)
            {
                this.Abort();
                throw new FaultException(e.Message);
            }
            catch (CommunicationException e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(e.Message);
            }
            catch (TimeoutException e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            catch (Exception e)
            {
                this.Abort();
                log.Error("Exception = ", e);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
        }


        private void AddIpmsContextToHeader()
        {
            OperationContextScope contextScope =
                              new OperationContextScope(InnerChannel);

            if (OperationContext.Current.OutgoingMessageHeaders.FindHeader("LoginName", "System") == -1)
            {
                //string userName = null;
                string userName = Thread.CurrentPrincipal.Identity.Name;
                // For External Agent and Lince Registrations
                if (string.IsNullOrEmpty(userName))
                    userName = "anuuser";

                MessageHeader<string> header = new MessageHeader<string>(userName);

                OperationContext.Current.OutgoingMessageHeaders.Add(
                                          header.GetUntypedHeader("LoginName", "System"));
                log.Debug("client base " + userName);
            }

            if (OperationContext.Current.OutgoingMessageHeaders.FindHeader("PortCode", "System") == -1)
            {
                HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies["Port"];
                if (authCookie != null)
                {
                    string portcode = authCookie.Value;
                    StringBuilder inSb = new StringBuilder(portcode);
                    StringBuilder outSb = new StringBuilder(portcode.Length);
                    char c;
                    for (int i = 0; i < portcode.Length; i++)
                    {
                        c = inSb[i];
                        c = (char)(c ^ 6); /// remember to use the same XORkey value you used in javascript
                        outSb.Append(c);
                    }
                    portcode = outSb.ToString();
                    MessageHeader<string> headerport = new MessageHeader<string>(portcode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(
                                         headerport.GetUntypedHeader("PortCode", "System"));
                    // }
                }
            }

        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this.State == CommunicationState.Faulted)
                    {
                        this.Abort();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch
                {
                    this.Abort();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        //public void Dispose()
        //{
        //    try
        //    {
        //        if (this.State == CommunicationState.Faulted)
        //        {
        //            this.Abort();
        //        }
        //        else
        //        {
        //            this.Close();
        //        }
        //    }
        //    catch
        //    {
        //        this.Abort();
        //    }
        //}
    }
}

