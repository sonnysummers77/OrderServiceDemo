using AutoMapper;
using NSubstitute;
using OrderServiceDemo.Controllers;
using OrderServiceDemo.Exceptions;
using OrderServiceDemo.Models.Exceptions;
using OrderServiceDemo.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using OrderServiceDemo.Unit.Tests.Utility;

namespace OrderServiceDemo.Unit.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly IOrderService _orderSerivce;
        private readonly IMapper _mapper;

        public OrderControllerTests()
        {
            var autoMapperConfig = Mapping.AutoMapperConfig.Configure();
            _mapper = autoMapperConfig.CreateMapper();

            _orderSerivce = Substitute.For<IOrderService>();
        }

        [Fact]
        public async Task OrderController_WhenCreatingOrder_IfInvalidRequest_ShouldReturn_HttpBadReqest()
        {
            //Arrange
            _orderSerivce
                .CreateOrder(Arg.Any<Models.Order>())
                .Returns(Task.FromException<Models.Order>(new InvalidRequestException("The Message")));

            var controller = BuildController();

            //Act
            var result = await Assert.ThrowsAsync<StatusCodeException>(() => controller.CreateOrder(new Core.v1.RequestModels.CreateOrderRequest
            {
                OrderLineItems = new List<Core.v1.OrderLineItem>()
            }));

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task OrderController_WhenCreatingOrder_IfValidRequest_ShouldReturn_Success()
        {
            //Arrange
            _orderSerivce
                .CreateOrder(Arg.Any<Models.Order>())
                .Returns((OrderServiceTestData.GetFakeOrder()));

            var controller = BuildController();

            //Act
            var result = await controller.CreateOrder(new Core.v1.RequestModels.CreateOrderRequest
            {
                OrderLineItems = new List<Core.v1.OrderLineItem>()
            });

            //Assert
            Assert.Equal(OrderServiceTestData.GetFakeOrder().OrderId, result.OrderId);
        }

        [Fact]
        public async Task OrderController_WhenCancelingOrder_IfOrderDoesNotExist_HttpAccepted()
        {
            //Arrange
            _orderSerivce
                .CancelOrder(Arg.Any<int>())
                .Returns(Task.FromException<Models.Order>(new OrderDoesNotExistException("The Message")));

            var controller = BuildController();

            //Act
            var result = await Assert.ThrowsAsync<StatusCodeException>(() => controller.CancelOrder(1));

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task OrderController_WhenCancelingOrder_IfOrderIsAlreadyCanceled_HttpAccepted()
        {
            //Arrange
            _orderSerivce
                .CancelOrder(Arg.Any<int>())
                .Returns(Task.FromException<Models.Order>(new OrderIsAlreadyCanceledException("The Message")));

            var controller = BuildController();

            //Act
            var result = await Assert.ThrowsAsync<StatusCodeException>(() => controller.CancelOrder(1));

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task OrderController_WhenCancelingOrder_IfValidRequest_ShouldReturn_Success()
        {
            //Arrange
            _orderSerivce
                .CancelOrder(Arg.Any<int>())
                .Returns((OrderServiceTestData.GetFakeOrder()));

            var controller = BuildController();

            //Act
            var result = await controller.CancelOrder(1);

            //Assert
            Assert.Equal(OrderServiceTestData.GetFakeOrder().OrderId, result.OrderId);
        }


        [Fact]
        public async Task OrderController_WhenDeletingOrder_IfOrderDoesNotExist_HttpAccepted()
        {
            //Arrange
            _orderSerivce
                .DeleteOrder(Arg.Any<int>())
                .Returns(Task.FromException<Models.Order>(new OrderDoesNotExistException("The Message")));

            var controller = BuildController();

            //Act
            var result = await Assert.ThrowsAsync<StatusCodeException>(() => controller.DeleteOrder(1));

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task OrderController_WhenDeletingOrder_IfValidRequest_ShouldReturn_Success()
        {
            //Arrange
            _orderSerivce
                .DeleteOrder(Arg.Any<int>())
                .Returns((OrderServiceTestData.GetFakeOrder()));

            var controller = BuildController();

            //Act
            var result = await controller.DeleteOrder(1);

            //Assert
            Assert.Equal(OrderServiceTestData.GetFakeOrder().OrderId, result.OrderId);
        }


        private OrderController BuildController() => new OrderController(
            _mapper, 
            _orderSerivce);
    }
}
