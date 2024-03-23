using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;


namespace api.Properties
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Stock> CreateAsync(Stock createStock)
        {
            await _context.Stock.AddAsync(createStock);
            await _context.SaveChangesAsync();
            return createStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id==id);

            if(stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName)){
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync(); 
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return  await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);

        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStock)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id==id);

            if(existingStock == null)
            {
                return null;
            }

            existingStock.Symbol=updateStock.Symbol;
            existingStock.CompanyName=updateStock.CompanyName;
            existingStock.Purchase=updateStock.Purchase;
            existingStock.LastDiv = updateStock.LastDiv;
            existingStock.MarketCap=updateStock.MarketCap;
            existingStock.Industry=updateStock.Industry;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}