using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using Core.Repository;
using IPMS.Domain.ValueObjects;
using System.Globalization;


namespace IPMS.Repository
{
    public class PortEntryPassApplicationRepository : IPortEntryPassApplicationRepository
    {
        private IUnitOfWork _unitOfWork;

        public PortEntryPassApplicationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PermitRequest> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl)
        {


            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);
            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo) &&
                                p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&
                                (p.permitStatus == "PSCC" || p.permitStatus == "PSCA" && p.permitStatus != "PSAA")).Tracking(true)
                            .Include(p => p.PermitRequestDocuments)

                             .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.PermitReasons)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                            .Include(p => p.VehiclePermitRequirementCodes)
                            .Include(p => p.PermitRequestAccessGates)
                             .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode && p.permitStatus == "PSCC" || p.permitStatus == "PSCA" && p.permitStatus != "PSAA"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }


        public List<PermitRequest> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl)
        {

            // .Query(t => t.VCN == vcn).Tracking(true)
            //andataAll = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y" && t.CreatedBy == userId).Include(t => t.SubCategory3)

            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo)
                                && p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&
                                p.permitStatus != "IEPA" && p.permitStatus != "IEMN" && p.PermitRequestTypeCode != "APCG").Tracking(true)
                         .Include(p => p.PermitRequestDocuments)
                        .Include(p => p.IndividualPersonalPermits)
                        .Include(p => p.IndividualPermitApplicationDetails)
                        .Include(p => p.IndividualVehiclePermits)
                        .Include(p => p.PermitReasons)
                        .Include(p => p.PermitRequestAreas)
                        .Include(p => p.PermitRequestSubAreas)
                        .Include(p => p.ContractorPermitApplicationDetails)
                        .Include(p => p.ContractorPermitEmployeeDetails)
                        .Include(p => p.Port)
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                         .Include(p => p.SubCategory)
                         .Include(p => p.SubCategory1)
                         .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                        .Select()
                         // where
                         //  p.PermitRequestID > 1000 &&
                         // p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode && p.permitStatus != "IEPA" && p.permitStatus != "IEMN" && p.PermitRequestTypeCode != "APCG"
                         orderby p.CreatedDate descending
                         select p


                  );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl)
        {
            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo) &&
                                p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&
                               p.permitStatus == "5IEMN").Tracking(true)
                             .Include(p => p.PermitRequestDocuments)
                            .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.PermitReasons)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                              .Include(p => p.PermitRequestVerifyedDetails)
                             .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                             .Select()
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetApprovedPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl)
        {
            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);


            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo) &&
                                p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&
                               (p.permitStatus == "PSOA" || p.permitStatus == "IEPA" || p.permitStatus == "PAAD" || p.permitStatus == "PRIC")).Tracking(true)

                             .Include(p => p.PermitRequestDocuments)
                               .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.PermitReasons)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                              .Include(p => p.PermitRequestVerifyedDetails)
                             .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                             .Select()
                         // where p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode && p.permitStatus == "PSOA" || p.permitStatus == "IEPA" || p.permitStatus == "PAAD" || p.permitStatus == "PRIC"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetInternalEmployeePermittobeapprovedlistSearch(PermitRequestSearchVO Searchmdl)
        {
            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);

               var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo) &&
                                p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&p.permitStatus == "IEMN" ).Tracking(true)
                             .Include(p => p.PermitRequestDocuments)
                             .Include(p => p.PersonalPermits)
                             .Include(p => p.PermitRequestAreas)
                             .Include(p => p.PermitRequestContractors)
                             .Include(p => p.VehiclePermits)
                             .Include(p => p.VisitorPermits)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                              .Include(p => p.PermitRequestVerifyedDetails)
                             .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                             .Include(p => p.WharfVehiclePermits).Select()
                        // where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "IEMN"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl)
        {

            Searchmdl.RequestTo = Searchmdl.RequestTo.AddDays(1);



            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => (p.CreatedDate >= Searchmdl.RequestFrom && p.CreatedDate <= Searchmdl.RequestTo) &&
                                p.RecordStatus == "A" && p.PortCode == Searchmdl.PortCode &&
                               p.permitStatus == "PSCC" || p.permitStatus == "PSAA" && p.permitStatus != "PSCA").Tracking(true)
                              .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.PermitReasons)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                             .Include(p => p.VehiclePermitRequirementCodes)
                            .Include(p => p.PermitRequestAccessGates)
                             .Include(p => p.WharfVehiclePermits).Select()
                         //where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "PSCC" || p.permitStatus == "PSAA" && p.permitStatus != "PSCA"
                         orderby p.CreatedDate descending
                         select p
                      );
            //SAPS
            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }



        public List<PermitRequest> GetPortEntryPassRequestlist(string portcode)
        {

            // .Query(t => t.VCN == vcn).Tracking(true)
            //andataAll = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y" && t.CreatedBy == userId).Include(t => t.SubCategory3)

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query(p => p.PermitRequestID > 1200 && p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus != "IEPA" && p.permitStatus != "IEMN" && p.PermitRequestTypeCode != "APCG").Tracking(true)
                         .Include(p => p.PermitRequestDocuments)
                        .Include(p => p.IndividualPersonalPermits)
                        .Include(p => p.IndividualPermitApplicationDetails)
                        .Include(p => p.IndividualVehiclePermits)
                        .Include(p => p.PermitReasons)
                        .Include(p => p.PermitRequestAreas)
                        .Include(p => p.PermitRequestSubAreas)
                        .Include(p => p.ContractorPermitApplicationDetails)
                        .Include(p => p.ContractorPermitEmployeeDetails)
                        .Include(p => p.Port)
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                         .Include(p => p.SubCategory)
                         .Include(p => p.SubCategory1)
                         .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                        .Select()
                         where
                         p.PermitRequestID > 1000 &&
                         p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus != "IEPA" && p.permitStatus != "IEMN" && p.PermitRequestTypeCode != "APCG"
                         orderby p.CreatedDate descending
                         select p

            /*
        var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                         .Include(p => p.PermitRequestDocuments)
                        .Include(p =>p.IndividualPersonalPermits)
                        .Include(p=>p.IndividualPermitApplicationDetails)
                        .Include(p => p.IndividualVehiclePermits)
                        .Include(p => p.PermitReasons)
                        .Include(p => p.PermitRequestAreas)
                        .Include(p => p.PermitRequestSubAreas)
                        .Include(p => p.ContractorPermitApplicationDetails)
                        .Include(p => p.ContractorPermitEmployeeDetails)
                        .Include(p=>p.Port)
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                         .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                         .Include(p=>p.SubCategory)
                         .Include(p=>p.SubCategory1)
                         .Include(p=> p.PermitRequestVerifyedDetails.Select(w=>w.PermitRequestVerifyedDocuments))
                        .Select()
                     where
                     p.PermitRequestID > 1000 &&
                     p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus != "IEPA" && p.permitStatus != "IEMN" && p.PermitRequestTypeCode != "APCG"
                     orderby p.CreatedDate descending
                     select p
                     */
                  );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetPortEntryPassRequestlistForSsa(string portcode)
        {

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                             .Include(p => p.PermitRequestDocuments)
                             //.Include(p => p.PersonalPermits)
                             //.Include(p => p.PermitRequestAreas)
                             //.Include(p => p.PermitRequestContractors)
                             //.Include(p => p.VehiclePermits)
                             //.Include(p => p.VisitorPermits)
                             .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.PermitReasons)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                            .Include(p => p.VehiclePermitRequirementCodes)
                            .Include(p => p.PermitRequestAccessGates)
                             .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "PSCC" || p.permitStatus == "PSCA" && p.permitStatus != "PSAA"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetPortEntryPassRequestlistForSaps(string portcode)
        {

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                             //.Include(p => p.PermitRequestDocuments)
                             //.Include(p => p.PersonalPermits)
                             //.Include(p => p.PermitRequestAreas)
                             //.Include(p => p.PermitRequestContractors)
                             //.Include(p => p.VehiclePermits)
                             //.Include(p => p.VisitorPermits)
                              .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.PermitReasons)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                             .Include(p => p.VehiclePermitRequirementCodes)
                            .Include(p => p.PermitRequestAccessGates)
                             .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "PSCC" || p.permitStatus == "PSAA" && p.permitStatus != "PSCA"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }

        public PermitRequest GetPortEntryPassRequest(string refrencenumber, int flag, string portcode)
        {
            PermitRequest query = new PermitRequest();
            if (flag == 1)
            {
                query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                                .Include(p => p.PermitRequestDocuments)
                                .Include(p => p.IndividualPermitApplicationDetails)
                                .Include(p => p.IndividualPersonalPermits)
                                .Include(p => p.IndividualVehiclePermits)
                                .Include(p => p.ContractorPermitApplicationDetails)
                                .Include(p => p.ContractorPermitEmployeeDetails)
                                .Include(p => p.PermitRequestAreas)
                                .Include(p => p.PermitRequestSubAreas)
                                .Include(p => p.PermitReasons)
                             //.Include(p => p.PersonalPermits)
                             //.Include(p => p.PermitRequestAreas)
                             //.Include(p => p.PermitRequestContractors)
                             //.Include(p => p.VehiclePermits)
                             //.Include(p => p.VisitorPermits)
                                .Include(p => p.Port)
                                .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                                .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                                .Include(p => p.SubCategory)
                                .Include(p => p.SubCategory1)
                             //.Include(p => p.VehiclePermitRequirementCodes)
                             //.Include(p => p.PermitRequestAccessGates)
                             //    .Include(p => p.WharfVehiclePermits)
                            .Select()
                         where p.RecordStatus == "A" && p.ReferenceNo == refrencenumber && p.PortCode == portcode && p.permitStatus == "PRRS"
                         orderby p.CreatedDate descending
                         select p
                         ).FirstOrDefault<PermitRequest>();

            }
            else if (flag == 2)
            {
                query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                              .Include(p => p.PermitRequestDocuments)
                             //.Include(p => p.PersonalPermits)
                             //.Include(p => p.PermitRequestAreas)
                             //.Include(p => p.PermitRequestContractors)
                             //.Include(p => p.VehiclePermits)
                             //.Include(p => p.VisitorPermits)
                               .Include(p => p.IndividualPermitApplicationDetails)
                              .Include(p => p.IndividualPersonalPermits)
                              .Include(p => p.IndividualVehiclePermits)
                              .Include(p => p.ContractorPermitApplicationDetails)
                              .Include(p => p.ContractorPermitEmployeeDetails)
                              .Include(p => p.PermitRequestAreas)
                              .Include(p => p.PermitRequestSubAreas)
                              .Include(p => p.PermitReasons)
                              .Include(p => p.Port)
                              .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                              .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                              .Include(p => p.SubCategory)
                              .Include(p => p.SubCategory1)
                              .Include(p => p.VehiclePermitRequirementCodes)
                              .Include(p => p.PermitRequestAccessGates)
                              .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.ReferenceNo == refrencenumber && p.PortCode == portcode && p.permitStatus == "PERR"
                         orderby p.CreatedDate descending
                         select p
                       ).FirstOrDefault<PermitRequest>();

            }
            // var permitrequestlist = query.ToList<PermitRequest>();
            // return permitrequestlist;
            return query;

        }


        public List<PermitRequest> GetApprovedPortEntryPassRequestlist(string portcode)
        {

            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                             .Include(p => p.PermitRequestDocuments)
                             //.Include(p => p.PersonalPermits)
                             //.Include(p => p.PermitRequestAreas)
                             //.Include(p => p.PermitRequestContractors)
                             //.Include(p => p.VehiclePermits)
                             //.Include(p => p.VisitorPermits)
                               .Include(p => p.IndividualPermitApplicationDetails)
                            .Include(p => p.IndividualPersonalPermits)
                            .Include(p => p.IndividualVehiclePermits)
                            .Include(p => p.ContractorPermitApplicationDetails)
                            .Include(p => p.ContractorPermitEmployeeDetails)
                            .Include(p => p.PermitRequestAreas)
                            .Include(p => p.PermitRequestSubAreas)
                            .Include(p => p.PermitReasons)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                              .Include(p => p.PermitRequestVerifyedDetails)
                             //   .Include(p => p.VehiclePermitRequirementCodes)
                             // .Include(p => p.PermitRequestAccessGates)
                             .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                             //.Include(p => p.WharfVehiclePermits)
                             .Select()
                         where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "PSOA" || p.permitStatus == "IEPA" || p.permitStatus == "PAAD" || p.permitStatus == "PRIC"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }


        public List<PermitRequest> GetInternalEmployeePermitlist(string portcode)
        {

            //var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
            //                 .Include(p => p.PermitRequestDocuments)
            //                 //.Include(p => p.PersonalPermits)
            //                 //.Include(p => p.PermitRequestAreas)
            //                 //.Include(p => p.PermitRequestContractors)
            //                 //.Include(p => p.VehiclePermits)
            //                 //.Include(p => p.VisitorPermits)
            //                   .Include(p => p.IndividualPermitApplicationDetails)
            //                .Include(p => p.IndividualPersonalPermits)
            //                .Include(p => p.IndividualVehiclePermits)
            //                .Include(p => p.ContractorPermitApplicationDetails)
            //                .Include(p => p.ContractorPermitEmployeeDetails)
            //                .Include(p => p.PermitRequestAreas)
            //                .Include(p => p.PermitRequestSubAreas)
            //                .Include(p => p.PermitReasons)
            //                 .Include(p => p.Port)
            //                 .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
            //                 .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
            //                 .Include(p => p.SubCategory)
            //                 .Include(p => p.SubCategory1)
            //                  .Include(p => p.PermitRequestVerifyedDetails)
            //                 .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
            //                 //.Include(p => p.WharfVehiclePermits)
            //                 .Select()
            //             where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "5IEMN"
            //             orderby p.CreatedDate descending
            //             select p
            //          );

            // var permitrequestlist = query.ToList<PermitRequest>();
            var permitrequestlist = new List<PermitRequest>();
            return permitrequestlist;

        }

        public List<PermitRequest> GetInternalEmployeePermittobeapprovedlist(string portcode)
        {
            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                             .Include(p => p.PermitRequestDocuments)
                             .Include(p => p.PersonalPermits)
                             .Include(p => p.PermitRequestAreas)
                             .Include(p => p.PermitRequestContractors)
                             .Include(p => p.VehiclePermits)
                             .Include(p => p.VisitorPermits)
                             .Include(p => p.Port)
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                             .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                             .Include(p => p.SubCategory)
                             .Include(p => p.SubCategory1)
                              .Include(p => p.PermitRequestVerifyedDetails)
                             .Include(p => p.PermitRequestVerifyedDetails.Select(w => w.PermitRequestVerifyedDocuments))
                             .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.PortCode == portcode && p.permitStatus == "IEMN"
                         orderby p.CreatedDate descending
                         select p
                      );

            var permitrequestlist = query.ToList<PermitRequest>();
            return permitrequestlist;

        }
        public PermitRequest GetPortEntryPassdetailsByid(string refrencenumber, string portcode)
        {
            var query = (from p in _unitOfWork.Repository<PermitRequest>().Query()
                                .Include(p => p.IndividualPersonalPermits)
                                .Include(p => p.PermitRequestDocuments)
                                .Include(p => p.PersonalPermits)
                                .Include(p => p.PermitRequestAreas)
                                .Include(p => p.PermitRequestContractors)
                                .Include(p => p.VehiclePermits)
                                .Include(p => p.VisitorPermits)
                                .Include(p => p.Port)
                                .Include(p => p.PermitRequestDocuments.Select(d => d.Document))
                                .Include(p => p.PermitRequestDocuments.Select(d => d.Document.SubCategory))
                                .Include(p => p.SubCategory)
                                .Include(p => p.SubCategory1)
                                .Include(p => p.VehiclePermitRequirementCodes)
                                .Include(p => p.PermitRequestAccessGates)
                                .Include(p => p.WharfVehiclePermits).Select()
                         where p.RecordStatus == "A" && p.ReferenceNo == refrencenumber && p.PortCode == portcode
                         select new PermitRequest
                                                     {
                                                         PermitRequestID = p.PermitRequestID,
                                                         Email = p.Email,
                                                         MobileNo = p.MobileNo,
                                                         PermitNO = p.ReferenceNo,
                                                         CreatedDate = p.CreatedDate,
                                                         PortCode = p.Port.PortCode,
                                                         PortName = p.Port.PortName,
                                                         ApplicantFullName = p.ApplicantFullName,
                                                         ApplicantSurName = p.ApplicantSurName,
                                                         ReferenceNo = p.ReferenceNo,
                                                         Remarks = p.PSOremarkes,
                                                         CreatedBy = p.CreatedBy,
                                                         PermitRequestType = p.SubCategory.SubCatName,
                                                         IndividualPersonalPermits = p.IndividualPersonalPermits,
                                                         PermitRequestAreas = p.PermitRequestAreas,
                                                         FromDate = Convert.ToDateTime(GetFromDateTimeByPermits(p.IndividualPersonalPermits)),
                                                         ToDate = Convert.ToDateTime(GetToDateTimeByPermits(p.IndividualPersonalPermits)),
                                                         PermittedAreaName = GetAreaNameByCodesList(p.PermitRequestAreas, p.SubCategory)

                                                     }).FirstOrDefault<PermitRequest>();
            return query;
        }

        private string GetAreaNameByCodesList(ICollection<PermitRequestArea> areas, SubCategory s)
        {
            string areaNames = string.Empty;
            if (areas != null && areas.ToList().Count > 0)
            {
                var subcatName = (from p in _unitOfWork.Repository<SubCategory>().Query()
                                  select new
                                  {
                                      SubCatCode = p.SubCatCode,
                                      SubCatName = p.SubCatName,
                                  });

                foreach (PermitRequestArea permitRequestArea in areas.ToList())
                {
                    if (areaNames == string.Empty)
                    {

                        areaNames = subcatName.Where(b => b.SubCatCode == permitRequestArea.PermitRequestAreaCode).FirstOrDefault().SubCatName;
                    }
                    else
                    {
                        areaNames = subcatName.Where(b => b.SubCatCode == permitRequestArea.PermitRequestAreaCode).FirstOrDefault().SubCatName;
                    }
                }
            }
            return areaNames;
        }

        private string GetFromDateTimeByPermits(ICollection<IndividualPersonalPermit> p)
        {
            if (p != null)
            {
                if (p.ToList().FirstOrDefault().ContractorPerFromDate != null && p.ToList().FirstOrDefault().ContractorPerFromDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().ContractorPerFromDate.ToString();
                }
                if (p.ToList().FirstOrDefault().ContractorTempFromDate != null && p.ToList().FirstOrDefault().ContractorTempFromDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().ContractorTempFromDate.ToString();
                }
                if (p.ToList().FirstOrDefault().TempFromDate != null && p.ToList().FirstOrDefault().TempFromDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().TempFromDate.ToString();
                }
                if (p.ToList().FirstOrDefault().PerFromDate != null && p.ToList().FirstOrDefault().PerFromDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().PerFromDate.ToString();
                }
            }
            return "";
        }


        private string GetToDateTimeByPermits(ICollection<IndividualPersonalPermit> p)
        {
            if (p != null)
            {
                if (p.ToList().FirstOrDefault().ContractorPerToDate != null && p.ToList().FirstOrDefault().ContractorPerToDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().ContractorPerToDate.ToString();
                }
                if (p.ToList().FirstOrDefault().ContractorTempToDate != null && p.ToList().FirstOrDefault().ContractorTempToDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().ContractorTempToDate.ToString();
                }
                if (p.ToList().FirstOrDefault().TempToDate != null && p.ToList().FirstOrDefault().TempToDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().TempToDate.ToString();
                }
                if (p.ToList().FirstOrDefault().PerToDate != null && p.ToList().FirstOrDefault().PerToDate.ToString() != string.Empty)
                {
                    return p.ToList().FirstOrDefault().PerToDate.ToString();
                }
            }
            return "";
        }


        //public PermitRequestVerifyedDetail GetPortEntryPassdetailsByID(string referenceNo, string Portcode)
        //{
        //  var query= from q in _unitOfWork.Repository<PermitRequest>().Query().Select().AsEnumerable<PermitRequest>()
        //                                    join p in _unitOfWork.Repository<PermitRequestVerifyedDetail>().Query().Select().AsEnumerable<PermitRequestVerifyedDetail>() on
        //                                    q.PermitRequestID equals p.permitrRequestID
        //                                    orderby q.CreatedDate descending
        //                                    where q.BerthMaintenanceID == id & p.RecordStatus == "A"
        //}

        public int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag, string portcode)
        {
            int status = 0;

            var query = (from q in _unitOfWork.Repository<PermitRequestVerifyedDetail>().Query().Select().AsEnumerable<PermitRequestVerifyedDetail>()
                         join p in _unitOfWork.Repository<PermitRequest>().Query().Select().AsEnumerable<PermitRequest>() on
                         q.permitrRequestID equals p.PermitRequestID
                         where q.permitrRequestID == id & q.RecordStatus == "A" & q.Flag == flag & p.PortCode == portcode
                         select q).ToList<PermitRequestVerifyedDetail>();


            if (query.Count > 0)
            { status = 1; }
            else
            { status = 0; }

            return status;
        }
        /// <summary>
        /// To Get Entity Details Based on EntitiyCode
        /// </summary>
        /// <param name="_entityCode"></param>
        /// <returns></returns>
        public Entity GetEnties(string _entityCode)
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                          where e.EntityCode == _entityCode
                          select e).FirstOrDefault<Entity>();
            return entity;
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int userid)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userid
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }
    }
}
