using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Website
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters( GlobalFilterCollection filters )
		{
			filters.Add( new HandleErrorAttribute() );
		}

		public static void RegisterRoutes( RouteCollection routes )
		{
			routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{option}",
				defaults: new { controller = "Home", action = "Index", option = UrlParameter.Optional }
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters( GlobalFilters.Filters );
			RegisterRoutes( RouteTable.Routes );

			BundleTable.Bundles.RegisterTemplateBundles();

			Shared.Me = new AsanaApi.Service.UsersService( Shared.API_KEY ).GetUser();
			Shared.Projects = new AsanaApi.Service.ProjectsService( Shared.API_KEY ).GetProjects(
				new AsanaApi.Models.OptionalFields[] {
					AsanaApi.Models.OptionalFields.Project_CreatedAt,
					AsanaApi.Models.OptionalFields.Project_ModifiedAt
				});
		}
	}
}