using Microsoft.VisualStudio.TestTools.UnitTesting;
using _astoriaTrainingAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using _astoriaTrainingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace _astoriaTrainingAPI.Controllers.Tests
{
    [TestClass()]
    public class EmployeeMastersControllerTests
    {
        private readonly astoriaTraining80Context _context;

        public EmployeeMastersControllerTests()
        {
            var optionBuilder = new DbContextOptionsBuilder<astoriaTraining80Context>();
            // optionBuilder.UseSqlServer("Data Source=abhishek184.database.windows.net;Database=astoriaTraining8.020221121; User ID = AbhishekM;Password=Abhishek@123");
            optionBuilder.UseSqlServer("Data Source=ASTORIA-LT103; Database=astoria Training8.0_2022; User ID = sa;Password=pass123");

            _context = new astoriaTraining80Context(optionBuilder.Options);
        }

        #region Unit Test Get EmployeeList ApI

        [TestMethod()]
        public void GetEmployeeList_MatchCountContent_Test()
        {
            //Arrange
            int expectedAllEmployeeCount = 8;
            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var apiResult = ObjEmployeeMasterController.GetEmployeeList();
           // var resultList = apiResult.Result.Result;
            var resultList = ((OkObjectResult)apiResult.Result.Result).Value as List<Employee>;
            //    int resutlCount = resultList.;
            //throw new NotImplementedException();
            //Assert
            Assert.AreEqual(expectedAllEmployeeCount, resultList.Count);
        }

        [TestMethod()]
        public void GetEmployeeList_noContentReturn_Test()
        {
            //Arrange
            var nocontentResult = typeof(NoContentResult);

            //Action
            var ObjEmployeeMaterController = new EmployeeMastersController(_context);
            var apiResult = ObjEmployeeMaterController.GetEmployeeList();
            var resultType = apiResult.Result.Result;

            //Assert
            Assert.IsNotInstanceOfType(resultType, nocontentResult);

        }

        [TestMethod()]

        public void Getvalid_Input__return_okresult_Test()
        {
            //Arrange
            long expectedAllEmployeeCount = 8;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.GetEmployeeList();
            //  var resultList = ((OkObjectResult)result.Result.Result).Value as List<Employee>;
            var resultList = ((OkObjectResult)result.Result.Result).Value as List<Employee>;
            //Assert
            Assert.AreNotEqual(resultList, typeof(OkObjectResult));
        }
        #endregion

        #region Unit Test Get EmployeeByID 


        [TestMethod()]
        public void Getvalid_Input_EmployeeByID_return_match_outputempkey_Test()
        {
            //Arrange
            long empKey_Input = 10028;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.GetEmployeeMaster(empKey_Input);
            // var resultList = ((OkObjectResult)result.Result.Result).Value as List<EmployeeMaster>;
            var resultList = ((OkObjectResult)result.Result.Result).Value as EmployeeMaster;

            //Assert
            // Assert.AreEqual(empKey_Input, resultList[0].EmployeeKey);
            Assert.AreEqual(empKey_Input,resultList.EmployeeKey);
        }

        //[TestMethod]
        //public void GetvaidEmployee_returncount_Test()
        //{
        //    //Action
        //    long empKey = 10028;
           

        //    //Action
        //    var ObjEmployeeMasterController = new EmployeeMastersController(_context);
        //    var result = ObjEmployeeMasterController.GetEmployeeMaster(empKey);
           
        //    var resultObject = ((OkObjectResult)result.Result.Result).Value as List<EmployeeMaster>;

        //    //Assert
        //    Assert.AreEqual(1, resultObject.Count);
        //}


        [TestMethod()]
        public void GetInvalid_Input_ByEmployeeID_return_NotFound_Test()
        {
            //Arrange
           int empKey_Input = 100;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.GetEmployeeMaster(empKey_Input);
          //  var resultList = ((OkObjectResult)result.Result.Result).Value as EmployeeMaster;

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }
       
        [TestMethod()]
        public void Getvalid_Input_TypeEmployeeByID_return_okresult_Test()
        {
            //Arrange
            long empKey_Input = 10028;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.GetEmployeeMaster(empKey_Input);
            var resultList = result.Result.Value as EmployeeMaster;

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(OkObjectResult));
        }
        #endregion

        #region Unit Test GetAllCompanies API
        [TestMethod]
        public void GetAllCompanies_Count_Test()
        {
            //Arrange
            int expextedCompanyCount = 3;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var apiResult = ObjEmployeeMasterController.GetCompanyMaster();
            var resultList = apiResult.Result.Value as List<CompanyMaster>;
            int result = resultList.Count;
            //Assert
            Assert.AreEqual(expextedCompanyCount, result);
        }
            [TestMethod]
            public void GetCompanies_noContentReturn_Test()
            {
                //Arrange
                var nocontentCompaniesResult = typeof(NoContentResult);

                //Action
                var ObjEmployeeMaterController = new EmployeeMastersController(_context);
                var apiResult = ObjEmployeeMaterController.GetCompanyMaster();
                var resultType = apiResult.Result.Result;

                //Assert
                Assert.IsNotInstanceOfType(resultType, nocontentCompaniesResult);
         
        }

        #endregion

        #region Unit Test GetAllDesignationCount API

        [TestMethod]
        public void GetAllDesignationCount_Test()
        {
            //Arrange

            int ExpeectedDesignationCount = 5;

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var apiResult = ObjEmployeeMasterController.GetDesignationMaster();
            var resultList = apiResult.Result.Value as List<DesignationMaster>;
            int result = resultList.Count;
            //Assert
            Assert.AreEqual(ExpeectedDesignationCount, result);
        }

        [TestMethod]
        public void GetDesignation_noContentReturn_Test()
        {
            //Arrange
            var nocontentDesignationResult = typeof(NoContentResult);

            //Action
            var ObjEmployeeMaterController = new EmployeeMastersController(_context);
            var apiResult = ObjEmployeeMaterController.GetDesignationMaster();
            var resultType = apiResult.Result.Result;

            //Assert
            Assert.IsNotInstanceOfType(resultType, nocontentDesignationResult);

        }
        #endregion

        #region Unit Test for Post Employee API

        [TestMethod()]

        public void PostEmployeeMaster_ValidInput_MatchResultType_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
        //   ObjEmployee.EmployeeId = "ATIL-124";
            ObjEmployee.EmpFirstName = "Rajesh";
            ObjEmployee.EmpLastName = "Balu";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 101;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 60;
            ObjEmployee.EmpJoiningDate = DateTime.Now.Date;

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PostEmployeeMaster(ObjEmployee);
           

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(OkResult));
        }

        [TestMethod]
        public void PostEmployee_Save_WithMissingInput_MatchType_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeId = "";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 105;
            ObjEmployee.EmpJoiningDate = DateTime.Now.Date;

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PostEmployeeMaster(ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void PostEmployee_InvalidLengthInpute_MatchType_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeId = "ATIL-11111111111111111111111111111111111111111";
          

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PostEmployeeMaster(ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));

            }

        [TestMethod]
        public void PostEmployee_DuplicateEmployeeIDInpute_MatchType_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
            
            ObjEmployee.EmployeeId = "ATIL-104";
            ObjEmployee.EmpFirstName = "Sachin";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 105;
            ObjEmployee.EmpJoiningDate = DateTime.Now.Date;

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PostEmployeeMaster(ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(ConflictObjectResult));

        }
        [TestMethod]
        public void PostEmployee_InvalidResignationDate_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeId = "ATIL-123";
            ObjEmployee.EmpFirstName = "Anja";
            ObjEmployee.EmpLastName = "B";
            ObjEmployee.EmpGender = "F";
            ObjEmployee.EmpCompanyId = 101;
            ObjEmployee.EmpDesignationId = 1004;
            ObjEmployee.EmpHourlySalaryRate = 50;
            ObjEmployee.EmpJoiningDate = DateTime.Parse("12-21-2022");
            ObjEmployee.EmpResignationDate = DateTime.Parse("11-09-2022");
            ObjEmployee.CreationDate = DateTime.Parse("11-10-2022");
            ObjEmployee.ModifiedDate = DateTime.Parse("11-10-2022");


            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PostEmployeeMaster(ObjEmployee);


            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));
        }

        #endregion

        #region Unit Test For Put EmployeeMaster

        [TestMethod]

        public void PutEmployee_EmployeeIdNotExistInput_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
           ObjEmployee.EmployeeKey = 100;
          //  ObjEmployee.EmployeeId = "ATIL-104";
           // ObjEmployee.EmpFirstName = "Sachin";
           // ObjEmployee.EmpLastName = "Rangote";
           // ObjEmployee.EmpGender = "M";
           // ObjEmployee.EmpCompanyId = 102;
           // ObjEmployee.EmpDesignationId = 1003;
           // ObjEmployee.EmpHourlySalaryRate = 105;
           // ObjEmployee.EmpJoiningDate = DateTime.Parse("12-21-2022");
           //ObjEmployee.CreationDate = DateTime.Parse("11-09-2022");

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey,ObjEmployee);


            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutEmployee_DuplicateEmployeeIDInput_MatchType_Test()
        {
            //Arrange

            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "ATIL-104";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 105;
            ObjEmployee.EmpJoiningDate = DateTime.Now.Date;

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(ConflictObjectResult));

        }

        [TestMethod]

        public void PutEmployee_InvalidEmptyEmployeeID_Input_MatchType_Test()
        {
           //Arrange

            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangote";

          //Action
             var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PutEmployee_InvalidEmptyFirstNameInput_MatchType_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "ATIL-1115";
            ObjEmployee.EmpFirstName = "";
            ObjEmployee.EmpLastName = "Rangote";

            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));
        }


        [TestMethod]
        public void PutEmployee_Invalid_EmptyLastNameInput_MatchType_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "ATIL-1115";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "";

         //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PutEmployee_Invalid_EmployeeID_LengthInput_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "ATIL-111555555555555555555555555555555555555555";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 105;
            ObjEmployee.EmpJoiningDate = DateTime.Now.Date;

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));

        }

        [TestMethod]
        public void PutEmployee_InvalidFirstName_LengthInput_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            //ObjEmployee.EmployeeId = "ATIL-1115";
            ObjEmployee.EmpFirstName = "Yashwantdeqefdeqfeflmrmgmfldlqwed21rddddddddddddddddd@#@#3#ewqqdefrwfdeqdeqfeqfeqdww ewdwdqeqfdcdfe2ewdlwldwqsw";
            //ObjEmployee.EmpLastName = "Rangote";

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));

        }

        [TestMethod]
        public void PutEmployee_InvalidLastName_LengthInput_Test()
        {
            //Arrange
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            //ObjEmployee.EmployeeId = "ATIL-1115";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangoteeeeeeeeeeettttttttttttguhggcfcsgyiusuhughwgsuwuusiu9w920q18qushwxgvwgwgsvsjJHBXJHSXJSVXJGSVXGSSVXGSVXGJSVX";

            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));

        }
        [TestMethod]
        public void PutEmployee_ValidInput_OkstatusTest()
        {
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10028;
            ObjEmployee.EmployeeId = "ATIL-104";
            ObjEmployee.EmpFirstName = "Sachine ";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1005;
            ObjEmployee.EmpHourlySalaryRate = 105;
            ObjEmployee.EmpJoiningDate = DateTime.Parse("2022-09-29");
            ObjEmployee.CreationDate = DateTime.Parse("2022-09-30");
            ObjEmployee.ModifiedDate = DateTime.Parse("2022-09-30");
            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey,ObjEmployee);


            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(OkResult));

        }


        [TestMethod]
        public void PutEmployee_Invalid_Joining_Designation_Input_()
        {
            var ObjEmployee = new EmployeeMaster();
            ObjEmployee.EmployeeKey = 10051;
            ObjEmployee.EmployeeId = "ATIL-1115";
            ObjEmployee.EmpFirstName = "Yashwant";
            ObjEmployee.EmpLastName = "Rangote";
            ObjEmployee.EmpGender = "M";
            ObjEmployee.EmpCompanyId = 102;
            ObjEmployee.EmpDesignationId = 1003;
            ObjEmployee.EmpHourlySalaryRate = 101;
            ObjEmployee.EmpJoiningDate = DateTime.Parse("2022-09-16");
            ObjEmployee.EmpResignationDate = DateTime.Parse("2022-09-10");
            ObjEmployee.CreationDate = DateTime.Parse("2022-11-09");
            ObjEmployee.ModifiedDate = DateTime.Parse("2022-11-09");
            //Action

            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.PutEmployeeMaster(ObjEmployee.EmployeeKey, ObjEmployee);


            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult));

        }

        #endregion

        #region Unit Test For DeleteEmployee

        [TestMethod]
        public void DeleteEmployee_InvalidDelete_Input_Test()
        {

            //Arrange
            long empKey_Input = 100;
            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.DeleteEmployeeMaster(empKey_Input);
            var resultList = result.Result.Result;

            //Assert
            Assert.IsInstanceOfType(resultList, typeof(NotFoundResult));
        }
            
        [TestMethod]
        public void  DeleteEmployeeIdInUse_Input_Test()
        {
            //Arrange
            long EmpKey = 10028;
         
            //Action
            var ObjEmployeeMasterController = new EmployeeMastersController(_context);
            var result = ObjEmployeeMasterController.DeleteEmployeeMaster(EmpKey);
            var resultList = result.Result.Result;

            //Assert
            Assert.IsInstanceOfType(resultList, typeof(ConflictObjectResult));

        }

         [TestMethod]
          public void DeleteEmployee_validInput_Test()
        {
            //Arrange
            long DeleteEmpKey = 10059;

            //Action
            var objEmployeeMasterController = new EmployeeMastersController(_context);
            var result = objEmployeeMasterController.DeleteEmployeeMaster(DeleteEmpKey);

          //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(OkResult));
        }

        #endregion
    }
}
