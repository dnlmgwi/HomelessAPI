using HomelessAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var beneficiaries = new List<Beneficiary>();

app.MapGet("/beneficiaries", () => Results.Ok(beneficiaries)).WithName("GetBeneficiary")
.WithOpenApi();

app.MapPost("/beneficiaries", (Beneficiary beneficiary) =>
{
    beneficiary.Id = Guid.NewGuid();
    beneficiaries.Add(beneficiary);
    return Results.Created($"/beneficiaries/{beneficiary.Id}", beneficiary);
}).WithName("CreateBeneficiary")
.WithOpenApi();

app.Run();
