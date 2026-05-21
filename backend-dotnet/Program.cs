using backend_dotnet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controller support
builder.Services.AddControllers();

// Add Swagger (API testing page)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect PostgreSQL database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Allow Angular frontend to call backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Enable Swagger page
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
















/*// Import Entity Framework Core namespace
// This is required to use DbContext and PostgreSQL provider (UseNpgsql)
using Microsoft.EntityFrameworkCore;

// Import your custom Data folder namespace
// This allows us to access AppDbContext class
using backend_dotnet.Data;


// Create a WebApplication builder object
// This initializes the ASP.NET Core application
// It loads configuration (appsettings.json), logging, and prepares services container
var builder = WebApplication.CreateBuilder(args);


// Register Controllers service into Dependency Injection container
// This allows the app to recognize and use Controller classes (like TodoController)
builder.Services.AddControllers();


// Register DbContext service for database access
// AddDbContext tells ASP.NET Core to create and manage AppDbContext instances
builder.Services.AddDbContext<AppDbContext>(options =>
    // UseNpgsql tells EF Core to connect to PostgreSQL database
    // It reads the connection string named "DefaultConnection"
    // from appsettings.json file
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// Register CORS service (Cross-Origin Resource Sharing)
// This allows frontend (Angular running on different port) to call this backend
builder.Services.AddCors(options =>
{
    // Create a policy named "AllowAll"
    options.AddPolicy("AllowAll",
        policy => policy
            // Allow requests from any frontend origin (like localhost:4200)
            .AllowAnyOrigin()
            // Allow all HTTP methods (GET, POST, PUT, DELETE)
            .AllowAnyMethod()
            // Allow all headers (Content-Type, Authorization, etc.)
            .AllowAnyHeader()
    );
});


// Build the application using all the configured services
// This creates the final app object that will handle requests
var app = builder.Build();


// Middleware: Redirect HTTP requests to HTTPS automatically
// Adds security by forcing encrypted communication
app.UseHttpsRedirection();


// Middleware: Enable the CORS policy defined above ("AllowAll")
// This must be added before MapControllers
app.UseCors("AllowAll");


// Middleware: Enables authorization checks
// If you add authentication later, this will enforce user permissions
app.UseAuthorization();


// Maps controller routes to endpoints
// This tells ASP.NET Core to use attribute routing defined in controllers
// Example: [Route("api/[controller]")]
app.MapControllers();


// Starts the web server and begins listening for HTTP requests
// This keeps the application running on defined port (like 5292)
app.Run();

*/

// ================= BACKEND SIMPLE REFERENCE NOTES =================

// ====================== ENTITY FRAMEWORK CORE ======================

// Entity Framework Core (EF Core) is a translator between C# and the database.
// It allows us to write C# code instead of SQL queries.
// It converts C# commands into SQL and sends them to PostgreSQL.
// Example: _context.Todos.ToList() → SELECT * FROM todos;
// EF Core is an ORM (Object Relational Mapper).
// ORM means: C# Class ↔ Database Table mapping.


// ====================== REGISTERING SERVICES ======================

// Registering means telling ASP.NET Core:
// "Please keep this service ready. I will need it later."
// When we register a service, ASP.NET stores it in the Dependency Injection container.
// Later, ASP.NET automatically provides it wherever required.


// ====================== CONTROLLER ======================

// Controller handles incoming HTTP requests.
// It is like a traffic police that controls request flow.
// Example:
// GET /api/todo → calls GetTodos() method
// POST /api/todo → calls CreateTodo() method
// Controller receives request, processes it, and returns response (usually JSON).


// ====================== DEPENDENCY INJECTION (DI) ======================

// Dependency Injection means ASP.NET automatically provides required objects.
// Instead of creating objects manually using "new", ASP.NET creates and gives them.
// Example:
// private readonly AppDbContext _context;
// ASP.NET injects AppDbContext automatically.
// Real life example: You order food → waiter brings it → you don't cook yourself.
// DI makes code cleaner, reusable, and easier to test.


// ====================== DB CONTEXT ======================

// DbContext represents a session with the database.
// It manages database connection.
// It tracks changes (Add, Update, Delete).
// DbSet<Todo> represents a table in the database.
// _context.SaveChanges() sends changes to the database.


// ====================== CORS ======================

// CORS = Cross-Origin Resource Sharing.
// It allows frontend (localhost:4200) to talk to backend (localhost:5292).
// Browsers block requests between different ports by default.
// CORS gives permission to allow those requests.
// Without CORS, Angular cannot call backend API.


// ====================== MIDDLEWARE ======================

// Middleware is like a security checkpoint.
// Every request passes through middleware before reaching controller.
// Example middleware:
// - UseHttpsRedirection()
// - UseCors()
// - UseAuthorization()
// Middleware can modify request or response.


// ====================== ROUTING ======================

// Routing decides which controller method handles which URL.
// Example:
// [Route("api/[controller]")]
// If controller name is TodoController → route becomes api/todo
// [HttpGet] → handles GET request
// [HttpPost] → handles POST request


// ====================== FULL REQUEST FLOW ======================

// 1. Angular sends HTTP request.
// 2. Request enters middleware pipeline.
// 3. CORS allows request.
// 4. Routing sends request to Controller.
// 5. Controller uses DbContext.
// 6. EF Core converts C# to SQL.
// 7. PostgreSQL executes SQL.
// 8. Data returns as JSON.
// 9. Angular receives response and updates U
// ====================== SIMPLE MEMORY TRICKS ======================

// Register = Keep ready
// Controller = Traffic police
// DI = Waiter serving you
// EF Core = Translator
// DbContext = Database manager
// CORS = Security permission
// Middleware = Security checkpoint