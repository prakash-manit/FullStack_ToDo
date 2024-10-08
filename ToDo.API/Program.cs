var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Json serilizer
builder.Services.AddControllers().AddNewtonsoftJson(); 

var app = builder.Build();

//Enable cors
app.UseCors(x=>x.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod()); //http://localhost:4200/

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
