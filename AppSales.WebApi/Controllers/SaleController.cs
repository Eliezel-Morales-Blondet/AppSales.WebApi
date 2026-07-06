using AppSales.WebApi.Business;
using AppSales.WebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSales.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController(SaleBusiness bs) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleRequest req)
        {
            var result =  await bs.CreateAsync(req);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetSalesQueryRequests req)
        {
            var result = await bs.GetAllAsync(req);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await bs.GetByIdAsync(id);
            return Ok(result);
        }

    }
}
