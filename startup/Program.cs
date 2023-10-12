using Microsoft.EntityFrameworkCore;
using startup.Models;

var builder = WebApplication.CreateBuilder(args);

// lấy chuỗi kết nối từ file appsetting.json
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
// truyền chuỗi kết nối vào tham số option của contructor DbContext
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

// định nghĩa 1 router bằng name
// khi vào router đó thì dùng pattern: thì gọi controller cần gọi và phương thức cần dùng trong controller đó
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
