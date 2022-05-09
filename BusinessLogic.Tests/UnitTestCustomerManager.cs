using Repository;
using Xunit;

namespace BusinessLogic.Tests
{
    public class UnitTestCustomerManager
    {
        [Fact]
        public void CustomerManager_Should_SaveCustomerWithBalance()
        {
            // act
            var repo = new InMemoryRepository();
            var manager = new CustomersManager(repo);

            // arrange
            const int customerId = 1;
            const int balance = 10;

            manager.SaveCustomer(new Test.EfData.Customer
            {
                Id = customerId,
                Name = "Someone",
                Surname = "SomeoneS",
                Balance = new Test.EfData.Balance
                {
                    CustomerId = customerId,
                    Amount = balance
                },
                IdCard = "TheId"
            });

            var theAddedCustoemr = manager.GetCustomer(customerId);
            // asert
            Assert.True(theAddedCustoemr.Balance.Amount == 10);
        }
    }
}
