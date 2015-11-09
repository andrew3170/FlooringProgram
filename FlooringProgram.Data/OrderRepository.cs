/* * * * * * * * * * * * * * * * * * * * *
OrderRepository.cs

9/25/15

Overview: 
        
Constructor: 
        
Variables:        

Defined Methods:     

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class OrderRepository
    {
        private string FilePath;
        string FilePathErrorLogger = @"DataFiles\Errors.txt";

        private List<State> states;

        private List<Product> products; 

        public void CreateOrder(Order newOrder, DateTime date, bool writeHeader = false)
        {
            var orders = LoadOrders(date);
            orders.Add(newOrder);

            if (writeHeader)
            {
                StreamWriter writer = File.AppendText(FilePath);
                WriteHeader(writer);
            }

            WriteOrder(newOrder);
        }

        public void WriteHeader(StreamWriter writer)
        {
            using (writer)
            {
                writer.Write("OrderNumber,");
                writer.Write("CustomerName,");
                writer.Write("State,");
                writer.Write("TaxRate,");
                writer.Write("ProductType,");
                writer.Write("Area,");
                writer.Write("CostPerSquareFoot,");
                writer.Write("LaborCostPerSquareFoot,");
                writer.Write("MaterialCostTotal,");
                writer.Write("LaborCostTotal,");
                writer.Write("TaxTotal,");
                writer.Write("Total");
            }
        }

        public void WriteOrder(Order newOrder)
        {
            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.Write(Environment.NewLine);
                writer.Write(newOrder.OrderNumber + ",");
                writer.Write(newOrder.LastName + ",");
                writer.Write(newOrder.StateInfo.StateAbbreviation + ",");
                writer.Write(newOrder.StateInfo.TaxRate + ",");
                writer.Write(newOrder.ProductInfo.ProductType + ",");
                writer.Write(newOrder.Area + ",");
                writer.Write(newOrder.ProductInfo.CostPerSquareFoot + ",");
                writer.Write(newOrder.ProductInfo.LaborCostPerSquareFoot + ",");
                writer.Write(newOrder.MaterialCostTotal + ",");
                writer.Write(newOrder.LaborCostTotal + ",");
                writer.Write(newOrder.TaxTotal + ",");
                writer.Write(newOrder.Total + ",");
            }
        }

        public void DeleteOrder(int orderNumber, DateTime date)
        {
            var orders = LoadOrders(date);

            var order1 = orders.First(o => o.OrderNumber == orderNumber);

            orders.Remove(order1);

            OverwriteFile(orders);
        }

        public int HighestOrderNumber(DateTime date)
        {
            List<Order> orders = LoadOrders(date);
            return orders.Select(a => a.OrderNumber).Max();
        }

        public Order LoadOrder(int orderNumber, DateTime date)
        {
            SetFilePath(date);
            List<Order> orders = LoadOrders(date);
            return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);


        }

        public void UpdateOrder(Order updatedOrder, DateTime date)
        {
            var orders = LoadOrders(date);

            var order1 = orders.First(o => o.OrderNumber == updatedOrder.OrderNumber);

            order1.LastName = updatedOrder.LastName;
            order1.StateInfo.StateAbbreviation = updatedOrder.StateInfo.StateAbbreviation;
            order1.StateInfo.StateName = updatedOrder.StateInfo.StateName;
            order1.Area = updatedOrder.Area;
            order1.LaborCostTotal = updatedOrder.LaborCostTotal;
            order1.ProductInfo.LaborCostPerSquareFoot = updatedOrder.ProductInfo.LaborCostPerSquareFoot;
            order1.MaterialCostTotal = updatedOrder.MaterialCostTotal;
            order1.ProductInfo.CostPerSquareFoot = updatedOrder.ProductInfo.CostPerSquareFoot;
            order1.ProductInfo.ProductType = updatedOrder.ProductInfo.ProductType;
            order1.StateInfo.TaxRate = updatedOrder.StateInfo.TaxRate;
            order1.TaxTotal = updatedOrder.TaxTotal;
            order1.Total = updatedOrder.Total;

            OverwriteFile(orders);

        }

        public void DeleteFile()
        {
            File.Delete(FilePath);
        }

        public void OverwriteFile(List<Order> orders )
        {
            File.Delete(FilePath);

            using (StreamWriter writer = File.CreateText(FilePath))
            {
                WriteHeader(writer);

                foreach (var order in orders)
                {
                    WriteOrder(order);
                }
            }
        }

        public void SetFilePath(DateTime date)
        {

            string sMonth = date.Month.ToString().PadLeft(2, '0');
            string sDay = date.Day.ToString().PadLeft(2, '0');

            string fileName = "Orders_" + sMonth + sDay + date.Year + ".txt";

            FilePath = @"TestOrders\" + fileName;
        }

        public void CreateFile(DateTime date)
        {        
            SetFilePath(date);

            File.Create(FilePath).Dispose();

        }

        public List<Order> LoadOrders(DateTime date)
        {
            SetFilePath(date);

            List<Order> orders = new List<Order>();

            var reader = File.ReadAllLines(FilePath);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');

                var order = new Order();
                order.StateInfo = new State();
                order.ProductInfo = new Product();
                

                order.OrderNumber = Int32.Parse(columns[0]);
                order.LastName = columns[1];
                order.StateInfo.StateAbbreviation = columns[2];
                order.StateInfo.TaxRate = Decimal.Parse(columns[3]);
                order.ProductInfo.ProductType = columns[4];
                order.Area = Decimal.Parse(columns[5]);
                order.ProductInfo.CostPerSquareFoot = Decimal.Parse(columns[6]);
                order.ProductInfo.LaborCostPerSquareFoot = Decimal.Parse(columns[7]);
                order.MaterialCostTotal = Decimal.Parse(columns[8]);
                order.LaborCostTotal = Decimal.Parse(columns[9]);
                order.TaxTotal = Decimal.Parse(columns[10]);
                order.Total = Decimal.Parse(columns[11]);

                orders.Add(order);

            }

            return orders;
        }

        public bool FileExists(DateTime date)
        {
            SetFilePath(date);
            return File.Exists(FilePath);
        }

        public void ErrorLogger(string exception)
        {
            //if the file exists, just go ahead and write to it
            if (File.Exists(FilePathErrorLogger))
            {
                using (StreamWriter writer = File.AppendText(FilePathErrorLogger))
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(DateTime.Now + " - ");
                    writer.Write(exception);
                }
            }

            //if the file doesn't exist, go ahead and create the file and log the first error
            else
            {
                File.Create(FilePathErrorLogger).Dispose();

                using (StreamWriter writer = File.AppendText(FilePathErrorLogger))
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(DateTime.Now + " - ");
                    writer.Write(exception);
                }
            }

        }


    }
}
