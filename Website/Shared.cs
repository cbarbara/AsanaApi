using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website
{
	public class Shared
	{
		public static string API_KEY = System.Configuration.ConfigurationManager.AppSettings[ "AsanaApiKey" ];

		public static User Me;

		public static Project[] Projects;

		public static int DefaultProjectIndex = 0;
	}
}