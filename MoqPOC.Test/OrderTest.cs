using Moq;
using MoqPOC.Entities;
using MoqPOC.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MoqPOC.Test
{
    [TestFixture]
    public class Tests : RepositoryBaseTest
    {
        IRepository<Order> _orderRepository;
        [SetUp]
        public void Setup()
        {
            var _orderList = new List<Order>
            {
                new Order {Id=1,FirstName="User1",LastName="User1LastName",TotalPrice=100,UserName="abc1",Quantity=1 },
                new Order {Id=2,FirstName="User2",LastName="User2LastName",TotalPrice=200,UserName="abc2",Quantity=2  },
                new Order {Id=3,FirstName="User3",LastName="User3LastName" ,TotalPrice=300,UserName="abc3",Quantity=3 }
            };

            Mock<IRepository<Order>> _mockRepo = new Mock<IRepository<Order>>();

            SetupRepositoryMock<Order>(_mockRepo, _orderList);
            _orderRepository = _mockRepo.Object;
        }

        [Test]
        public void Update_And_Calculate_Quantity()
        {
            var order = new Order { Id = 1, Quantity = 5 };
            _orderRepository.Update(order);
            var item = _orderRepository.GetById(1);
            Assert.AreEqual(order.Quantity, item.Quantity);
        }
        [Test]
        public void Remove_And_Check_Total_Item_Count()
        {
            var oldCount= _orderRepository.GetAll().Count();
            _orderRepository.Delete(new Order { Id = 1 });
            var newCount = _orderRepository.GetAll().Count();
            Assert.AreNotEqual(oldCount, newCount);
        }


    }
}