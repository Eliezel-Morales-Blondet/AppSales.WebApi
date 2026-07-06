using AppSales.WebApi.Data;
using AppSales.WebApi.Data.Entities;
using AppSales.WebApi.Data.Enums;
using AppSales.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppSales.WebApi.Business
{
    public class SaleBusiness(AppDbContext _db)
    {
        public async Task<ApiResponse<int>> CreateAsync(CreateSaleRequest req)
        {
            if (string.IsNullOrEmpty(req.customerName))
                return ApiResponse<int>.Fail("Customer name is required");
            
            if (!Enum.IsDefined(typeof(PaymentType),req.paymentTypeValue))
                return ApiResponse<int>.Fail("Payment type not allowed");
            
            var productNameEmpty = req.details.Any(p => string.IsNullOrEmpty(p.productName));

            if (productNameEmpty)
                return ApiResponse<int>.Fail("Product name is required");

            var dbEntity = new Sale()
            {
                CustomerName = req.customerName,
                PaymentType = (PaymentType)req.paymentTypeValue,
                Total = req.total,
                Details = req.details.Select(e => new SaleDetail
                {
                    ProductName = e.productName, 
                    Quantity = e.quantity,
                    UnitPrice = e.unitPrice
                }).ToList()
            };

            await _db.Sale.AddAsync(dbEntity);
            await _db.SaveChangesAsync();
            return ApiResponse<int>.Success(dbEntity.SaleId);
        }
        public async Task<ApiResponse<GetSalesQueryResponse>> GetAllAsync(GetSalesQueryRequests req)
        {
            var query = _db.Sale.AsNoTracking(); // select * from sale

            var totalItems = await query.CountAsync(); // select count(*) from sale

            var sales = await query.OrderByDescending(c => c.SaleDate).Skip((req.page - 1) * req.pageSize).Take(req.pageSize).ToListAsync();

            var formatSales = sales.Select(e => new GetSaleResponse
            (
                saleId : e.SaleId,
                customerName : e.CustomerName,
                paymentTypeName : Enum.GetName(typeof(PaymentType), e.PaymentType)!,
                total : e.Total,
                saleDate : e.SaleDate.ToString("dd/MM/YYYY", CultureInfo.InvariantCulture)
            ));
        }

    }
}
