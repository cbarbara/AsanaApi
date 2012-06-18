using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Tests
{
	public abstract class BaseTest
	{
		protected readonly string API_KEY,
			TEST_USER_NAME;

		protected readonly long TEST_USER_ID,
			TEST_WORKSPACE_ID,
			TEST_PROJECT_ID;

		public BaseTest()
		{
			API_KEY = System.Configuration.ConfigurationManager.AppSettings[ "testApiKey" ];
			TEST_USER_NAME = System.Configuration.ConfigurationManager.AppSettings[ "testUserName" ];

			TEST_USER_ID = long.Parse( System.Configuration.ConfigurationManager.AppSettings[ "testUserId" ] );
			TEST_WORKSPACE_ID = long.Parse( System.Configuration.ConfigurationManager.AppSettings[ "testWorkspaceId" ] );
			TEST_PROJECT_ID = long.Parse( System.Configuration.ConfigurationManager.AppSettings[ "testProjectId" ] );
		}
	}
}
