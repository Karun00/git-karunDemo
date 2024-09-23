using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System.Data.SqlClient;
using System.Globalization;


namespace IPMS.Repository
{
    public class MobileScheduledTasksRepository : IMobileScheduledTasksRepository
    {
        private IUnitOfWork _unitOfWork;
        //private readonly ILog log;

        public MobileScheduledTasksRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = LogManager.GetLogger(typeof(MobileScheduledTasksRepository));

        }

        public ResourceAllocationVO GetScheduleTaskDetailsById(string resourceAllocationId)
        {

            var resourceAlcId = new SqlParameter("@presourceallocationid", Convert.ToInt32(resourceAllocationId, CultureInfo.InvariantCulture));

            var scheduledTasks = _unitOfWork.SqlQuery<ResourceAllocationVO>("dbo.usp_GetMobileTasksDetails @presourceallocationid", resourceAlcId).ToList();

            return scheduledTasks[0];

        }

        public Entity GetEntities(string entityCode)
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                          where e.EntityCode == entityCode
                          select e).FirstOrDefault<Entity>();
            return entity;
        }



        public CompanyVO GetUserDetails(int UserId)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == UserId
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }

    }
}
