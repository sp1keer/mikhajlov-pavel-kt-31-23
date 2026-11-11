using mikhajlov_pavel_kt_31_23.Services.Interfaces;

namespace mikhajlov_pavel_kt_31_23.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IStudentDisciplineService, StudentDisciplineService>();
            services.AddScoped<IGroupDisciplineService, GroupDisciplineService>();
            return services;
        }
    }
}
