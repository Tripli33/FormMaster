using FormMaster.WEB.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddFormMasterDbContext("DbConnection");
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHelpers();

builder.Services.AddAutoMapper();

builder.Services.AddElasticSearch();

builder.Services.AddIdentity();
builder.Services.ConfigureApplicationCookie();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await app.MigrateDatabaseAndSeedDataAsync();

app.Run();