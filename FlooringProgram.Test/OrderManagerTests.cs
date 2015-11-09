using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Data;

namespace FlooringProgram.Test
{
    [TestFixture]
    public class OrderManagerTests
    {
        OrderRepoTests testRepo = new OrderRepoTests();

        OrderManager manager = new OrderManager();

        DateTime date = DateTime.Parse("9 / 25 / 2015");

        [Test]
        public void FileExists()
        {
            testRepo.TestSetUp();

            bool result = manager.FileExists(date);

            Assert.AreEqual(result, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void FileDoesNotExist()
        {
            testRepo.TestSetUp();

            DateTime wrongDate = new DateTime();

            bool result = manager.FileExists(wrongDate);

            Assert.AreEqual(result, false);

            testRepo.TestTearDown();
        }

        [Test]
        public void OrderExists()
        {
            testRepo.TestSetUp();

            bool result = manager.OrderExists(2, date);

            Assert.AreEqual(result, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void OrderDoesNotExist()
        {
            testRepo.TestSetUp();

            bool result = manager.OrderExists(100, date);

            Assert.AreEqual(result, false);

            testRepo.TestTearDown();
        }

        [Test]
        public void CheckUpdateOrde()
        {
            testRepo.TestSetUp();

            var response = manager.EditOrder(testRepo.TestOrder(), date);

            Assert.AreEqual(response.Success, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void CheckRemoveOrder()
        {
            testRepo.TestSetUp();

            var response = manager.RemoveOrder(3, date);

            Assert.AreEqual(response.Success, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void CheckAddOrder()
        {
            testRepo.TestSetUp();

            var order = testRepo.TestOrder();
            order.OrderNumber = 5;

            var response = manager.AddOrder(order, date);

            Assert.AreEqual(response.Success, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void CheckDisplayOrders()
        {
            testRepo.TestSetUp();

            var order = testRepo.TestOrder();
            order.OrderNumber = 5;

            var response = manager.DisplayOrders(date);

            Assert.AreEqual(response.Success, true);

            testRepo.TestTearDown();
        }

        [Test]
        public void CheckLoadOrders()
        {
            testRepo.TestSetUp();

            var order = manager.LoadOrder(3, date);

            Assert.AreEqual(order.LastName, "Rodgers");
            Assert.AreEqual(order.StateInfo.StateAbbreviation, "MI");
            Assert.AreEqual(order.ProductInfo.ProductType, "Wood");

            testRepo.TestTearDown();
        }
    }
}
