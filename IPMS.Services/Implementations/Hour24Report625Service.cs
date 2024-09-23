using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Hour24Report625Service : ServiceBase, IHour24Report625Service
    {
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;
        // private IPortConfigurationRepository _portConfigurationRepository;       
        private ISubCategoryRepository _subcategoryRepository;
        private IHour24AndSection625Repository _hour24andsection625Repository;
        private IAccountRepository _accountRepository;
        private IPortRepository _portRepository;
        private const string _entityCode = EntityCodes.Hour24Report625;

        public Hour24Report625Service(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            //_UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _hour24andsection625Repository = new Hour24AndSection625Repository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            //  _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);           
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
        }


        public Hour24Report625Service()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            //_UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _hour24andsection625Repository = new Hour24AndSection625Repository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            //_portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
        }


        public List<Hour24Report625VO> GetHour24Report625SList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _hour24andsection625Repository.Gethoursreportlist(_PortCode).MapToDTO();
            });
        }

        public Hour24Report625VO AddHour24Report625(Hour24Report625VO hoursereportdata)
        {
            return EncloseTransactionAndHandleException(() =>
         {
             var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

             int userid = 0;
             if (!string.IsNullOrEmpty(_LoginName))
                 userid = _accountRepository.GetUserId(_LoginName);
             else
                 userid = anonymousUserId; // 1;
             Hour24Report625 Hour24Report625 = null;
             Section625ABCD section625abcd = null;
             Section625B section625b = null;
             Section625C section625c = null;
             Section625D section625D = null;
             Section625E section625E = null;
             Section625G section625G = null;
             List<Section625GDetail2> section625gdetail2slist = null;
             //List<Section625GDetail1> section625gdetail1slist = null;
             List<Section625EDetail> section625edetailslist = null;
             List<Section625BUnion> section625bunionlist = null;
             List<Section625CPrevent> section625cpreventlist = null;
             List<Section625CRecommended> section625crecommendedlist = null;
             List<string> recordingofIncidentlist = null;
             List<string> generalagencieslist = null;
             List<string> OccupationalHygieneAgencieslist = null;
             List<string> TypeofContactlist = null;
             List<string> StandardActlist = null;
             List<string> SubstandardConditionlist = null;
             List<string> PersonalFactorslist = null;
             List<string> JobFactorslist = null;
             List<string> ControlStepsToPreventRecurrencelist = null;
             List<string> FireDepartmentlist = null;
             List<string> IncidentClassificationlist = null;
             List<string> DiscriptionofExposedRisklist = null;
             List<string> SectionRecordingofIncidentlist = null;


             hoursereportdata.CreatedDate = DateTime.Now;
             hoursereportdata.ModifiedDate = DateTime.Now;
             hoursereportdata.ModifiedBy = userid;
             hoursereportdata.CreatedBy = userid;
             Entity entity = GetEntities(_entityCode);
             CompanyVO nextStepCompany = new CompanyVO();
             nextStepCompany.UserTypeId = 0;
             nextStepCompany.UserType = "EMP";

             hoursereportdata.CDContactNumber = hoursereportdata.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.CDMobileNumber = hoursereportdata.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

             hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

             //       hoursereportdata.section625b.CDContactNumber = hoursereportdata.section625b.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             //      hoursereportdata.section625b.CDMobileNumber = hoursereportdata.section625b.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

             //      hoursereportdata.Section625BUnion.CDContactNumber = hoursereportdata.Section625BUnion.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             //      hoursereportdata.Section625BUnion.CDMobileNumber = hoursereportdata.Section625BUnion.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

             //hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             //hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

             //hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
             //hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");






             hoursereportdata.section625G.WIContactNo1 = hoursereportdata.section625G.WIContactNo1.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625G.WIContactNo2 = hoursereportdata.section625G.WIContactNo2.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625G.ContactNoOfComplainant = hoursereportdata.section625G.ContactNoOfComplainant.Replace("(", "").Replace(")", "").Replace("-", "");

             hoursereportdata.section625E.MobileNo = hoursereportdata.section625E.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625c.WITelephoneNo1 = hoursereportdata.section625c.WITelephoneNo1.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625c.WITelephoneNo2 = hoursereportdata.section625c.WITelephoneNo2.Replace("(", "").Replace(")", "").Replace("-", "");
             hoursereportdata.section625E.TelephoneNo = hoursereportdata.section625E.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");




             Hour24Report625 = hoursereportdata.MapToEntity();
             section625abcd = hoursereportdata.section625abcd.MapToEntity();
             section625b = hoursereportdata.section625b.MapToEntity();
             section625bunionlist = hoursereportdata.Section625BUnion.MapToEntity();
             section625c = hoursereportdata.section625c.MapToEntity();
             section625cpreventlist = hoursereportdata.Section625CPrevent.MapToEntity();
             section625crecommendedlist = hoursereportdata.Section625CRecommended.MapToEntity();
             section625D = hoursereportdata.section625D.MapToEntity();
             section625E = hoursereportdata.section625E.MapToEntity();
             section625edetailslist = hoursereportdata.Section625EDetail.MapToEntity();
             section625G = hoursereportdata.section625G.MapToEntity();

             section625gdetail2slist = hoursereportdata.Section625GDetail2.MapToEntity();


             recordingofIncidentlist = hoursereportdata.selectedrecordingofIncidentdetails;
             generalagencieslist = hoursereportdata.selectedgeneralagenciesdetails;
             OccupationalHygieneAgencieslist = hoursereportdata.selectedOccupationalHygieneAgencies;
             TypeofContactlist = hoursereportdata.selectedTypeofContact;
             StandardActlist = hoursereportdata.selectedStandardAct;
             SubstandardConditionlist = hoursereportdata.selectedSubstandardCondition;
             PersonalFactorslist = hoursereportdata.selectedPersonalFactors;
             JobFactorslist = hoursereportdata.selectedJobFactors;
             ControlStepsToPreventRecurrencelist = hoursereportdata.selectedControlStepsToPreventRecurrence;
             FireDepartmentlist = hoursereportdata.selectedFireDepartment;
             IncidentClassificationlist = hoursereportdata.selectedIncidentClassification;
             DiscriptionofExposedRisklist = hoursereportdata.selectedDiscriptionofExposedRisk;
             SectionRecordingofIncidentlist = hoursereportdata.selectedRecordingofIncident;

             //hour24 report main Saving------------------------------------------------------------------------------------
             Hour24Report625.ObjectState = ObjectState.Added;
             Hour24Report625.RecordStatus = "A";
             Hour24Report625.Section625ABCD = null;
             Hour24Report625.Section625B = null;
             Hour24Report625.Section625C = null;
             Hour24Report625.Section625D = null;
             Hour24Report625.Section625E = null;
             Hour24Report625.Section625G = null;


             Hour24Report625.Section625BUnion = null;
             Hour24Report625.Section625CPrevent = null;
             Hour24Report625.Section625CRecommended = null;
             Hour24Report625.Section625EDetail = null;
             Hour24Report625.Section625GDetail2 = null;



             _unitOfWork.Repository<Hour24Report625>().Insert(Hour24Report625);
             _unitOfWork.SaveChanges();
             //Section A Saving------------------------------------------------------------------------------------
             //if (Hour24Report625.NONatureCode == "625A")
             //{
             section625abcd.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
             section625abcd.CreatedDate = DateTime.Now;
             section625abcd.ModifiedDate = DateTime.Now;
             section625abcd.ModifiedBy = userid;
             section625abcd.CreatedBy = userid;
             section625abcd.RecordStatus = "A";
             section625abcd.ObjectState = ObjectState.Added;
             _unitOfWork.Repository<Section625ABCD>().Insert(section625abcd);
             _unitOfWork.SaveChanges();
             //}

             //Section B Saving------------------------------------------------------------------------------------
             if (Hour24Report625.NONatureCode == "625B")
             {

                 section625b.Section625ABCDID = section625abcd.Section625ABCDID;
                 section625b.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                 section625b.CreatedDate = DateTime.Now;
                 section625b.ModifiedDate = DateTime.Now;
                 section625b.ModifiedBy = userid;
                 section625b.CreatedBy = userid;
                 section625b.RecordStatus = "A";
                 section625b.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625B>().Insert(section625b);
                 _unitOfWork.SaveChanges();


                 foreach (var section625bunion in section625bunionlist)
                 {

                     section625bunion.Section625BID = section625b.Section625BID;
                     section625bunion.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                 }
                 //section625bunionlist.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625BUnion>().InsertRange(section625bunionlist);
                 _unitOfWork.SaveChanges();
             }

             if (Hour24Report625.NONatureCode == "625C")
             {
                 //Section c Saving-----------------------------------------------------------------------------------
                 section625c.Section625ABCDID = section625abcd.Section625ABCDID;
                 section625c.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                 section625c.CreatedDate = DateTime.Now;
                 section625c.ModifiedDate = DateTime.Now;
                 section625c.ModifiedBy = userid;
                 section625c.CreatedBy = userid;
                 section625c.RecordStatus = "A";
                 section625c.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625C>().Insert(section625c);
                 _unitOfWork.SaveChanges();
                 if (section625cpreventlist.Count > 0)
                 {

                     foreach (var section625cprevent in section625cpreventlist)
                     {
                         section625cprevent.Section625CID = section625c.Section625CID;
                         section625cprevent.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cprevent.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CPrevent>().Insert(section625cprevent);
                         _unitOfWork.SaveChanges();
                     }

                 }
                 if (section625crecommendedlist.Count > 0)
                 {
                     foreach (var section625crecommended in section625crecommendedlist)
                     {

                         section625crecommended.Section625CID = section625c.Section625CID;
                         section625crecommended.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                         section625crecommended.ObjectState = ObjectState.Added;


                         _unitOfWork.Repository<Section625CRecommended>().Insert(section625crecommended);
                         _unitOfWork.SaveChanges();

                     }
                 }

                 if (recordingofIncidentlist.Count > 0)
                 {
                     foreach (var recordingofIncident in recordingofIncidentlist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = recordingofIncident;
                         section625cdetails.GroupCode = "6CRE";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }

                 if (generalagencieslist.Count > 0)
                 {
                     foreach (var generalagencies in generalagencieslist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = generalagencies;
                         section625cdetails.GroupCode = "6CGE";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }

                 if (OccupationalHygieneAgencieslist.Count > 0)
                 {
                     foreach (var OccupationalHygieneAgencies in OccupationalHygieneAgencieslist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = OccupationalHygieneAgencies;
                         section625cdetails.GroupCode = "6COC";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (TypeofContactlist.Count > 0)
                 {
                     foreach (var TypeofContact in TypeofContactlist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = TypeofContact;
                         section625cdetails.GroupCode = "6CTC";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (StandardActlist.Count > 0)
                 {
                     foreach (var StandardAct in StandardActlist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = StandardAct;
                         section625cdetails.GroupCode = "6CAC";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (SubstandardConditionlist.Count > 0)
                 {
                     foreach (var SubstandardCondition in SubstandardConditionlist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = SubstandardCondition;
                         section625cdetails.GroupCode = "6CSC";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (PersonalFactorslist.Count > 0)
                 {
                     foreach (var PersonalFactors in PersonalFactorslist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = PersonalFactors;
                         section625cdetails.GroupCode = "6CPF";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (JobFactorslist.Count > 0)
                 {
                     foreach (var JobFactors in JobFactorslist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = JobFactors;
                         section625cdetails.GroupCode = "6CJF";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }

                 if (ControlStepsToPreventRecurrencelist.Count > 0)
                 {
                     foreach (var ControlStepsToPreventRecurrence in ControlStepsToPreventRecurrencelist)
                     {
                         Section625CDetail section625cdetails = new Section625CDetail();
                         section625cdetails.DetailCode = ControlStepsToPreventRecurrence;
                         section625cdetails.GroupCode = "6CPR";
                         section625cdetails.Section625CID = section625c.Section625CID;
                         section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625cdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
             }

             //Section D Saving-----------------------------------------------------------------------------------
             if (Hour24Report625.NONatureCode == "625D")
             {
                 section625D.Section625ABCDID = section625abcd.Section625ABCDID;
                 section625D.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                 section625D.CreatedDate = DateTime.Now;
                 section625D.ModifiedDate = DateTime.Now;
                 section625D.ModifiedBy = userid;
                 section625D.CreatedBy = userid;
                 section625D.RecordStatus = "A";
                 section625D.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625D>().Insert(section625D);
                 _unitOfWork.SaveChanges();
                 if (FireDepartmentlist.Count > 0)
                 {
                     foreach (var FireDepartment in FireDepartmentlist)
                     {
                         Section625DDetail section625ddetails = new Section625DDetail();
                         section625ddetails.DetailCode = FireDepartment;
                         section625ddetails.GroupCode = "6DFD";
                         section625ddetails.Section625DID = section625D.Section625DID;
                         section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625ddetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (IncidentClassificationlist.Count > 0)
                 {
                     foreach (var IncidentClassification in IncidentClassificationlist)
                     {
                         Section625DDetail section625ddetails = new Section625DDetail();
                         section625ddetails.DetailCode = IncidentClassification;
                         section625ddetails.GroupCode = "6DIC";
                         section625ddetails.Section625DID = section625D.Section625DID;
                         section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625ddetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
                 if (DiscriptionofExposedRisklist.Count > 0)
                 {
                     foreach (var DiscriptionofExposedRisk in DiscriptionofExposedRisklist)
                     {
                         Section625DDetail section625ddetails = new Section625DDetail();
                         section625ddetails.DetailCode = DiscriptionofExposedRisk;
                         section625ddetails.GroupCode = "6DER";
                         section625ddetails.Section625DID = section625D.Section625DID;
                         section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625ddetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                         _unitOfWork.SaveChanges();
                     }
                 }
             }
             //Section E Saving-----------------------------------------------------------------------------------
             if (Hour24Report625.NONatureCode == "625E")
             {
                 section625E.Section625ABCDID = section625abcd.Section625ABCDID;
                 section625E.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                 section625E.CreatedDate = DateTime.Now;
                 section625E.ModifiedDate = DateTime.Now;
                 section625E.ModifiedBy = userid;
                 section625E.CreatedBy = userid;
                 section625E.RecordStatus = "A";
                 section625E.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625E>().Insert(section625E);
                 _unitOfWork.SaveChanges();

                 if (section625edetailslist.Count > 0)
                 {
                     foreach (var section625edetail in section625edetailslist)
                     {

                         section625edetail.Section625EID = section625E.Section625EID;
                         section625edetail.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625edetail.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625EDetail>().Insert(section625edetail);
                         _unitOfWork.SaveChanges();

                     }
                 }
             }


             //Section G Saving-----------------------------------------------------------------------------------
             if (Hour24Report625.NONatureCode == "625G")
             {
                 section625G.Section625ABCDID = section625abcd.Section625ABCDID;
                 section625G.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                 section625G.CreatedDate = DateTime.Now;
                 section625G.ModifiedDate = DateTime.Now;
                 section625G.ModifiedBy = userid;
                 section625G.CreatedBy = userid;
                 section625G.RecordStatus = "A";
                 section625G.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<Section625G>().Insert(section625G);
                 _unitOfWork.SaveChanges();
                 if (section625gdetail2slist.Count > 0)
                 {
                     foreach (var section625gdetail2 in section625gdetail2slist)
                     {

                         section625gdetail2.Section625GID = section625G.Section625GID;
                         section625gdetail2.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625gdetail2.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625GDetail2>().Insert(section625gdetail2);
                         _unitOfWork.SaveChanges();

                     }
                 }
                 if (SectionRecordingofIncidentlist.Count > 0)
                 {
                     foreach (var SectionRecordingofIncident in SectionRecordingofIncidentlist)
                     {
                         Section625GDetail1 section625gdetails = new Section625GDetail1();
                         section625gdetails.RISubCatCode = SectionRecordingofIncident;
                         section625gdetails.Section625GID = section625G.Section625GID;
                         section625gdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                         section625gdetails.ObjectState = ObjectState.Added;
                         _unitOfWork.Repository<Section625GDetail1>().Insert(section625gdetails);
                         _unitOfWork.SaveChanges();
                     }
                 }

             }

             _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.Hour24Report625).EntityID, Hour24Report625.Hour24Report625ID.ToString(CultureInfo.InvariantCulture), 1, nextStepCompany, Hour24Report625.PortCode.ToString(CultureInfo.InvariantCulture), WFStatus.New24HoursincidentReport);

             hoursereportdata = Hour24Report625.MapToDTO();
             return hoursereportdata;
         });

        }




        public Hour24Report625VO EditHour24Report625(Hour24Report625VO hoursereportdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                int userid = 0;
                if (!string.IsNullOrEmpty(_LoginName))
                    userid = _accountRepository.GetUserId(_LoginName);
                else
                    userid = anonymousUserId; // 1; 
                Hour24Report625 Hour24Report625 = null;
                Section625ABCD section625abcd = null;
                Section625B section625b = null;
                Section625C section625c = null;
                Section625D section625D = null;
                Section625E section625E = null;
                Section625G section625G = null;
                List<Section625GDetail2> section625gdetail2slist = null;
                //List<Section625GDetail1> section625gdetail1slist = null;
                List<Section625EDetail> section625edetailslist = null;
                List<Section625BUnion> section625bunionlist = null;
                List<Section625CPrevent> section625cpreventlist = null;
                List<Section625CRecommended> section625crecommendedlist = null;
                List<string> recordingofIncidentlist = null;
                List<string> generalagencieslist = null;
                List<string> OccupationalHygieneAgencieslist = null;
                List<string> TypeofContactlist = null;
                List<string> StandardActlist = null;
                List<string> SubstandardConditionlist = null;
                List<string> PersonalFactorslist = null;
                List<string> JobFactorslist = null;
                List<string> ControlStepsToPreventRecurrencelist = null;
                List<string> FireDepartmentlist = null;
                List<string> IncidentClassificationlist = null;
                List<string> DiscriptionofExposedRisklist = null;
                List<string> SectionRecordingofIncidentlist = null;


                hoursereportdata.CreatedDate = DateTime.Now;
                hoursereportdata.ModifiedDate = DateTime.Now;
                hoursereportdata.ModifiedBy = userid;
                hoursereportdata.CreatedBy = userid;

                hoursereportdata.CDContactNumber = hoursereportdata.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.CDMobileNumber = hoursereportdata.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

                hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

                //       hoursereportdata.section625b.CDContactNumber = hoursereportdata.section625b.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                //      hoursereportdata.section625b.CDMobileNumber = hoursereportdata.section625b.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

                //      hoursereportdata.Section625BUnion.CDContactNumber = hoursereportdata.Section625BUnion.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                //      hoursereportdata.Section625BUnion.CDMobileNumber = hoursereportdata.Section625BUnion.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

                //hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                //hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");

                //hoursereportdata.section625abcd.CDContactNumber = hoursereportdata.section625abcd.CDContactNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                //hoursereportdata.section625abcd.CDMobileNumber = hoursereportdata.section625abcd.CDMobileNumber.Replace("(", "").Replace(")", "").Replace("-", "");






                hoursereportdata.section625G.WIContactNo1 = hoursereportdata.section625G.WIContactNo1.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625G.WIContactNo2 = hoursereportdata.section625G.WIContactNo2.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625G.ContactNoOfComplainant = hoursereportdata.section625G.ContactNoOfComplainant.Replace("(", "").Replace(")", "").Replace("-", "");

                hoursereportdata.section625E.MobileNo = hoursereportdata.section625E.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625c.WITelephoneNo1 = hoursereportdata.section625c.WITelephoneNo1.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625c.WITelephoneNo2 = hoursereportdata.section625c.WITelephoneNo2.Replace("(", "").Replace(")", "").Replace("-", "");
                hoursereportdata.section625E.TelephoneNo = hoursereportdata.section625E.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");




                Hour24Report625 = hoursereportdata.MapToEntity();
                section625abcd = hoursereportdata.section625abcd.MapToEntity();
                section625b = hoursereportdata.section625b.MapToEntity();
                section625bunionlist = hoursereportdata.Section625BUnion.MapToEntity();
                section625c = hoursereportdata.section625c.MapToEntity();
                section625cpreventlist = hoursereportdata.Section625CPrevent.MapToEntity();
                section625crecommendedlist = hoursereportdata.Section625CRecommended.MapToEntity();
                section625D = hoursereportdata.section625D.MapToEntity();
                section625E = hoursereportdata.section625E.MapToEntity();
                section625edetailslist = hoursereportdata.Section625EDetail.MapToEntity();
                section625G = hoursereportdata.section625G.MapToEntity();

                section625gdetail2slist = hoursereportdata.Section625GDetail2.MapToEntity();


                recordingofIncidentlist = hoursereportdata.selectedrecordingofIncidentdetails;
                generalagencieslist = hoursereportdata.selectedgeneralagenciesdetails;
                OccupationalHygieneAgencieslist = hoursereportdata.selectedOccupationalHygieneAgencies;
                TypeofContactlist = hoursereportdata.selectedTypeofContact;
                StandardActlist = hoursereportdata.selectedStandardAct;
                SubstandardConditionlist = hoursereportdata.selectedSubstandardCondition;
                PersonalFactorslist = hoursereportdata.selectedPersonalFactors;
                JobFactorslist = hoursereportdata.selectedJobFactors;
                ControlStepsToPreventRecurrencelist = hoursereportdata.selectedControlStepsToPreventRecurrence;
                FireDepartmentlist = hoursereportdata.selectedFireDepartment;
                IncidentClassificationlist = hoursereportdata.selectedIncidentClassification;
                DiscriptionofExposedRisklist = hoursereportdata.selectedDiscriptionofExposedRisk;
                SectionRecordingofIncidentlist = hoursereportdata.selectedRecordingofIncident;

                //hour24 report main Saving------------------------------------------------------------------------------------
                Hour24Report625.ObjectState = ObjectState.Modified;
                Hour24Report625.RecordStatus = "A";
                Hour24Report625.Section625ABCD = null;
                Hour24Report625.Section625B = null;
                Hour24Report625.Section625C = null;
                Hour24Report625.Section625D = null;
                Hour24Report625.Section625E = null;
                Hour24Report625.Section625G = null;


                Hour24Report625.Section625BUnion = null;
                Hour24Report625.Section625CPrevent = null;
                Hour24Report625.Section625CRecommended = null;
                Hour24Report625.Section625EDetail = null;
                Hour24Report625.Section625GDetail2 = null;



                _unitOfWork.Repository<Hour24Report625>().Update(Hour24Report625);
                _unitOfWork.SaveChanges();
                //Section A Saving------------------------------------------------------------------------------------
                //if (Hour24Report625.NONatureCode == "625A")
                //{
                section625abcd.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                section625abcd.CreatedDate = DateTime.Now;
                section625abcd.ModifiedDate = DateTime.Now;
                section625abcd.ModifiedBy = userid;
                section625abcd.CreatedBy = userid;
                section625abcd.RecordStatus = "A";

                section625abcd.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Section625ABCD>().Update(section625abcd);
                _unitOfWork.SaveChanges();
                //}

                //Section B Saving------------------------------------------------------------------------------------
                if (Hour24Report625.NONatureCode == "625B")
                {

                    section625b.Section625ABCDID = section625abcd.Section625ABCDID;
                    section625b.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                    section625b.CreatedDate = DateTime.Now;
                    section625b.ModifiedDate = DateTime.Now;
                    section625b.ModifiedBy = userid;
                    section625b.CreatedBy = userid;
                    section625b.RecordStatus = "A";
                    section625b.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Section625B>().Update(section625b);
                    _unitOfWork.SaveChanges();


                    foreach (var section625bunion in section625bunionlist)
                    {

                        section625bunion.Section625BID = section625b.Section625BID;
                        section625bunion.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                        if (section625bunion.Section625BUnionID > 0)
                        {
                            section625bunion.ObjectState = ObjectState.Modified;
                            _unitOfWork.Repository<Section625BUnion>().Update(section625bunion);
                        }
                        else
                        {
                            section625bunion.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<Section625BUnion>().Insert(section625bunion);
                        }

                    }
                    ////section625bunionlist.ObjectState = ObjectState.Added;
                    //_unitOfWork.Repository<Section625BUnion>().InsertRange(section625bunionlist);
                    _unitOfWork.SaveChanges();
                }

                if (Hour24Report625.NONatureCode == "625C")
                {
                    //Section c Saving-----------------------------------------------------------------------------------
                    section625c.Section625ABCDID = section625abcd.Section625ABCDID;
                    section625c.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                    section625c.CreatedDate = DateTime.Now;
                    section625c.ModifiedDate = DateTime.Now;
                    section625c.ModifiedBy = userid;
                    section625c.CreatedBy = userid;
                    section625c.RecordStatus = "A";
                    section625c.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Section625C>().Update(section625c);
                    _unitOfWork.SaveChanges();
                    if (section625cpreventlist.Count > 0)
                    {

                        foreach (var section625cprevent in section625cpreventlist)
                        {
                            section625cprevent.Section625CID = section625c.Section625CID;
                            section625cprevent.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625cprevent.Section625CPreventID > 0)
                            {
                                section625cprevent.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CPrevent>().Update(section625cprevent);
                            }
                            else
                            {
                                section625cprevent.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CPrevent>().Insert(section625cprevent);
                            }
                            //section625cprevent.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CPrevent>().Update(section625cprevent);
                            _unitOfWork.SaveChanges();
                        }

                    }
                    if (section625crecommendedlist.Count > 0)
                    {
                        foreach (var section625crecommended in section625crecommendedlist)
                        {

                            section625crecommended.Section625CID = section625c.Section625CID;
                            section625crecommended.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625crecommended.Section625CRecommendedID > 0)
                            {
                                section625crecommended.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CRecommended>().Update(section625crecommended);
                            }
                            else
                            {
                                section625crecommended.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CRecommended>().Insert(section625crecommended);
                            }

                            //section625crecommended.ObjectState = ObjectState.Modified;


                            //_unitOfWork.Repository<Section625CRecommended>().Update(section625crecommended);
                            _unitOfWork.SaveChanges();

                        }
                    }

                    if (recordingofIncidentlist.Count > 0)
                    {
                        foreach (var recordingofIncident in recordingofIncidentlist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = recordingofIncident;
                            section625cdetails.GroupCode = "6CRE";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (generalagencieslist.Count > 0)
                    {
                        foreach (var generalagencies in generalagencieslist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = generalagencies;
                            section625cdetails.GroupCode = "6CGE";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (OccupationalHygieneAgencieslist.Count > 0)
                    {
                        foreach (var OccupationalHygieneAgencies in OccupationalHygieneAgencieslist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = OccupationalHygieneAgencies;
                            section625cdetails.GroupCode = "6COC";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (TypeofContactlist.Count > 0)
                    {
                        foreach (var TypeofContact in TypeofContactlist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = TypeofContact;
                            section625cdetails.GroupCode = "6CTC";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (StandardActlist.Count > 0)
                    {
                        foreach (var StandardAct in StandardActlist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = StandardAct;
                            section625cdetails.GroupCode = "6CAC";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (SubstandardConditionlist.Count > 0)
                    {
                        foreach (var SubstandardCondition in SubstandardConditionlist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = SubstandardCondition;
                            section625cdetails.GroupCode = "6CSC";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (PersonalFactorslist.Count > 0)
                    {
                        foreach (var PersonalFactors in PersonalFactorslist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = PersonalFactors;
                            section625cdetails.GroupCode = "6CPF";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (JobFactorslist.Count > 0)
                    {
                        foreach (var JobFactors in JobFactorslist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = JobFactors;
                            section625cdetails.GroupCode = "6CJF";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (ControlStepsToPreventRecurrencelist.Count > 0)
                    {
                        foreach (var ControlStepsToPreventRecurrence in ControlStepsToPreventRecurrencelist)
                        {
                            Section625CDetail section625cdetails = new Section625CDetail();
                            section625cdetails.DetailCode = ControlStepsToPreventRecurrence;
                            section625cdetails.GroupCode = "6CPR";
                            section625cdetails.Section625CID = section625c.Section625CID;
                            section625cdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625cdetails.Section625CDetailID > 0)
                            {
                                section625cdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            }
                            else
                            {
                                section625cdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625CDetail>().Insert(section625cdetails);
                            }
                            //section625cdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625CDetail>().Update(section625cdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

                //Section D Saving-----------------------------------------------------------------------------------
                if (Hour24Report625.NONatureCode == "625D")
                {
                    section625D.Section625ABCDID = section625abcd.Section625ABCDID;
                    section625D.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                    section625D.CreatedDate = DateTime.Now;
                    section625D.ModifiedDate = DateTime.Now;
                    section625D.ModifiedBy = userid;
                    section625D.CreatedBy = userid;
                    section625D.RecordStatus = "A";
                    section625D.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Section625D>().Update(section625D);
                    _unitOfWork.SaveChanges();
                    if (FireDepartmentlist.Count > 0)
                    {
                        foreach (var FireDepartment in FireDepartmentlist)
                        {
                            Section625DDetail section625ddetails = new Section625DDetail();
                            section625ddetails.DetailCode = FireDepartment;
                            section625ddetails.GroupCode = "6DFD";
                            section625ddetails.Section625DID = section625D.Section625DID;
                            section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625ddetails.Section625DDetailID > 0)
                            {
                                section625ddetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625DDetail>().Update(section625ddetails);
                            }
                            else
                            {
                                section625ddetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                            }
                            _unitOfWork.SaveChanges();
                        }
                    }
                    if (IncidentClassificationlist.Count > 0)
                    {
                        foreach (var IncidentClassification in IncidentClassificationlist)
                        {
                            Section625DDetail section625ddetails = new Section625DDetail();
                            section625ddetails.DetailCode = IncidentClassification;
                            section625ddetails.GroupCode = "6DIC";
                            section625ddetails.Section625DID = section625D.Section625DID;
                            section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625ddetails.Section625DDetailID > 0)
                            {
                                section625ddetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625DDetail>().Update(section625ddetails);
                            }
                            else
                            {
                                section625ddetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                            }
                        }
                    }
                    if (DiscriptionofExposedRisklist.Count > 0)
                    {
                        foreach (var DiscriptionofExposedRisk in DiscriptionofExposedRisklist)
                        {
                            Section625DDetail section625ddetails = new Section625DDetail();
                            section625ddetails.DetailCode = DiscriptionofExposedRisk;
                            section625ddetails.GroupCode = "6DER";
                            section625ddetails.Section625DID = section625D.Section625DID;
                            section625ddetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625ddetails.Section625DDetailID > 0)
                            {
                                section625ddetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625DDetail>().Update(section625ddetails);
                            }
                            else
                            {
                                section625ddetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625DDetail>().Insert(section625ddetails);
                            }
                            _unitOfWork.SaveChanges();
                        }
                    }
                }
                //Section E Saving-----------------------------------------------------------------------------------
                if (Hour24Report625.NONatureCode == "625E")
                {
                    section625E.Section625ABCDID = section625abcd.Section625ABCDID;
                    section625E.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                    section625E.CreatedDate = DateTime.Now;
                    section625E.ModifiedDate = DateTime.Now;
                    section625E.ModifiedBy = userid;
                    section625E.CreatedBy = userid;
                    section625E.RecordStatus = "A";
                    section625E.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Section625E>().Update(section625E);
                    _unitOfWork.SaveChanges();

                    if (section625edetailslist.Count > 0)
                    {
                        foreach (var section625edetail in section625edetailslist)
                        {

                            section625edetail.Section625EID = section625E.Section625EID;
                            section625edetail.Hour24Report625ID = Hour24Report625.Hour24Report625ID;

                            if (section625edetail.Section625EDetailID > 0)
                            {
                                section625edetail.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625EDetail>().Update(section625edetail);
                            }
                            else
                            {
                                section625edetail.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625EDetail>().Insert(section625edetail);
                            }
                            //section625edetail.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625EDetail>().Update(section625edetail);
                            _unitOfWork.SaveChanges();

                        }
                    }
                }


                //Section G Saving-----------------------------------------------------------------------------------
                if (Hour24Report625.NONatureCode == "625G")
                {
                    section625G.Section625ABCDID = section625abcd.Section625ABCDID;
                    section625G.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                    section625G.CreatedDate = DateTime.Now;
                    section625G.ModifiedDate = DateTime.Now;
                    section625G.ModifiedBy = userid;
                    section625G.CreatedBy = userid;
                    section625G.RecordStatus = "A";
                    section625G.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Section625G>().Update(section625G);
                    _unitOfWork.SaveChanges();
                    if (section625gdetail2slist.Count > 0)
                    {
                        foreach (var section625gdetail2 in section625gdetail2slist)
                        {

                            section625gdetail2.Section625GID = section625G.Section625GID;
                            section625gdetail2.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625gdetail2.Section625GDetail2ID > 0)
                            {
                                section625gdetail2.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625GDetail2>().Update(section625gdetail2);
                            }
                            else
                            {
                                section625gdetail2.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625GDetail2>().Insert(section625gdetail2);
                            }
                            //section625gdetail2.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625GDetail2>().Update(section625gdetail2);
                            _unitOfWork.SaveChanges();

                        }
                    }
                    if (SectionRecordingofIncidentlist.Count > 0)
                    {
                        foreach (var SectionRecordingofIncident in SectionRecordingofIncidentlist)
                        {
                            Section625GDetail1 section625gdetails = new Section625GDetail1();
                            section625gdetails.RISubCatCode = SectionRecordingofIncident;
                            section625gdetails.Section625GID = section625G.Section625GID;
                            section625gdetails.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                            if (section625gdetails.Section625GDetail1ID > 0)
                            {
                                section625gdetails.ObjectState = ObjectState.Modified;
                                _unitOfWork.Repository<Section625GDetail1>().Update(section625gdetails);
                            }
                            else
                            {
                                section625gdetails.ObjectState = ObjectState.Added;
                                _unitOfWork.Repository<Section625GDetail1>().Insert(section625gdetails);
                            }
                            //section625gdetails.ObjectState = ObjectState.Modified;
                            //_unitOfWork.Repository<Section625GDetail1>().Update(section625gdetails);
                            _unitOfWork.SaveChanges();
                        }
                    }

                }



                hoursereportdata = Hour24Report625.MapToDTO();
                return hoursereportdata;
            });

        }
        public Hour24Report625VO Gethoursreportdetailsbyid(string value, int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _hour24andsection625Repository.Gethoursreportdetailsbyid(value, id);
            });

        }



        public Hour24ReportReferenceDataVO GetHour24ReportReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Hour24ReportReferenceDataVO VO = new Hour24ReportReferenceDataVO();
                VO.Naturetypes = _subcategoryRepository.GetNaturetypes();

                VO.RecordingofIncident = _subcategoryRepository.GetRecordingofIncident();
                VO.GeneralAgencies = _subcategoryRepository.GetGeneralAgencies();
                VO.OccupationalHygieneAgencies = _subcategoryRepository.GetOccupationalHygieneAgencies();
                VO.TypeofContact = _subcategoryRepository.GetTypeofContact();
                VO.StandardActs = _subcategoryRepository.GetStandardActs();
                VO.SubstandardConditions = _subcategoryRepository.GetSubstandardConditions();
                VO.PersonalFactors = _subcategoryRepository.GetPersonalFactors();
                VO.JobFactors = _subcategoryRepository.GetJobFactors();
                VO.ControlStepsToPreventRecurrence = _subcategoryRepository.GetControlStepsToPreventRecurrence();
                VO.FireDepartment = _subcategoryRepository.GetFireDepartment();
                VO.IncidentClassification = _subcategoryRepository.GetIncidentClassification();
                VO.DiscriptionofExposedRisk = _subcategoryRepository.GetDiscriptionofExposedRisk();
                VO.Section_Record_Incident = _subcategoryRepository.GetSection_Record_Incident();
                VO.WeatherTypes = _subcategoryRepository.GetWeatherTypes();
                VO.Ports = _portRepository.GetPortzs();

                return VO;

            });
        }

        /// <summary>
        /// To Get Entity Details Based on EntitiyCode
        /// </summary>
        /// <param name="_entityCode"></param>
        /// <returns></returns>
        public Entity GetEntities(string entityCode)
        {
            return _hour24andsection625Repository.GetEntities(entityCode);
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int userid)
        {
            return _hour24andsection625Repository.Getuserdetails(_UserId);
        }
    }
}
