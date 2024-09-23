
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Adapters;
using IPMS.Web.Api;
using Microsoft.AspNet.SignalR;
using System.Threading;
using WebMatrix.WebData;

namespace IPMS.Web.Controllers.ChatHubs
{
    public class ChatHub : Hub
    {
        public void Send(string userName, string Message)
        { 
            string userName1 = Thread.CurrentPrincipal.Identity.Name;
            Clients.User(userName).broadcastMessage(userName1, Message);
            
        }

        public void Show()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.BrodcastNews();
          
        }
    }



    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            string userName = Thread.CurrentPrincipal.Identity.Name;
            return userName;
        }
    }
}

