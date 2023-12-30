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

app.MapGet("/beneficiaries/{id}", (Guid id) =>
{
    var beneficiary = beneficiaries.FirstOrDefault(b => b.Id == id);
    return beneficiary != null ? Results.Ok(beneficiary) : Results.NotFound();
}).WithName("GetBeneficiaryById")
.WithOpenApi();

var benefactors = new List<Benefactor>();

app.MapPost("/benefactors", (Benefactor benefactor) =>
{
    benefactor.Id = Guid.NewGuid();
    benefactors.Add(benefactor);
    return Results.Created($"/benefactors/{benefactor.Id}", benefactor);
}).WithName("CreateBenefactor")
.WithOpenApi();

app.MapGet("/benefactors", () => Results.Ok(benefactors)).WithName("GetBenefactors")
.WithOpenApi();

app.MapGet("/benefactors/{id}", (Guid id) =>
{
    var benefactor = benefactors.FirstOrDefault(b => b.Id == id);
    return benefactor != null ? Results.Ok(benefactor) : Results.NotFound();
}).WithName("GetBenefactorById")
.WithOpenApi();

app.MapPost("/benefactors/{benefactorId}/benefits", (Guid benefactorId, Benefit benefit) =>
{
    var benefactor = benefactors.FirstOrDefault(b => b.Id == benefactorId);
    if (benefactor == null) return Results.NotFound();

    benefactor.ProvidedBenefits.Add(benefit);
    return Results.Ok(benefactor);
}).WithName("AddBenefitToBenefactor")
.WithOpenApi();

var benefits = new List<Benefit>();

app.MapGet("/benefits", () => Results.Ok(benefits)).WithName("GetBenefits")
.WithOpenApi();

app.MapPost("/benefits", (Benefit benefit) =>
{
    benefit.Id = Guid.NewGuid();
    benefits.Add(benefit);
    return Results.Created($"/benefits/{benefit.Id}", benefit);
}).WithName("CreateBenefit");

app.MapPost("/beneficiaries/{beneficiaryId}/claim", (Guid beneficiaryId, Guid benefitId, Guid benefactorId) =>
{
    var beneficiary = beneficiaries.FirstOrDefault(b => b.Id == beneficiaryId);

    if (beneficiary == null) return Results.NotFound("Benficiary Not Found");

    var benefactor = benefactors.FirstOrDefault(b => b.Id == benefactorId);

    if (benefactor == null) return Results.NotFound("Benefactor Not Fount");

    var benefit = benefactor.ProvidedBenefits.FirstOrDefault(b => b.Id == benefitId);

    if (benefit == null) return Results.NotFound("Benefit Not Fount");

    beneficiary.BenefitsClaimed.Add(benefit);
    benefactor.ProvidedBenefits.Remove(benefit);

    return Results.Ok(beneficiary);
}).WithName("ClaimBenefit")
.WithOpenApi();


app.Run();
