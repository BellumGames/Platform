using Microsoft.Extensions.Options;
using Platform;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName = "Albany";
});

WebApplication app = builder.Build();

app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) => {
    Platform.MessageOptions opts = msgOpts.Value;
    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
});

app.MapGet("/", () => "Hello World!");

/*((IApplicationBuilder)app).Map("/branch", branch => {
    //branch.Run(new Platform.QueryStringMiddleWare().Invoke);

    //branch.UseMiddleware<Platform.QueryStringMiddleWare>();

    branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });

    branch.Run(async (context) => {
        await context.Response.WriteAsync($"Branch Middleware");
    });
});

app.UseMiddleware<Platform.QueryStringMiddleWare>();
app.MapGet("/", () => "Hello World!");*/

/*app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
});*/

/*// Primul middleware 
app.Use(async (context, next) => {
    await context.Response.WriteAsync("Getting Response from 1st Middleware \n");
    await next();
});

// Al doilea middleware 
app.Run(async context => { 
    await context.Response.WriteAsync("Response Response from second Middleware \n");
});*/

/*//Primul middleware
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Use Middleware1 Incoming Request \n");
    await next();
    await context.Response.WriteAsync("Use Middleware1 Outgoing Response \n");
});

// Al doilea middleware 
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Use Middleware2 Incoming Request \n");
    await next();
    await context.Response.WriteAsync("Use Middleware2 Outgoing Response \n");
});

// Al treilea middleware 
app.Run(async context => {
    await context.Response.WriteAsync("Run Middleware3 Request Handled and Response Generated\n");
});*/

/*app.Use(async (context, next) => {
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.UseMiddleware<Platform.QueryStringMiddleWare>();*/

/*app.Use(async (context, next) => {
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) => {
    if (context.Request.Path == "/short")
    {
        await context.Response.WriteAsync($"Request Short Circuited");
    }
    else
    {
        await next();
    }
});

app.Use(async (context, next) => {
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
});*/

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();