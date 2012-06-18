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
	public class UserTests : BaseTest
	{
		[Test]
		public void GetMyUser()
		{
			UsersService service = new UsersService( API_KEY );
			User user = service.GetUser();
			Assert.IsNotNull( user );
		}

		[Test]
		public void GetUser()
		{
			UsersService service = new UsersService( API_KEY );
			User user = service.GetUser( TEST_USER_ID );
			Assert.IsNotNull( user );
			Assert.AreEqual( TEST_USER_ID, user.Id, "user id is incorrect" );
			Assert.AreEqual( TEST_USER_NAME, user.Name, "name is incorrect" );
			Assert.IsNotNull( user.Email, "email shouldn't be null" );
			Assert.IsNotNull( user.Workspaces, "Workspaces shouldn't be null" );
			Assert.IsTrue( user.Workspaces.Length > 0, "user.Workspaces.Length == " + user.Workspaces.Length.ToString() + " - it should be > 0 " );

		}

		[Test]
		public void GetUsers()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsers();
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			Assert.IsNull( users[ 0 ].Workspaces, "Workspaces should come back null" );
			Assert.IsNull( users[ 0 ].Email, "Email should come back null" );
		}

		[Test]
		public void GetUsersInWorkspace()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsersInWorkspace( TEST_WORKSPACE_ID );
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			Assert.IsNull( users[ 0 ].Workspaces, "Workspaces should come back null" );
			Assert.IsNull( users[ 0 ].Email, "Email should come back null" );
		}

		[Test]
		public void GetUsersWithEmail()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsers( new OptionalFields[] { OptionalFields.User_Email } );
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			Assert.IsNull( users[ 0 ].Workspaces, "Workspaces should come back null" );
			Assert.IsNotNull( users[ 0 ].Email, "Email should come back" );
		}

		[Test]
		public void GetUsersWithWorkspaces()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsers( new OptionalFields[] { OptionalFields.User_Workspaces } );
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			Assert.IsNotNull( users[ 0 ].Workspaces, "Workspaces should come back" );
			Assert.IsNull( users[ 0 ].Email, "Email should come back null" );
		}

		[Test]
		public void GetUsersWithWorkspacesAndEmail()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsers( new OptionalFields[] { OptionalFields.User_Workspaces, OptionalFields.User_Email } );
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			Assert.IsNotNull( users[ 0 ].Workspaces, "Workspaces should come back" );
			Assert.IsNotNull( users[ 0 ].Email, "Email should come back" );
		}

		[Test]
		public void GetUsersWithWorkspacesAndEmail_DifferentEnumOrdering()
		{
			UsersService service = new UsersService( API_KEY );
			User[] users = service.GetUsers( new OptionalFields[] { OptionalFields.User_Workspaces, OptionalFields.User_Email } );
			Assert.IsNotNull( users );
			Assert.IsTrue( users.Length > 0, "users.Length == " + users.Length.ToString() + " - it should be > 0 " );
			
			Assert.IsNotNull( users[ 0 ].Workspaces, "Workspaces should come back" );
			Assert.IsNotNull( users[ 0 ].Email, "Email should come back" );
			Assert.AreEqual( TEST_USER_ID, users[ 0 ].Id, "user id is incorrect" );
			Assert.AreEqual( TEST_USER_NAME, users[ 0 ].Name, "name is incorrect" );
		}
	}
}
