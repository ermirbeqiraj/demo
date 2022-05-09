namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic.Exceptions;
    using DataTransferObjects;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Test.EfData;

    public class CustomersManager : ICustomersManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CustomersManager(IRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            this.Repository.DeleteCustomer(id);
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Customer GetCustomer(Int32 id)
        {
            return this.Repository.GetCustomer(id);
        }

        public List<Customer> GetCustomers()
        {
            return this.Repository.GetCustomers();
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
            this.Repository.SaveCustomer(customer);
        }

        public CustomerFundsDto GetBalance(int id)
        {
            var customer = Repository.GetCustomer(id);
            if (customer == null)
                throw new CustomerNotFoundException();

            var customerFunds = Repository.GetAvailableFunds(id);
            return new CustomerFundsDto
            {
                Funds = customerFunds
            };
        }

        public void Deposit(int id, CustomerFundsDto model)
        {
            var customer = Repository.GetCustomer(id);
            if (customer == null)
                throw new CustomerNotFoundException();

            if (model.Funds < 0)
                throw new DepositMinimumException();

            Repository.DepositFunds(id, model.Funds);
        }


        public void Withdraw(int id, CustomerFundsDto model)
        {
            var customer = Repository.GetCustomer(id);
            if (customer == null)
                throw new CustomerNotFoundException();

            if (model.Funds < 0)
                throw new DepositMinimumException();

            var customerFunds = Repository.GetAvailableFunds(id);
            if (customerFunds < model.Funds)
                throw new FundsOutOfRange(customerFunds);

            Repository.WithdrawFunds(id, model.Funds);
        }

        public void Transfer(TransferFundsDto model)
        {
            var customerFrom = Repository.GetCustomer(model.From);
            var customerTo = Repository.GetCustomer(model.To);
            if (customerFrom == null || customerTo == null)
                throw new CustomerNotFoundException();

            if (model.Funds < 0)
                throw new DepositMinimumException();

            var customerFunds = Repository.GetAvailableFunds(customerFrom.Id);
            if (customerFunds < model.Funds)
                throw new FundsOutOfRange(customerFunds);

            try
            {
                Repository.TransferFunds(customerFrom, customerTo, model.Funds);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidBalanceStateException();
            }
        }
        #endregion
    }
}