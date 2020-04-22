using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace MyWeb {

    public class Student {
        public int Id { set; get; }
        public string Name { set; get; }
        public ICollection<StudentBook> StudentBooks { set; get; }
    }

    public class Book {
        public int Id { set; get; }
        public string Name { set; get; }
        public ICollection<StudentBook> StudentBooks { set; get; }
    }

    public class StudentBook {
        public int Id { set; get; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { set; get; }
        public int StudentId { set; get; }

        [ForeignKey(nameof(BookId))]
        public Book Book { set; get; }
        public int BookId { set; get; }
    }

    public class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) {

        }

        public DbSet<Student> Students { set; get; }
        public DbSet<Book> Books { set; get; }
        public DbSet<StudentBook> StudentBooks { set; get; }
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var connection = "Host=localhost;Database=XX;User Id=root;Password=1234";

            services.AddDbContext<MyContext>(options => {
                options.UseNpgsql(connection, v => {
                    v.SetPostgresVersion(11, 4);
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext context) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            context.Database.EnsureCreated();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
