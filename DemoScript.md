# Codeium Live Demonstration: Optimizing .NET Core Web API Performance

**Meeting duration:** 30 minutes

---

## Part 1: Introduction (2 min)
- Briefly introduce yourself and today’s topic.
- “Today, I’ll demonstrate how Codeium can help optimize .NET Core Web APIs for better performance. We’ll start with a deliberately suboptimal API, then use Codeium to improve it, and measure the results.”

---

## Part 2: Setting the Stage (3 min)
- Present the initial suboptimal API code.
    - Highlight key files: `ProductsController.cs`, `AppDbContext.cs`, `Startup.cs`, `Product.cs`.
- Open `ProductsController.cs` and point out:
    - Blocking calls (`.ToList()`, `.SaveChanges()`).
    - Inefficient data access (loading all products, unnecessary loops).
    - Lack of validation.
    - Unnecessary processing and async delays.
---

## Part 3: Running the Suboptimal API (5 min)
- Start the API (`dotnet run` or via IDE).
- Use Postman/curl to:
    - Add products (POST).
    - Get all products (GET).
    - Get product by ID (GET).
- Demonstrate:
    - Latency/sluggishness on GET (especially with increased data).
    - Discuss why performance is suffering (point to code).

---

## Part 4: Introducing Codeium (2 min)
- “Codeium is an AI-powered coding assistant that gives real-time suggestions for code improvements, including performance enhancements.”
- Show Codeium in your IDE.
- “Let’s see how Codeium can help us here.”

---

## Part 5: Live Refactoring with Codeium (8 min)
- Show Codeium suggestions in action:
    - Make queries asynchronous (`ToListAsync`, `SaveChangesAsync`).
    - Use `AsNoTracking()` for read-only queries.
    - Remove unnecessary async delays and loops.
    - Add input validation.
    - Optimize data access (query by ID directly).
- Accept suggestions and refactor code live.
- Summarize the changes (show before/after).

-------------------------------------------
//GetProducts()
// 1. Real-Time Performance Optimization & 2. Data Access Improvements
// - Using async/await for non-blocking I/O
// - Using AsNoTracking for read-only queries (avoids unnecessary EF Core overhead)
**Prompt:**  
> Optimize @GetProducts method for performance and readability. Use async database calls, remove unnecessary processing, and ensure best practices for EF Core data access.
-------------------------------------------

//PostProduct()
// 3. Detection and Correction of Anti-Patterns
// - Added input validation (guards against bad data)
// - Removed unnecessary synchronous calls
// 4. Improved Code Readability and Maintainability
// - Clear, concise method structure and variable naming

**Prompt:**  
> Refactor @PostProduct method to use async database operations, add input validation, and follow best practices for maintainability and performance.
-------------------------------------------

//GetProduct()
// 2. Data Access Improvements & 3. Detection and Correction of Anti-Patterns
// - Querying directly by ID using FirstOrDefaultAsync with AsNoTracking
// - Avoids loading all records, and avoids tracking when not needed

**Prompt:**  
> Improve @GetProduct method to fetch the product by ID efficiently using async and EF Core best practices. Avoid loading all products and use AsNoTracking for read-only access.
-------------------------------------------

//GetProductsWithCategory()
**Prompt:**
### 1. N+1 Query Problem
Optimize @GetProductsWithCategory method to avoid the N+1 query problem. Use EF Core best practices for eager loading and efficient DTO projection.
-------------------------------------------

//BulkUpdateProductPrices()
**Prompt:**
### 2. Bulk Update
Refactor @BulkUpdateProductPrices method to perform a bulk update efficiently. Avoid saving changes inside the loop and use optimal EF Core patterns for batch modification.
-------------------------------------------

//GetPagedProducts()
**Prompt:**
### 3. Pagination
Optimize @GetPagedProducts method for database-side pagination instead of in-memory. Use EF Core skip/take on the query itself.
-------------------------------------------

//SearchProducts()
**Prompt:**
### 4. Search Filtering
Refactor @SearchProducts method for efficient, case-insensitive search without breaking SQL indexes. Use EF Core best practices for read-only queries.
-------------------------------------------
---

## Part 6: Run the Optimized API (5 min)
- Start the optimized API.
- Repeat the same API calls (POST, GET, GET by ID).
    - Show improved response times and resource usage.
- Optionally, use a profiler (dotnet-counters or Visual Studio diagnostics) to highlight differences.

---

## Part 7: Q&A and Takeaways (5 min)
- Invite questions from the audience.
- Summarize key learnings:
    - The impact of small code changes.
    - How Codeium accelerates code review and optimization.
    - Encourage attendees to try Codeium with their own projects.

---

## Closing (30 seconds)
- Thank the audience for attending.
- Share resources/links for Codeium and .NET Core best practices.

---

**End of Demonstration**