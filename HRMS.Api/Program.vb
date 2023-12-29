Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports HRMS.Business
Imports HRMS.Repository
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Rewrite
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.OpenApi

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Add services to the container.



        builder.Services.AddControllers().AddJsonOptions(Sub(opt) opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
        ' Learn more about configuring Swagger/OpenAPI at https://aka.ms/
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        builder.Services.AddDbContext(Of HRMSContext)
        builder.Services.AddAutoMapper(GetType(AutoMapperProfiles))

        builder.Services.AddScoped(Of ILeavesTypeRepository, LeavesTypeRepository)
        builder.Services.AddScoped(Of ILeavesTypeBs, LeavesTypeBs)

        builder.Services.AddScoped(Of ICandidateRepository, CandidateRepository)
        builder.Services.AddScoped(Of ICandidateBs, CandidateBs)

        builder.Services.AddScoped(Of IEmployeeRepository, EmployeeRepository)
        builder.Services.AddScoped(Of IEmployeeBs, EmployeeBs)

        builder.Services.AddScoped(Of IDepartmentRepository, DepartmentRepository)
        builder.Services.AddScoped(Of IDepartmentBs, DepartmentBs)

        builder.Services.AddScoped(Of IPositionRepository, PositionRepository)
        builder.Services.AddScoped(Of IPositionBs, PositionBs)

        builder.Services.AddScoped(Of IInterviewRepository, InterviewRepository)
        builder.Services.AddScoped(Of IInterviewBs, InterviewBs)

        builder.Services.AddScoped(Of IUserRepository, UserRepository)
        builder.Services.AddScoped(Of IUserBs, UserBs)

        builder.Services.AddScoped(Of IPerformanceReviewRepository, PerformanceReviewRepository)
        builder.Services.AddScoped(Of IPerformanceReviewBs, PerformanceReviewBs)


        builder.Services.AddScoped(Of ILeaveRepository, LeaveRepository)
        builder.Services.AddScoped(Of ILeaveBs, LeaveBs)


        builder.Services.AddScoped(Of ISalaryRepository, SalaryRepository)
        builder.Services.AddScoped(Of ISalaryBs, SalaryBs)

        builder.Services.AddScoped(Of IImageRepository, ImageRepository)
        builder.Services.AddScoped(Of IImageBs, ImageBs)

        builder.Services.AddSingleton(Of IWebRootProvider, WebRootProvider)

        Dim app = builder.Build()

        app.UseRewriter(New RewriteOptions().AddRedirect("^$", "swagger"))

        '  Configure the HTTP request pipeline.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If

        app.UseHttpsRedirection()
        app.UseStaticFiles()

        app.UseAuthorization()

        app.MapControllers()

        app.Run()
    End Sub
End Module