using EfCodeFirstTutorial.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstTutorial.Controllers {
    
    public class OrderlinesController {

        private readonly AppDbContext _context;

        public async Task<IEnumerable<Orderline>> GetAll() {
            return await _context.Orderlines.ToListAsync();
        }

        public async Task<Orderline> GetByPK(int id) {
            return await _context.Orderlines.FindAsync(id);
        }

        public async Task<Orderline> Create(Orderline orderline) {
            if(orderline == null) {
                throw new Exception("Input cannot be null");
            }
            if(orderline.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            _context.Orderlines.Add(orderline);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return orderline;
        }

        public async Task Change(Orderline orderline) {
            if(orderline == null) {
                throw new Exception("Input cannot be null");
            }
            if(orderline.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(orderline).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }
        public async Task<Orderline> Remove(int id) {
            var orderline = await _context.Orderlines.FindAsync(id);
            if(orderline == null) {
                throw new Exception("Not found");
            }
            _context.Orderlines.Remove(orderline);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return orderline;
        }


        public OrderlinesController() {
            _context = new AppDbContext();

        }
    }
}
