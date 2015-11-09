using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Data;


namespace FlooringProgram.Test
{
    [TestFixture]
    public class OrderRepoTests
    {
        DateTime date = DateTime.Parse("9 / 25 / 2015");
        OrderRepository repo = new OrderRepository();

        public List<Order> ListOfTestOrders()
        {
            var orders = new List<Order>
            {
                                
                             new Order()
                {
                      OrderNumber = 2,
            LastName = "Buchea",


            StateInfo = new State()
            {
                StateAbbreviation = "IN",
                TaxRate = 6M,
            },

            ProductInfo = new Product()
            {
                ProductType = "Carpet",
                CostPerSquareFoot = 2.25M,
                LaborCostPerSquareFoot = 2.10M
            },

            Area = 175M,

            MaterialCostTotal = 393.75M,
            LaborCostTotal = 367.50M,
            TaxTotal = 45.675M,
            Total = 806.925M
                },
                            new Order()
                {
                      OrderNumber = 3,
            LastName = "Rodgers",


            StateInfo = new State()
            {
                StateAbbreviation = "MI",
                TaxRate = 5.75M,
            },

            ProductInfo = new Product()
            {
                ProductType = "Wood",
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M
            },

            Area = 1200M,

            MaterialCostTotal = 6180,
            LaborCostTotal = 5700,
            TaxTotal = 683.1M,
            Total = 12563.10M
                },
                new Order()
                {
                      OrderNumber = 4,
            LastName = "Manziel",


            StateInfo = new State()
            {
                StateAbbreviation = "OH",
                TaxRate = 6.25M,
            },

            ProductInfo = new Product()
            {
                ProductType = "Tile",
                CostPerSquareFoot = 3.50M,
                LaborCostPerSquareFoot = 4.15M
            },

            Area = 850,

            MaterialCostTotal = 2975,
            LaborCostTotal = 3527.5M,
            TaxTotal = 406.40625M,
            Total = 6908.90625M
                }
            };

            return orders;
        }

        public Order FirstOrder()
        {
            var order = new Order()
            {
                OrderNumber = 1,
                LastName = "Kaufax",

                StateInfo = new State()
                {
                    StateAbbreviation = "PA",
                    TaxRate = 6.75M,
                },

                ProductInfo = new Product()
                {
                    ProductType = "Wood",
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M
                },

                Area = 600M,
                MaterialCostTotal = 3090M,
                LaborCostTotal = 2850M,
                TaxTotal = 400.95M,
                Total = 6340.95M
            };

            return order;
        }

        public Order TestOrder()
        {
            var order = new Order()
            {
                OrderNumber = 4,
                LastName = "James",

                StateInfo = new State()
                {
                    StateAbbreviation = "OH",
                    TaxRate = 6.25M,
                },

                ProductInfo = new Product()
                {
                    ProductType = "Wood",
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M
                },

                Area = 100000M,
                MaterialCostTotal = 515000,
                LaborCostTotal = 475000,
                TaxTotal = 61875,
                Total = 1051875
            };

            return order;
        }
    

        public void TestSetUp()
        {

            repo.CreateFile(date);

            repo.CreateOrder(FirstOrder(), date, true);

            var orders = ListOfTestOrders();

            foreach (var o in orders)
            {
                repo.CreateOrder(o, date);
            }

        }

        public void TestTearDown()
        {
            repo.SetFilePath(date);

            repo.DeleteFile();
        }

        [Test]
        public void TestSetupAndTearDown()
        {
            TestSetUp();

            var order = repo.LoadOrder(4, date);

            Assert.AreEqual("Manziel", order.LastName);

            TestTearDown();
        }

        [Test]
        public void CanLoadOrderGivenExistingFile()
        {
            TestSetUp();

            var order = repo.LoadOrder(2, date);

            Assert.AreEqual(2, order.OrderNumber);
            Assert.AreEqual("Buchea", order.LastName);

            TestTearDown();
        }

        [Test]
        public void UpdateOrderSucceeds()
        {
            TestSetUp();

            repo.UpdateOrder(TestOrder(), date);

            var orderUpdated = repo.LoadOrder(4, date);

            Assert.AreEqual("James", orderUpdated.LastName);
            Assert.AreEqual("OH", orderUpdated.StateInfo.StateAbbreviation);
            Assert.AreEqual(1051875M, orderUpdated.Total);

            TestTearDown();
        }

        [Test]
        public void CheckHighestOrderNumber()
        {
            TestSetUp();

            int result = repo.HighestOrderNumber(date);

            Assert.AreEqual(4, result);

            TestTearDown();
        }

        [Test]
        public void CheckAddOrder()
        {
            TestSetUp();

            var testOrder = TestOrder();

            testOrder.OrderNumber = repo.HighestOrderNumber(date) + 1;
            repo.CreateOrder(testOrder, date);

            var result = repo.LoadOrder(5, date);

            Assert.AreEqual(result.LastName, testOrder.LastName);
            Assert.AreEqual(result.ProductInfo.ProductType, testOrder.ProductInfo.ProductType);
            Assert.AreEqual(result.LaborCostTotal, testOrder.LaborCostTotal);
            Assert.AreEqual(result.OrderNumber, testOrder.OrderNumber);

            TestTearDown();
        }

        [Test]
        public void CheckDeleteOrder()
        {
            TestSetUp();

            repo.DeleteOrder(2, date);

            Order order = new Order();
            order = repo.LoadOrder(2, date);

            Assert.IsNull(order);
        }
    }
}
