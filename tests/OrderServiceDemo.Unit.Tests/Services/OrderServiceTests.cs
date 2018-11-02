using NSubstitute;
using OrderServiceDemo.Models.Exceptions;
using OrderServiceDemo.Services.Components;
using OrderServiceDemo.Services.Infrastructure;
using OrderServiceDemo.Core;
using System.Threading.Tasks;
using Xunit;

namespace OrderServiceDemo.Unit.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineItemRepository _orderLineItemRepository;

        public OrderServiceTests()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _orderLineItemRepository = Substitute.For<IOrderLineItemRepository>();
        }

        [Fact]
        public async Task OrderService_WhenCreatingOrder_IfNoLineItems_ThrowsInvalidRequestException()
        {
            //Arrange
            var order = new Models.Order();
            var service = BuildService();

            //Act && Assert
            var result = await Assert.ThrowsAsync<InvalidRequestException>(() => service.CreateOrder(order));
        }
        
        [Fact]
        public async Task OrderService_WhenCancelingOrder_IfNoOrderExists_ThrowsOrderDoesNotExistException()
        {
            //Arrange
            var service = BuildService();
            var order = new Models.Order();
            _orderRepository.GetOrder(Arg.Any<int>()).Returns((Models.Order)null);
            //Act && Assert
            var result = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => service.CancelOrder(Arg.Any<int>()));
        }

        [Fact]
        public async Task OrderService_WhenCancelingOrder_IfOrderIsCanceled_ThrowsOrderIsAlreadyCanceled()
        {
            //Arrange
            var service = BuildService();
            var order = new Models.Order();
            order.OrderStatus = OrderStatus.GetOrderStatus(1025);
            var orderId = 1;
            order.OrderId = orderId;
            _orderRepository.GetOrder(orderId).Returns(order);

            //Act && Assert
            var result = await Assert.ThrowsAsync<OrderIsAlreadyCanceledException>(() => service.CancelOrder(orderId));
        }

        [Fact]
        public async Task OrderService_WhenDeletingOrder_IfOrderDoesNotExist_ThrowsOrderDoesNotExistException ()
        {
            //Arrange
            var service = BuildService();
            var order = new Models.Order();
            _orderRepository.DeleteOrder(Arg.Any<Models.Order>()).Returns((Models.Order)null);
            
            //Act && Assert
            var result = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => service.DeleteOrder(Arg.Any<int>()));
        }

        // TODO write happy path tests

        private OrderService BuildService() => new OrderService(
            _orderRepository,
            _orderLineItemRepository);
    }
}
