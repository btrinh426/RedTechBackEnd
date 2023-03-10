using FakeItEasy;
using RedTechBackEnd.Interfaces;
using RedTechBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTechController.Test.OrderController
{
    public class OrderControllerTest
    {
        private readonly IOrderService _service;
        public OrderControllerTest()
        {
            _service = A.Fake<IOrderService>();
        }

        [Fact]
        public void OrderController_GetOrders_ReturnOK()
        {
            //Arrange
            var orders = A.Fake<ICollection<Order>>();

            //Act

            //Assert
        }
    }
}
