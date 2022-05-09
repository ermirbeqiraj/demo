namespace BusinessLogic
{
    using DataTransferObjects;
    using System;
    using System.Collections.Generic;
    using Test.EfData;

    public interface ICustomersManager
    {
        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCustomer(Int32 id);

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Customer GetCustomer(Int32 id);

        List<Customer> GetCustomers();
        CustomerFundsDto GetBalance(int id);
        void Deposit(int id, CustomerFundsDto model);
        void Withdraw(int id, CustomerFundsDto model);

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void SaveCustomer(Customer customer);
        void Transfer(TransferFundsDto model);
        #endregion
    }
}