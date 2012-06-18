using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using AsanaApi.Models;

namespace AsanaApi.Tests.Models
{
	[TestFixture]
	public class TargetTests : BaseTest
	{
		[Test]
		public void PointlessTest()
		{
			Project project = new Project() { Id = 100 };
			Task task = new Task(){ Id = 100 };

			Assert.IsTrue( project is ITarget, "project should be a ITarget" );
			Assert.IsTrue( task is ITarget, "project should be a ITarget" );
			
			AssertITargetId( 100, project );
			AssertITargetId( 100, task );
		}

		private void AssertITargetId( long id, ITarget iTarget )
		{
			Assert.AreEqual( id, iTarget.Id, "Id is incorrect" );
		}
	}
}
