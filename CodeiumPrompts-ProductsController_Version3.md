## Codeium Prompts for Optimizing ProductsController Methods

---

### 1. GetProducts (GET: api/products)

**Suboptimal Code:**
```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
{
    var products = _context.Products.Where(p => true).ToList();
    foreach (var p in products)
    {
        await Task.Delay(5);
        p.Name = p.Name.ToUpper();
    }
    return products;
}
```

**Prompt:**  
> Optimize this method for performance and readability. Use async database calls, remove unnecessary processing, and ensure best practices for EF Core data access.

---

### 2. PostProduct (POST: api/products)

**Suboptimal Code:**
```csharp
[HttpPost]
public async Task<ActionResult<Product>> PostProduct(Product product)
{
    _context.Products.Add(product);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
}
```

**Prompt:**  
> Refactor this method to use async database operations, add input validation, and follow best practices for maintainability and performance.

---

### 3. GetProduct (GET: api/products/{id})

**Suboptimal Code:**
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<Product>> GetProduct(int id)
{
    var products = _context.Products.ToList();
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
        return NotFound();

    return product;
}
```

**Prompt:**  
> Improve this method to fetch the product by ID efficiently using async and EF Core best practices. Avoid loading all products and use AsNoTracking for read-only access.

---

### 4. (Optional) Add a PUT Method for Update

**Prompt:**  
> Add a method to update an existing product by ID. Use async, validate input, and follow best practices for readability, maintainability, and efficient data access.

---

## How to Use

- Paste the suboptimal method and the prompt into your IDE with Codeium enabled.
- Let Codeium generate the optimized code.
- Review suggestions and apply them as demonstrated in your live session.

---