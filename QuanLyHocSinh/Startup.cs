using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace QuanLyHocSinh
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLyHocSinh", Version = "v1" });
            });
            services.AddDbContextFactory<QuanLyHocSinhContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
            //services.AddMvc(option => option.EnableEndpointRouting = false)
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
            });

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IScheduleDetailRepository, ScheduleDetailRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITestTypeRepository, TestTypeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<INewsFeedRepository, NewsFeedRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyHocSinh v1"));
            }
            else
            {
                app.UseSwagger(); 
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyHocSinh v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
