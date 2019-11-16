using DataSample.BusinessLayer.Services;
using DataSample.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DataSample.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        /// <summary>
        /// Gets a paginated list of products.
        /// </summary>
        /// <param name="searchTerm">An optional string used to filter products by name</param>
        /// <param name="pageIndex">The index of the page to retrieve</param>
        /// <param name="itemsPerPage">How many items to retrieve</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(string searchTerm = null, int pageIndex = 0, int itemsPerPage = 50)
        {
            var products = await productsService.GetAsync(searchTerm, pageIndex, itemsPerPage);
            return Ok(products);
        }

        /// <summary>
        /// Saves an existing product in the database. Currently, only the name is updated.
        /// </summary>
        /// <param name="product">The product to update</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Save(Product product)
        {
            await productsService.SaveAsync(product);
            return NoContent();
        }
    }
}
