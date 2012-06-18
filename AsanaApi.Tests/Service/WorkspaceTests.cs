using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using AsanaApi.Service;
using AsanaApi.Models;

namespace AsanaApi.Tests.Service
{
	[TestFixture]
	public class WorkspaceTests : BaseTest
	{
		[Test]
		public void GetWorkspaces()
		{
			WorkspacesService service = new WorkspacesService( API_KEY );
			Workspace[] workspaces = service.GetWorkspaces();
			Assert.IsNotNull( workspaces, "we should have gotten some workspaces back" );
			foreach( var item in workspaces )
			{
				Assert.IsNotNull( item.Name, "name should always come back" );
			}
		}

		[Test]
		public void GetProjectsOfWorkspace()
		{
			WorkspacesService service = new WorkspacesService( API_KEY );
			Project[] projects = service.GetProjects( TEST_WORKSPACE_ID );
			Assert.IsNotNull( projects, "we should have gotten some projects back" );
			foreach( var item in projects )
			{
				Assert.IsNotNull( item.Name, "name should always come back" );
				Assert.AreEqual( DateTime.MinValue, item.CreatedAt, "CreatedAt isn't populated by default" );
				Assert.IsFalse( item.ModifiedAt.HasValue, "modified date is null" );
				Assert.IsNull( item.Followers, "shouldn't be any followers" );
			}
		}
	}
}
