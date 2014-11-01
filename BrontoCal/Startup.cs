using Autofac;
using Autofac.Integration.WebApi;
using Lansky.BrontoCal.Services;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Lansky.BrontoCal
{
    public class Startup
    {
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();

			this.RegisterAutofacIntoWebApi(config);

			appBuilder.UseWebApi(config);
		}

		private void RegisterAutofacIntoWebApi(HttpConfiguration config)
		{
			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<HtmlParser>();
			builder.RegisterType<ICalFormatter>();

			var container = builder.Build();
			var resolver = new AutofacWebApiDependencyResolver(container);
			config.DependencyResolver = resolver;
		}
    }
}
