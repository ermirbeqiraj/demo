using System;
using System.Collections.Generic;

namespace Repository
{
    using System.Linq;
    using Test.EfData;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Repository.IRepository" />
    public class InMemoryRepository : IRepository
    {
        /// <summary>
        /// The customers
        /// </summary>
        private readonly List<Customer> Customers;

        /// <summary>
        /// The balances
        /// </summary>
        private readonly Dictionary<Int32, Decimal> Balances;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository()
        {
            this.Customers = new List<Customer>();
            this.Balances = new Dictionary<Int32, Decimal>();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            Customer customer = this.GetCustomer(id);
            this.Customers.Remove(customer);
        }

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DepositFunds(Int32 customerId,
                                 Decimal funds)
        {
            if (this.Balances.ContainsKey(customerId))
            {
                this.Balances[customerId] += funds;
            }
            else
            {
                this.Balances.Add(customerId, funds);
            }
        }

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            return this.Balances.ContainsKey(customerId) ? this.Balances[customerId] : 0;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomer(Int32 id)
        {
            return this.Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetCustomers()
        {
            return this.Customers.ToList();
        }

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
            if (this.DoesCustomerExist(customer.Id))
            {
                Customer existingCustomer = this.GetCustomer(customer.Id);
                existingCustomer.IdCard = customer.IdCard;
                existingCustomer.Name = customer.Name;
                existingCustomer.Surname = customer.Surname;
            }
            else
            {
                this.Customers.Add(customer);
                this.Balances.Add(customer.Id, customer.Balance.Amount);
            }
        }

        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WithdrawFunds(Int32 customerId,
                                  Decimal funds)
        {
            if (this.Balances.ContainsKey(customerId))
            {
                this.Balances[customerId] -= funds;
            }
        }

        public void TransferFunds(Customer fromCustomer, Customer toCustomer, decimal funds)
        {
            if (!this.Balances.ContainsKey(fromCustomer.Id) || !this.Balances.ContainsKey(toCustomer.Id))
            {
                return;
            }

            this.Balances[fromCustomer.Id] -= funds;
            this.Balances[toCustomer.Id] += funds;
        }

        /// <summary>
        /// Does the customer exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private Boolean DoesCustomerExist(Int32 id)
        {
            return this.Customers.Any(c => c.Id == id);
        }
    }
}
