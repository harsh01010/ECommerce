using IMS.Web.Services.IServices;
using IMS.Web.Services;
using IMS.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IProductService,ProductService>();
builder.Services.AddHttpClient<ICartService,CartService>();


StaticDetails.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
StaticDetails.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
StaticDetails.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:CartAPI"];
StaticDetails.OrderAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];


builder.Services.AddScoped<IBaseservice,BaseService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IProductService, ProductService>();  
builder.Services.AddScoped<ICartService,CartService>(); 
builder.Services.AddScoped<IOrderService,OrderService>(); 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/AuthAPI/Login";
        options.AccessDeniedPath = "/AuthAPI/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
