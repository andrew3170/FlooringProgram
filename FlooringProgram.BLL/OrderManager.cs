/* * * * * * * * * * * * * * * * * * * * *
OrderManager.cs

9/25/15

Overview: This manager class serves as the sort of middleman between the UI and the data layers. Hopefully gracefully handles any exceptions that may occur. 
        
Constructor: 
        
Variables:        

Defined Methods:     

    public Response<List<Order>> DisplayOrders(DateTime date)        
    public Response<Order> AddOrder(Order order, DateTime date)
    public Response<EditReceipt> EditOrder(Order updatedOrder, DateTime date)
    public Response<Order> RemoveOrder(int orderNumber, DateTime date)
    public bool FileExists(DateTime date)
    public bool OrderExists(int orderNumber, DateTime date)
    public Order LoadOrder(int orderNumber, DateTime date)

* * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class OrderManager
    {
        public Response<List<Order>> DisplayOrders(DateTime date)
        {

            var repo =  new OrderRepository();

            var response = new Response<List<Order>>();

            try
            {
                var orders = repo.LoadOrders(date);

                if (orders == null)
                {
                    response.Success = false;
                    response.Message = "A file with that date was not found.";
                }

                else
                {
                    response.Success = true;
                    response.Data = orders;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error with Loading the File: " + ex.Message;
                repo.ErrorLogger(ex.Message);
            }

            return response;
        }

        public Response<Order> AddOrder(Order order, DateTime date)
        {
            var repo = new OrderRepository();
            var response = new Response<Order>();

            if (repo.FileExists(date))
            {
                order.OrderNumber = repo.HighestOrderNumber(date) + 1;

                try
                {
                    repo.CreateOrder(order, date);

                    response.Success = true;

                    response.Data = order;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "There was an error adding the order: " + ex.Message;
                    repo.ErrorLogger(ex.Message);
                }

                return response;
            }

            else
            {
                order.OrderNumber = 1;

                try
                {
                    repo.CreateFile(date);
                    repo.CreateOrder(order, date, true);
                    response.Success = true;
                    response.Data = order;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "There was an error adding the order: " + ex.Message;
                    repo.ErrorLogger(ex.Message);
                }

                return response;
            }

            
        }

        public Response<EditReceipt> EditOrder(Order updatedOrder, DateTime date)
        {
            OrderRepository repo = new OrderRepository();
            var response = new Response<EditReceipt>();

            try
            {
                repo.UpdateOrder(updatedOrder, date);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error with updating the order: " + ex.Message;
                repo.ErrorLogger(ex.Message);
            }

            return response;
        }

        public Response<Order> RemoveOrder(int orderNumber, DateTime date)
        {
            OrderRepository repo = new OrderRepository();
            var response = new Response<Order>();

            try
            {
                repo.DeleteOrder(orderNumber, date);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error with deleting the order: " + ex.Message;
                repo.ErrorLogger(ex.Message);
            }

            //check to see if it deleted the last order in the file, if so, delete the file also
            var orders = repo.LoadOrders(date);
            if (orders.Count == 0)
            {
               repo.DeleteFile();
            }

            return response;
        }

        public bool FileExists(DateTime date)
        {
            OrderRepository repo = new OrderRepository();

            return repo.FileExists(date);
        }

        public bool OrderExists(int orderNumber, DateTime date)
        {
            OrderRepository repo = new OrderRepository();

            var order = repo.LoadOrder(orderNumber, date);

            if (order == null)
            {
                return false;
            }
            return true;
        }

        public Order LoadOrder(int orderNumber, DateTime date)
        {
            OrderRepository repo = new OrderRepository();

            return repo.LoadOrder(orderNumber, date);
        }


    }
}
