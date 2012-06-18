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
	public class ProjectsTests : BaseTest
	{
		[Test]
		public void GetProjects()
		{
			ProjectsService service = new ProjectsService( API_KEY );
			Project[] projects = service.GetProjects();
			Assert.IsNotNull( projects, "we should have gotten some projects back" );
			foreach( var item in projects )
			{
				Assert.IsNotNull( item.Name, "name should always come back" );
				Assert.AreEqual( DateTime.MinValue, item.CreatedAt, "CreatedAt isn't populated by default" );
				Assert.IsFalse( item.ModifiedAt.HasValue, "modified date is null" );
				Assert.IsNull( item.Followers, "shouldn't be any followers" );
			}
		}

		[Test]
		public void GetProjectsWithFields()
		{
			ProjectsService service = new ProjectsService( API_KEY );
			Project[] projects = service.GetProjects( new OptionalFields[] { OptionalFields.Project_CreatedAt, OptionalFields.Project_ModifiedAt } );
			Assert.IsNotNull( projects, "we should have gotten some projects back" );

			foreach( var item in projects )
			{
				Assert.IsNotNull( item.Name, "name should always come back" );
				Assert.AreNotEqual( DateTime.MinValue, item.CreatedAt, "CreatedAt should be populated" );
				Assert.IsTrue( item.ModifiedAt.HasValue, "modified date shouldn't be null" );
				Assert.IsNull( item.Followers, "shouldn't be any followers" );
			}
		}
	}
}
