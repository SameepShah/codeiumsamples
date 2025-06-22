using CodeiumSample.Data;
using CodeiumSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CodeiumSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Suboptimal: ToList() is blocking, unnecessary Where, and unnecessary async delay and processing
            var products = _context.Products.Where(p => true).ToList();
            foreach (var p in products)
            {
                await Task.Delay(5); // simulate inefficiency
                p.Name = p.Name.ToUpper(); // unnecessary processing
            }
            return products;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Suboptimal: No input validation, blocking add/save
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Suboptimal: Loads all products, not just the required one
            var products = _context.Products.ToList();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return product;
        }

        // GET: api/products/with-category
        [HttpGet("with-category")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithCategory()
        {
            var products = await _context.Products.ToListAsync();
            var result = new List<ProductDto>();
            foreach (var product in products)
            {
                // N+1: Each iteration triggers a separate query
                var category = await _context.Categories.FindAsync(product.CategoryId);
                result.Add(new ProductDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    CategoryName = category?.Name
                });
            }
            return result;
        }

        // PUT: api/products/bulk-update-price
        [HttpPut("bulk-update-price")]
        public async Task<IActionResult> BulkUpdateProductPrices([FromBody] decimal percentageIncrease)
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.Price += product.Price * (percentageIncrease / 100);
                await _context.SaveChangesAsync(); // Inefficient: save inside loop
            }
            return Ok();
        }

        // GET: api/products/paged
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPagedProducts(int page = 1, int pageSize = 20)
        {
            //Inefficient Pagination: Loading All Data Into Memory
            var products = await _context.Products.ToListAsync();
            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        // GET: api/products/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            // Inefficient Filtering - String Search with Poor Index Use
            // Poor: ToLower disables index use, no AsNoTracking
            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
            return products;
        }

    }
}