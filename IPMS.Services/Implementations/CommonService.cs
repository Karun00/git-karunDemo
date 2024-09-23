using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
namespace IPMS.Services
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CommonService : ServiceBase,ICommonService
    {
       private readonly IUnitOfWork _unitOfWork;
       
        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CommonService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }


       public List<SubCategory> GetSubCategories(string SupCatCode)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               var listSubCategory = (from ad in _unitOfWork.Repository<SubCategory>().Query().Select()
                                      where ad.SupCatCode == SupCatCode
                                select ad);

               return listSubCategory.ToList();
           });
       }
    }
}
