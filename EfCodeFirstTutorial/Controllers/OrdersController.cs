using EfCodeFirstTutorial.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstTutorial.Controllers {
    
    public class OrdersController {

        private readonly AppDbContext _context;

        public async Task<IEnumerable<Order>> GetAll() {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByPK(int id) {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> Create(Order order) {
            if(order == null) {
                throw new Exception("Input cannot be null");
            }
            if(order.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            _context.Orders.Add(order);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return order;
        }

        public async Task Change(Order order) {
            if(order == null) {
                throw new Exception("Input cannot be null");
            }
            if(order.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(order).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public OrdersController() {
            _context = new AppDbContext();

        }
    }
}
