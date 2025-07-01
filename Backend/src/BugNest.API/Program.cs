using System.Text;
using System.Text.Json.Serialization;
using BugNest.Application.Interfaces;
using BugNest.Infrastructure.Data;
using BugNest.Infrastructure.Services;
using BugNest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using FluentValidation;
using BugNest.Application.Validators;
using BugNest.Application.Users.Commands;
using BugNest.Application.UseCases.Users.Validators;
using BugNest.Application.Projects.Commands.CreateProject;
using BugNest.Application.UseCases.Projects.Validators;
using BugNest.Application.Projects.Queries.GetAllProjectSummaries;
using BugNest.Application.Projects.Queries.GetProjectDetail;
using BugNest.Application.Projects.Queries.GetProjectMembers;
using BugNest.Application.Projects.Queries.GetProjectsByOwner;
using BugNest.Application.Projects.Queries.GetProjectSummary;
using BugNest.Application.Projects.Queries.GetRecentProjects;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// 🔌 1. Database setup
builder.Services.AddDbContext<BugNestDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<CreateUpdateProfileValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateLoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTagValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetProjectDetailQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetProjectMembersQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetProjectsByOwnerQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetProjectSummaryQueryValidator>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddHttpContextAccessor();
// 📦 MediatR & FluentValidation
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CreateLoginValidator).Assembly);

// 🧩 Baru di sini daftar pipeline-nya
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditLoggingBehavior<,>));


// 🧩 3. Dependency Injection - Repository & Service only (Handler pakai MediatR)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IIssueHistoryRepository, IssueHistoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
builder.Services.AddScoped<IRecentProjectRepository, RecentProjectRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddHttpContextAccessor();

// 🌐 4. CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// 🔐 5. JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// 📚 6. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugNest API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Masukkan token JWT Anda. Contoh: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🧾 7. Controller & JSON enum handling
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 🚀 8. Build App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
    ),
    RequestPath = "" // serve langsung dari root
});

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
