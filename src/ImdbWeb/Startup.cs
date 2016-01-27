using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using ImdbDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace ImdbWeb
{
	public class Startup
	{

		private IConfigurationRoot _configuration;
		public Startup()
		{
			var cb = new ConfigurationBuilder();
			cb.AddJsonFile("appSettings.json");
			_configuration = cb.Build();

		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<ImdbContext>();

			services.AddOptions();

			services.Configure<ImdbOptions>(_configuration.GetSection("Imdb"));
			
			services.AddMvc();

			services.Configure<MvcOptions>(options =>
			{
				options.OutputFormatters.Clear();
				options.InputFormatters.Clear();

				var jss = new JsonSerializerSettings
				{
					Formatting = Formatting.Indented,
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				};
				jss.Converters.Add(new StringEnumConverter());

				options.InputFormatters.Add(new JsonInputFormatter(jss));
				options.OutputFormatters.Add(new JsonOutputFormatter(jss));

				options.InputFormatters.Add(new XmlSerializerInputFormatter());
				options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			app.UseIISPlatformHandler();
			app.UseStaticFiles();
			app.UseDeveloperExceptionPage();

			app.UseMvc(routes => {
				routes.MapRoute("Area", "{area:exists}/{Controller=Home}/{Action=Index}/{id?}");
				routes.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
			});
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
