using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.EfData;

namespace Repository
{
    public class EFRepository : IRepository
    {
        private readonly ApplicationContext _context;

        public EFRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            _context.Remove(customer);
            _context.SaveChanges();
        }

        public void DepositFunds(int customerId, decimal funds)
        {
            var balance = _context.Balances.Where(x => x.CustomerId == customerId).First();
            balance.Amount += funds;
            _context.SaveChanges();
        }

        public decimal GetAvailableFunds(int customerId)
        {
            return _context.Balances.Where(x => x.CustomerId == customerId).Select(x => x.Amount).First();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers
                            .Include(x => x.Balance)
                            .Where(x => x.Id == id)
                            .FirstOrDefault();
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public void SaveCustomer(Customer customer)
        {
            var balance = new Balance { Customer = customer };
            _context.Customers.Add(customer);
            _context.Balances.Add(balance);
            _context.SaveChanges();
        }

        public void TransferFunds(Customer fromCustomer, Customer toCustomer, decimal funds)
        {
            fromCustomer.Balance.Amount -= funds;
            toCustomer.Balance.Amount += funds;
            _context.SaveChanges();
        }

        public void WithdrawFunds(int customerId, decimal funds)
        {
            var customer = GetCustomer(customerId);
            customer.Balance.Amount -= funds;
            _context.SaveChanges();
        }
    }
}
