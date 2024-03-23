using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock createStock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStock);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}