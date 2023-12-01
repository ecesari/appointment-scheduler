using iPractice.Domain.Repository;
using iPractice.Infrastructure.Repository.Base;
using iPractice.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using iPractice.Application.Common.Configurations;

namespace iPractice.Api.ServiceCollections
{

    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPsychologistRepository, PsychologistRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());

            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }

}
