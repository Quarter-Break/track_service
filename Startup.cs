using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackService.Database.Contexts;
using TrackService.Database.Models;
using TrackService.Database.Converters;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;
using LocatieService.Repositories;
using TrackService.Repositories;
using TrackService.Services;

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
            var connection = Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<TrackContext>(
                options => options.UseSqlServer(connection).UseLazyLoadingProxies());

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:5999/gateway/*") // Only allow API gateway to call service. Dev environment.
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
            });

            // Inject converters.
            services.AddScoped<IDtoConverter<Track, TrackRequest, TrackResponse>, TrackDtoConverter>();
            services.AddScoped<IDtoConverter<Album, AlbumRequest, AlbumResponse>, AlbumDtoConverter>();
            services.AddScoped<IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse>, PlaylistDtoConverter>();
            services.AddScoped<IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse>, PlaylistTrackDtoConverter>();

            services.AddSwaggerGen();

            // Inject repositories.
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<ITrackRepository, TrackRepository>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();
            services.AddTransient<IPlaylistTrackRepository, PlaylistTrackRepository>();

            // Inject services.
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<ITrackService, TrackModelService>();
            services.AddTransient<IPlaylistService, PlaylistService>();
            services.AddTransient<IPlaylistTrackService, PlaylistTrackService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TrackContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackService");
                // Serve the swagger UI at the app's root
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
