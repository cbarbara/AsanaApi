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

		public static string ToRelativeTime( TimeSpan ts )
		{
			//stolen from http://stackoverflow.com/questions/11/calculating-relative-time
			const int SECOND = 1;
			const int MINUTE = 60 * SECOND;
			const int HOUR = 60 * MINUTE;
			const int DAY = 24 * HOUR;
			const int MONTH = 30 * DAY;
			double delta = Math.Abs( ts.TotalSeconds );

			if( delta < 0 )
			{
				return "not yet";
			}
			if( delta < 1 * MINUTE )
			{
				return ts.Seconds == 1 ? "one second" : ts.Seconds + " seconds";
			}
			if( delta < 2 * MINUTE )
			{
				return "a minute";
			}
			if( delta < 45 * MINUTE )
			{
				return ts.Minutes + " minutes";
			}
			if( delta < 90 * MINUTE )
			{
				return "an hour";
			}
			if( delta < 24 * HOUR )
			{
				return ts.Hours + " hours";
			}
			if( delta < 48 * HOUR )
			{
				return "yesterday";
			}
			if( delta < 30 * DAY )
			{
				return ts.Days + " days";
			}
			if( delta < 12 * MONTH )
			{
				int months = Convert.ToInt32( Math.Floor( (double)ts.Days / 30 ) );
				return months <= 1 ? "one month" : months + " months";
			}
			else
			{
				int years = Convert.ToInt32( Math.Floor( (double)ts.Days / 365 ) );
				return years <= 1 ? "one year" : years + " years";
			}
		}
	}
}