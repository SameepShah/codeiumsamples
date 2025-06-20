# Demo Script: How Codeium Improves .NET Core Web API Performance

---

## 1. Introduction (2 min)
**Script:**  
"Hello everyone! Today, I’ll demonstrate how Codeium, the AI-powered code assistant, helps .NET Core Web API developers write more performant code—quickly and easily. We’ll look at real code, see common anti-patterns, and watch Codeium suggest improvements in real-time."

---

## 2. Project Setup (1 min)
**Script:**  
"I have a simple .NET Core Web API with an in-memory database. The UsersController has some endpoints that are intentionally suboptimal, so we can see how Codeium assists in making them better."

---

## 3. Real-Time Performance Suggestions (5 min)
### Example: Fetching All Users, Filtering in Memory

**Show this code:**
```csharp
[HttpGet("active")]
public IActionResult GetActiveUsers()
{
    var allUsers = _context.Users.ToList(); // fetch all users
    var activeUsers = new List<User>();
    foreach (var user in allUsers)
    {
        if (user.IsActive)
        {
            activeUsers.Add(user);
        }
    }
    return Ok(activeUsers);
}
```

**Script:**  
"This endpoint fetches all users, then filters active ones in memory. This approach is inefficient, especially with a large dataset."

**Action:**  
Begin editing this method.  
- Start typing `var activeUsers = await _context.Users.Where(u => u.IsActive)...`  
- Let Codeium auto-complete the rest, suggesting `ToListAsync()` and using the async pattern.

**Script:**  
"See how Codeium suggests filtering in the database, and using async/await for better scalability. This reduces memory usage and database load."

---

## 4. Asynchronous Programming Patterns (3 min)
### Example: Synchronous Data Access

**Show this code:**
```csharp
[HttpGet("by-email/{email}")]
public IActionResult GetUserByEmail(string email)
{
    var user = _context.Users.FirstOrDefault(u => u.Email == email);
    if (user == null) return NotFound();
    return Ok(user);
}
```

**Script:**  
"This method is synchronous. Codeium recognizes the pattern and suggests converting to an async method."

**Action:**  
Start typing `public async Task<IActionResult> GetUserByEmail(string email)` and let Codeium suggest `await _context.Users.FirstOrDefaultAsync(...)`.

---

## 5. Query & Data Access Optimization (3 min)
### Example: Bulk Update Anti-Pattern

**Show this code:**
```csharp
[HttpPost("activate-all")]
public IActionResult ActivateAll()
{
    var users = _context.Users.ToList();
    foreach (var user in users)
    {
        user.IsActive = true;
        _context.Users.Update(user);
    }
    _context.SaveChanges();
    return Ok();
}
```

**Script:**  
"This endpoint loads all users and updates them one by one. Codeium can suggest more efficient, set-based approaches, or at least flag the inefficiency."

---

## 6. Best Practices and Dependency Injection (3 min)
**Script:**  
"Codeium also helps by reminding you to use dependency injection for your DbContext—improving resource management and testability."

---

## 7. Resource Management and Memory Efficiency (2 min)
**Script:**  
"If you accidentally instantiate DbContext manually or forget to dispose resources, Codeium will suggest proper DI patterns and using statements."

---

## 8. Code Review and Refactoring (3 min)
**Script:**  
"Let’s look at another method with some redundant logic. As I edit, Codeium proposes removing dead code, simplifying expressions, and enforcing best practices."

---

## 9. Q&A or Live Coding (5 min)
**Script:**  
"Any code samples you’d like to see improved? Let’s try them live and see Codeium’s suggestions!"

---

## 10. Recap (1 min)
**Script:**  
"Today, you saw Codeium in action—making .NET Core Web API code more performant and maintainable, in real-time. From database queries to async patterns and best practices, Codeium is a powerful productivity and quality booster for developers."

---

## Optional: Before/After Slide

| Before (Unoptimized) | After (Optimized by Codeium) |
|----------------------|------------------------------|
| Synchronous code     | Async/await patterns         |
| In-memory filtering  | DB-level filtering           |
| Manual resource mgmt | Dependency Injection         |
| Inefficient updates  | Set-based operations         |

---

**Thank your audience, and invite them to try Codeium themselves!**