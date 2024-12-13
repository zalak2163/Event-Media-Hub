using Event_Media_Hub_Group_Project.Data;  // Importing the application data context
using Event_Media_Hub_Group_Project.Interface;  // Importing interfaces for services
using Event_Media_Hub_Group_Project.Services;  // Importing the service implementations
using Microsoft.AspNetCore.Identity;  // Importing Identity services
using Microsoft.EntityFrameworkCore;  // Importing EF Core for database operations

var builder = WebApplication.CreateBuilder(args);  // Initialize the WebApplication builder

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");  // Retrieving the connection string from app settings
builder.Services.AddDbContext<ApplicationDbContext>(options =>  // Registering the ApplicationDbContext with the SQL Server database connection
    options.UseSqlServer(connectionString));  // Using SQL Server for the DbContext
builder.Services.AddDatabaseDeveloperPageExceptionFilter();  // Enabling database error page for development environment

// Add Identity services with default user and role management
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)  // Enabling default Identity with confirmation requirement
    .AddRoles<IdentityRole>()  // Adding role support (e.g., Admin, User)
    .AddEntityFrameworkStores<ApplicationDbContext>();  // Using Entity Framework stores for Identity (stores user, roles, etc. in the database)

// Add Swagger services to generate API documentation
builder.Services.AddSwaggerGen();  // Adding Swagger service to the container for API documentation generation

// Registering service interfaces with their implementations for dependency injection
builder.Services.AddScoped<IEventService, EventService>();  // Registering EventService implementation for IEventService
builder.Services.AddScoped<ILocationService, LocationService>();  // Registering LocationService implementation for ILocationService
builder.Services.AddScoped<IUserService, UserService>();  // Registering UserService implementation for IUserService
builder.Services.AddScoped<IPhotoService, PhotoService>();  // Registering PhotoService implementation for IPhotoService
builder.Services.AddScoped<ICategoryService, CategoryService>();  // Registering CategoryService implementation for ICategoryService

// Adding MVC (Model-View-Controller) services with support for views and controllers
builder.Services.AddControllersWithViews();  // Adding controllers with views for MVC pattern

var app = builder.Build();  // Building the application

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())  // Checking if the environment is development
{
    app.UseMigrationsEndPoint();  // Enables migrations end point for handling database migrations
    app.UseSwagger();  // Enabling Swagger middleware
    app.UseSwaggerUI();  // Enabling Swagger UI for API documentation browsing in development
}
else  // In production or other environments
{
    app.UseExceptionHandler("/Home/Error");  // Redirecting to error page for exceptions
    app.UseHsts();  // Enabling HTTP Strict Transport Security (HSTS) for increased security
}

app.UseHttpsRedirection();  // Redirecting HTTP requests to HTTPS for secure communication
app.UseStaticFiles();  // Enabling static files (e.g., CSS, JavaScript, images) to be served from the wwwroot folder

app.UseRouting();  // Enabling routing for HTTP requests to map to controllers

app.UseAuthentication();  // Enabling authentication middleware (ensures requests are authenticated)
app.UseAuthorization();  // Enabling authorization middleware (ensures requests are authorized)

app.MapControllerRoute(  // Configuring default controller route (URL pattern mapping)
    name: "default",  // Naming the route as "default"
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Default pattern: Home controller, Index action, and optional id parameter
app.MapRazorPages();  // Enabling Razor Pages routing (for .cshtml pages)

app.Run();  // Running the application (this starts the web server and listens for requests)
