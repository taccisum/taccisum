using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.CommonModel;
using Model.Models;
using Practice.Controllers;

namespace UnitTest.Practice.Controller
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class Menu
    {
        private MenuController _controller;

        public Menu()
        {
            _controller = new MenuController();
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetMenusList()
        {
            try
            {
                var query = new MenuQuery()
                {
                    draw = 0,
                    length = 10,
                    start = 10,
                    Name = ""
                };
                var obj = _controller.GetMenusList(query).Data as DataTablesResult;
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.data);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
