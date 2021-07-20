using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Models.Auth;
using WebApiPuntoVenta.Services.CompraService;
using WebApiPuntoVenta.Services.DireccionService;
using WebApiPuntoVenta.Services.EmpresaService;
using WebApiPuntoVenta.Services.ProductoService;
using WebApiPuntoVenta.Services.ProveedorService;
using WebApiPuntoVenta.Services.ServicesCliente;
using WebApiPuntoVenta.Services.UsuarioService;
using WebApiPuntoVenta.Services.VentasServices;

namespace WebApiPuntoVenta
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private string MiCord = "MiCord";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddControllers();
 
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //JWT
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(d => {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d => {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiPuntoVenta", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<PuntoDeVentaContext>(option => 
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<IDireccionServices, DireccionServices>();
            services.AddScoped<IProveedorServices, ProveedorServices>();
            services.AddScoped<IVentasServices, VentasServices>();
            services.AddScoped<ICompraServices, CompraServices>();
            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IEmpresaService, EmpresaService>();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

           
            app.UseCors(builder => builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiPuntoVenta v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
