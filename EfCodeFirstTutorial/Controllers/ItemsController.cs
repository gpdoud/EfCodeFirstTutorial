using EfCodeFirstTutorial.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstTutorial.Controllers {
    
    public class ItemsController {

        private readonly AppDbContext _context;

        public async Task<IEnumerable<Item>> GetAll() {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByPK(int id) {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item> Create(Item item) {
            if(item == null) {
                throw new Exception("Input cannot be null");
            }
            if(item.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            _context.Items.Add(item);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return item;
        }

        public async Task Change(Item item) {
            if(item == null) {
                throw new Exception("Input cannot be null");
            }
            if(item.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(item).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public ItemsController() {
            _context = new AppDbContext();

        }
    }
}
