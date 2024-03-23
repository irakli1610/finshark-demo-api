using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){

            return new StockDto{
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName=stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()

            };
        }

        public static Stock ToStackFromCreateDto(this  CreateStockRequestDto stocRequestDto)
        {
            return new Stock
            {
                Symbol = stocRequestDto.Symbol,
                CompanyName=stocRequestDto.CompanyName,
                Purchase = stocRequestDto.Purchase,
                LastDiv = stocRequestDto.LastDiv,
                Industry = stocRequestDto.Industry,
                MarketCap = stocRequestDto.MarketCap,

            };
        }
    }
}