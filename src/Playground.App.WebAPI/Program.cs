
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen(options =>
{
    // Enable annotations for descriptions, etc.
    options.EnableAnnotations();

    // You can provide additional metadata here
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Playground App API",
        Version = "v1",
        Description = "An example API for Playground App using OpenAPI & Swagger",
    });

    // Optional: If you want to add XML comments (useful for summarizing methods, models, etc.)
    var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
