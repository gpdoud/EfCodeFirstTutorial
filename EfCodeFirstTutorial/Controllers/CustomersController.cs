using EfCodeFirstTutorial.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstTutorial.Controllers {
    
    public class CustomersController {

        private readonly AppDbContext _context;

        public async Task<IEnumerable<Customer>> GetAll() {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByPK(int id) {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> Create(Customer customer) {
            if(customer == null) {
                throw new Exception("Input cannot be null");
            }
            if(customer.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            customer.Created = DateTime.Now;
            _context.Customers.Add(customer);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return customer;
        }

        public async Task Change(Customer customer) {
            if(customer == null) {
                throw new Exception("Input cannot be null");
            }
            if(customer.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(customer).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public async Task<Customer> Remove(int id) {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null) {
                throw new Exception("Not found");
            }
            _context.Customers.Remove(customer);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return customer;
        }

        public CustomersController() {
            _context = new AppDbContext();
        }
    }
}
