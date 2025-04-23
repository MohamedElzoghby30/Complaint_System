using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Repo.Repository;
using ComplaintSystem.Repository.Repository;
using ComplaintSystem.Service.Services;
using EasyComplaint.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintSystem.Api.Extension
{
    public static class InfrastractureServiceExtensions
    {

        public static IServiceCollection AddInfrastractureService(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddScoped<Token>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<IComplaintService, ComplaintService>();
            services.AddScoped<IComplaintTypeRepository, ComplaintTypeRepository>();
            services.AddScoped<IComplaintTypeService, ComplaintTypeService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();




            return services;
        }

    }
}
