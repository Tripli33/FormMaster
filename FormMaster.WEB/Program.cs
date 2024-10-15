using FormMaster.WEB.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddFormMasterDbContext("DbConnection");

builder.Services.AddAutoMapper();

builder.Services.AddServices();

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

app.Run();