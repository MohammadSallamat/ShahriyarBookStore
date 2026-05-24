using Dapper;
using Domain.SellerAggregate;
using Domain.SellerAggregate.Repository;
using Infrastructure._Utilities;
using Infrastructure.Persistent.Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SellerAgg
{
    internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        private readonly DapperContext _dapperContext;
        public SellerRepository(ShahriarBookStoreContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
        }

        //public async Task<InventoryResult?> GetInventoryById(long id)
        //{
        //    return await _context.SellerInventories.Where(b => b.Id == id)
        //        .Select(i=>new InventoryResult
        //        {
        //            Id = i.Id,
        //            SellerId = i.SellerId,
        //            ProductId = i.ProductId,
        //            Price = i.Price,
        //            Count = i.Count

        //        }).FirstOrDefaultAsync();
        //}

        public async Task<InventoryResult?> GetInventoryById(long id)
        {
            using var Connection = _dapperContext.CreateConnection();
            var sql = $"Select * from {_dapperContext.Inventories} where Id=@id";
            return await Connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { id = id });
        }

    }
}
