using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class AccountServiceTest
    {
        protected IAccountService accountservice;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;
        private IAccountRepository _accountRepository;

        Module module;
        Entity entity;
        User user;
        Role role;
        UserRole userrole;
        EntityPrivilege entityprivilege;
        RolePrivilege roleprivilege;
        SuperCategory supercategory;
        SubCategory subcategory;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            accountservice = new AccountService(unitOfWork);
            _accountRepository = new AccountRepository(unitOfWork);

            //  1	admin	EMP	1	Administrator	adminlastname	98859885	ramakrishna.peddoju@navayugainfotech.com	NULL	A	N	1	8/6/2014 1:55:09 PM	1	9/9/2014 4:33:51 PM	l9gHZiV4Jn7YKQu7+BmzOQ==	N	10/21/2014 4:33:43 PM	0	9/10/2014 5:26:00 PM
            user = new User
            {
                UserID = 1,
                UserName = "admin",
                UserType = "Add",
                UserTypeID = 1,
                FirstName = "A",
                LastName = "B",
                ContactNo = "2345566788",
                EmailID = "abc@a.com",
                RecordStatus = "A",
                PWD = "l9gHZiV4Jn7YKQu7+BmzOQ==",
                PwdExpirtyDate = DateTime.Now.AddDays(42),
                IsFirstTimeLogin = "N",
                IncorrectLogins = 0,
                LoginTime = DateTime.Now

            };

            role = new Role
            {
                RoleID = 1,
                RoleName = "ADMIN",
                RecordStatus = "A"
            };

            userrole = new UserRole
            {
                UserID = 1,
                RoleID = 1,
                RecordStatus = "A",
            };

            supercategory = new SuperCategory
            {
                SupCatCode = "PRIV",
                SupCatName = "Priveleges",
                RecordStatus = "A"
            };

            subcategory = new SubCategory
            {
                SubCatCode = "Add",
                SupCatCode = "PRIV",
                SubCatName = "Add",
                RecordStatus = "A"
            };

            module = new Module
            {
                ModuleID = 1,
                ModuleName = "Administration",
                ParentModuleID = null,
                RecordStatus = "A"
            };

            entity = new Entity
            {
                EntityID = 1,
                ModuleID = 2,
                EntityName = "User Privileges",
                EntityCode = "UP",
                RecordStatus = "A"
            };

            entityprivilege = new EntityPrivilege
            {
                EntityID = 1,
                SubCatCode = "Add",
                RecordStatus = "A"

            };

            roleprivilege = new RolePrivilege
            {
                EntityID = 1,
                RoleID = 1,
                SubCatCode = "Add",
                RecordStatus = "A"

            };


            unitOfWork.Repository<SuperCategory>().Insert(supercategory);
            unitOfWork.Repository<SubCategory>().Insert(subcategory);
            unitOfWork.Repository<User>().Insert(user);
            unitOfWork.Repository<Role>().Insert(role);
            unitOfWork.Repository<UserRole>().Insert(userrole);
            unitOfWork.Repository<Module>().Insert(module);

            unitOfWork.SaveChanges();

            module = new Module
            {
                ModuleID = 2,
                ModuleName = "Administration",
                ParentModuleID = 1,
                RecordStatus = "A"
            };

            unitOfWork.Repository<Module>().Insert(module);
            unitOfWork.Repository<Entity>().Insert(entity);
            unitOfWork.Repository<EntityPrivilege>().Insert(entityprivilege);
            unitOfWork.Repository<RolePrivilege>().Insert(roleprivilege);
            unitOfWork.SaveChanges();



        }

        [Test]
        public void GetUserModulesTest()
        {
            var actualResult = accountservice.GetModulesByUser();
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult.FirstOrDefault<Module>().ModuleID);
            Assert.AreEqual("Administration", actualResult.FirstOrDefault<Module>().ModuleName);
        }

        [Test]
        public void RandomPasswordTest()
        {
            string randompwd = Password.Generate(8, 10);
            string randompwd1 = Password.Generate();
        }

        [Test]
        public void UserLoginTest()
        {
            string username = "admin";
            string password = "navayuga";

            var actualResult = _accountRepository.UserLogin(username, Encrypt(password, true), string.Empty);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult.UserID);
            Assert.AreEqual("admin", actualResult.UserName);
        }

        [Test]
        public void ChangePasswordTest()
        {
            string username = "admin";
            string oldpassword = Encrypt("navayuga", true);
            string newpassword = Encrypt("navayuga", true); ;

            var actualResult = _accountRepository.ChangePassword(oldpassword, newpassword, username, 1, "DB","5");
            Assert.IsNotNull(actualResult);

        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader =
            //                                    new AppSettingsReader();
            //// Get the key from config file

            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                                 typeof(String));

            string key = "Navayuga";

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

    }
}

