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
    public class UserInfoUnitTestTest
    {
        private readonly astoriaTraining80Context _context;

        public UserInfoUnitTestTest()
        {
            var optionBuilder = new DbContextOptionsBuilder<astoriaTraining80Context>();
            optionBuilder.UseSqlServer("Data Source=ASTORIA-LT103; Database=astoria Training8.0_2022; User ID = sa;Password=pass123");
            _context = new astoriaTraining80Context(optionBuilder.Options);
        }

      

        [TestMethod()]

        public void PostUserInfo_ValidInput_MatchResultType_Test()
        {
            //Arrange

            var ObjUser = new UserInfo();
           
            ObjUser.FirstName = "Rogan";
            ObjUser.LastName = "Phul";
            ObjUser.UserId = 2;
            ObjUser.UserName = "Roh@Phul";
            ObjUser.Password = "Roh@123";
            ObjUser.Email = "Roh@gmail.com";
            ObjUser.CreationDate = DateTime.Now.Date;

            //Action

            var ObjUserinfoController = new UserInfoesController(_context);
            var result = ObjUserinfoController.PostUserInfo(ObjUser);


            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(OkResult));
        }

    }
}
