using Microsoft.EntityFrameworkCore;
using WeatherApi.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // �������� ������ ����������� �� ����� ������������
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");

        // ��������� �������� ApplicationContext � �������� ������� � ����������
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
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
    }
}