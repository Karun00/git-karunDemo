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
using IPMS.Repository;
using IPMS.Domain;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PortService : ServiceBase, IPortService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IAccountRepository _accountRepository;
        public PortService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public PortService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public Port AddPort(Port portData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                portData.ObjectState = ObjectState.Added;
                portData.CreatedDate = DateTime.Now;
                portData.CreatedBy = _UserId;
                portData.ModifiedBy = _UserId;
                portData.RecordStatus = "A";
                portData.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<Port>().Insert(portData);
                _unitOfWork.SaveChanges();
                return portData;
            });
        }

        public Port ModifyPort(Port portData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                portData.ObjectState = ObjectState.Added;
                portData.ModifiedDate = DateTime.Now;
                portData.ModifiedBy = _UserId;
                _unitOfWork.Repository<Port>().Update(portData);
                _unitOfWork.SaveChanges();
                return portData;
            });
        }

        public Port DelPort(long id)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var portObj = _unitOfWork.Repository<Port>().Find(id);
                portObj.RecordStatus = "I";
                portObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Port>().Update(portObj);
                _unitOfWork.SaveChanges();

                return portObj;

            });


        }
        //public Port UpdtPort(long id)
        //{
        //    var portObj = _unitOfWork.Repository<Port>().Find(id);
        //    portObj.ObjectState = ObjectState.Modified;
        //    _unitOfWork.Repository<Port>().Update(portObj);
        //    _unitOfWork.SaveChanges();
        //    return portObj;
        //}
        public Port GetPortId(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var portid = _unitOfWork.Repository<Port>().Queryable().Where(x => x.PortCode == code).FirstOrDefault<Port>();
                return portid;
            });
        }


        public List<Port> GetLoginPort()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var ports = _unitOfWork.Repository<Port>().Queryable().Where(x => x.PortCode == _PortCode);
                return ports.ToList<Port>();
            });
        }

        public List<Port> GetPorts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var ports = _unitOfWork.Repository<Port>().Queryable().Where(x=>x.RecordStatus==RecordStatus.Active).OrderBy(x=>x.PortName);
                //By chandrima Das
                //TODO:Let me know the Reason to comment the below Line from Chandrima
                //Below line uncommented by Ramakrishna
                return ports.ToList<Port>();
            });
        }
    }
}
