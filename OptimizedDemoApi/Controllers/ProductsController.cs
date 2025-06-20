using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizedDemoApi.Data;
using OptimizedDemoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptimizedDemoApi.Controllers
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
        // 1. Real-Time Performance Optimization & 2. Data Access Improvements
        // - Using async/await for non-blocking I/O
        // - Using AsNoTracking for read-only queries (avoids unnecessary EF Core overhead)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Optimized: Asynchronous, no unnecessary processing
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }


        // POST: api/products
        // 3. Detection and Correction of Anti-Patterns
        // - Added input validation (guards against bad data)
        // - Removed unnecessary synchronous calls
        // 4. Improved Code Readability and Maintainability
        // - Clear, concise method structure and variable naming
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Optimized: Input validation
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0)
            {
                return BadRequest("Invalid product data.");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        // GET: api/products/{id}
        // 2. Data Access Improvements & 3. Detection and Correction of Anti-Patterns
        // - Querying directly by ID using FirstOrDefaultAsync with AsNoTracking
        // - Avoids loading all records, and avoids tracking when not needed
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Optimized: Efficient query
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/products/with-category
        [HttpGet("with-category")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithCategory()
        {
            var result = await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
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
            }
            await _context.SaveChangesAsync(); // Save changes once
            return Ok();
        }

        // GET: api/products/paged
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPagedProducts(int page = 1, int pageSize = 20)
        {
            var products = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return products;
        }

        // GET: api/products/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name, $"%{keyword}%"))
                .ToListAsync();
            return products;
        }
    }
}