using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TrackService.Database.Contexts;
using TrackService.Database.Models;
using TrackService.Database.Converters;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Inject controllers.
            services.AddControllers();
            // Inject database context
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TrackContext>(
                options => options.UseSqlServer(connection).UseLazyLoadingProxies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackService", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
            });

            // Inject converters
            services.AddScoped<IDtoConverter<Track, TrackRequest, TrackResponse>, TrackDtoConverter>();
            services.AddScoped<IDtoConverter<Album, AlbumRequest, AlbumResponse>, AlbumDtoConverter>();
            services.AddScoped<IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse>, PlaylistDtoConverter>();
            services.AddScoped<IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse>, PlaylistTrackDtoConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TrackContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackService v1"));
            }

            app.UseCors(MyAllowSpecificOrigins);

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
