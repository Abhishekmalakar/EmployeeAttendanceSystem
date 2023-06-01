using _astoriaTrainingAPI.Controllers;
using _astoriaTrainingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace astoriaTraining8._0UnitTest.Controllers
{
    [TestClass()]
    public class StudentsDataControllerTests
    {
       private readonly CivilServicesContext _context;

        public StudentsDataControllerTests()
        {
            var optionBuilder = new DbContextOptionsBuilder<CivilServicesContext>();
            optionBuilder.UseSqlServer("Data Source=ASTORIA-LT103; Database=CivilServices; User ID = sa;Password=pass123");
            _context = new CivilServicesContext(optionBuilder.Options);
        }

        [TestMethod()]
        public void GetStudentList_MatchCountContent_Test()
        {
            //Arrange
            int expectedAllEmployeeCount = 1;
            //Action
            var ObjStudentMastersController = new StudentMastersController(_context);
            var apiResult = ObjStudentMastersController.GetStudentDataList();
            // var resultList = apiResult.Result.Result;
            var resultList = ((OkObjectResult)apiResult.Result.Result).Value as List<StudentData>;
            //    int resutlCount = resultList.;
            //throw new NotImplementedException();
            //Assert
            Assert.AreEqual(expectedAllEmployeeCount, resultList.Count);
        }
    }
}
