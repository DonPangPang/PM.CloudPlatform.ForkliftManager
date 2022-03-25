using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.Options;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.IO.Converters;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Authorization;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Services;
using PM.CloudPlatform.ForkliftManager.Apis.Redis;
using Microsoft.AspNetCore.Authorization;

namespace PM.CloudPlatform.ForkliftManager.Apis
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"> </param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// </summary>
        /// <param name="services"> </param>
        public void ConfigureServices(IServiceCollection services)
        {
            //redis缓存
            var section = Configuration.GetSection("Redis:Default");
            //连接字符串
            string _connectionString = section.GetSection("Connection").Value;
            //实例名称
            string _instanceName = section.GetSection("InstanceName").Value;
            //默认数据库
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0");
            services.AddSingleton(new RedisHelper(_connectionString, _instanceName, _defaultDB));

            var AppContig = Configuration.GetSection("TokenParameter").Get<PermissionRequirement>();
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("Identify", policy =>
                        policy.Requirements.Add(new PermissionRequirement()));
                })
                .AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AppContig.Audience,
                        ValidIssuer = AppContig.Issuer,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppContig.Secret))
                    };

                    options.SaveToken = true;
                });

            services
                .AddControllers(setup =>
                {
                    setup.ReturnHttpNotAcceptable = true;
                })
                .AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    setup.SerializerSettings.Converters.Add(new GeometryConverter());
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.pmxxkj.com",
                            Title = "有错误",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请看详细信息",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PM.CloudPlatform.ForkliftManager.Apis", Version = "v1" });
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //    //... and tell Swagger to use those XML comments.
            //    c.IncludeXmlComments(xmlPath, true);
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ForkliftManager.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddDbContext<ForkliftManagerDbContext>(opts =>
            //{
            //    opts.UseSqlite("Data Source=test.db");
            //});

            services.AddDb(Configuration);

            services.AddRepositoryByNamespace("PM.CloudPlatform.ForkliftManager.Apis.Repositories");

            services.AddScoped<IGeneralRepository, GeneralRepository>();

            services.AddCors(opts =>
            {
                opts.AddPolicy("Any", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            //services.AddScoped<TcpSocketServerHostedService>();

            //services.AddHostedService<TcpSocketServerHostedService>(x => new TcpSocketServerHostedService(new ServerOption()
            //{
            //}));

            // Redis
            //services.AddDistributedRedisCache(opts =>
            //{
            //    opts.InstanceName = "";
            //    opts.Configuration = "";
            //});

            services.Configure<ServerOption>(Configuration.GetSection("ServerOption"));
            services.Configure<KafkaOption>(Configuration.GetSection("KafkaOption"));
            //services.AddSingleton<TcpSocketServerHostedService>();

            services.AddDistributedMemoryCache();
            services.AddLoginUserInfo();

            services.AddTcpServer();

            services.AddWsServer();

            services.AddJTT808();

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //验证码
            //services.AddMemoryCacheCaptcha(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// </summary>
        /// <param name="app"> </param>
        /// <param name="env"> </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseLoginUserInfo();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PM.CloudPlatform.ForkliftManager.Apis v1");
            });

            app.UseStaticFiles();
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Any");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAutoMapperMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}