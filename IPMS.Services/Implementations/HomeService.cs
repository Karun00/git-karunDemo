using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;

namespace IPMS.Services
{
    public class HomeService : IHomeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public HomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    
    }
}
