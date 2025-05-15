using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Repo.Repository;
using ComplaintSystem.Repository.Repository;
using ComplaintSystem.Service.Services;
using EasyComplaint.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IWorkflowRepo, WorkflowRepo>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();




            return services;
        }

    }
}
