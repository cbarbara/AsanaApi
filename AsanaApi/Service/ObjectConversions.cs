using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsanaApi.Models;

namespace AsanaApi.Service
{
	class ObjectConversions
	{
		internal static Assignee[] ToAssignees( dynamic dUsers )
		{
			if( dUsers == null )
			{
				return null;
			}

			List<Assignee> ret = new List<Assignee>();
			foreach( var item in dUsers )
			{
				ret.Add( ToAssignee( item ) );
			}
			return ret.ToArray();
		}
		internal static Assignee ToAssignee( dynamic dUser )
		{
			if( dUser == null )
			{
				return null;
			}

			Assignee user = new Assignee()
			{
				Id = long.Parse( dUser.id.ToString() ),
				Name = dUser.name
			};

			return user;
		}
		internal static User ToUser( dynamic dUser )
		{
			if( dUser == null )
			{
				return null;
			}

			User user = new User()
			{
				Id = long.Parse( dUser.id.ToString() ),
				Name = dUser.name,
				Email = dUser.email
			};

			user.Workspaces = ToWorkspaces( dUser.workspaces );

			return user;
		}
		internal static User[] ToUsers( dynamic dUsers )
		{
			if( dUsers == null )
			{
				return null;
			}

			List<User> ret = new List<User>();
			foreach( var item in dUsers )
			{
				ret.Add( ToUser( item ) );
			}
			return ret.ToArray();
		}

		internal static Workspace[] ToWorkspaces( dynamic dWorkspaces )
		{
			if( dWorkspaces == null )
			{
				return null;
			}
			
			List<Workspace> ret = new List<Workspace>();
			foreach( var item in dWorkspaces )
			{
				ret.Add( ToWorkspace( item ) );
			}
			return ret.ToArray();
		}
		internal static Workspace ToWorkspace( dynamic dWorkspace )
		{
			if( dWorkspace == null )
			{
				return null;
			}
			
			return new Workspace()
			{
				Id = long.Parse( dWorkspace.id.ToString() ),
				Name = dWorkspace.name
			};
		}


		internal static Task[] ToTasks( dynamic dTasks )
		{
			if( dTasks == null )
			{
				return null;
			}
			
			List<Task> ret = new List<Task>();
			foreach( var item in dTasks )
			{
				ret.Add( ToTask( item ) );
			}
			return ret.ToArray();

		}
		internal static Task ToTask( dynamic dTask )
		{
			if( dTask == null )
			{
				return null;
			}

			/*
			{
				"id": 752517863351,
				"created_at": "2012-04-22T03:29:33.284Z",
				"modified_at": "2012-05-05T14:54:17.789Z",
				"name": "require a login",
				"notes": "",
				"assignee": {
					"id": 752517793331,
					"name": "Chris Barbara"
				},
				"completed": false,
				"assignee_status": "later",
				"due_on": null,
				"projects": [
					{
						"id": 752517863337,
						"name": "T-1000"
					}
				],
				"workspace": {
					"id": 752517862999,
					"name": "Xignite"
				},
				"followers": [
					{
						"id": 752517793331,
						"name": "Chris Barbara"
					}
				],
				"completed_at": null
			}
			 */
			Task t = new Task()
			{
				Id = long.Parse( dTask.id.ToString() ),
				Name = dTask.name
			};
			
			//if this is null, then we are just dealing with a list of Ids & names.
			if( dTask.created_at == null )
			{
				return t;
			}

			t.CreatedAt = DateTime.Parse( dTask.created_at );

			if( dTask.modified_at != null )
			{
				t.ModifiedAt = DateTime.Parse( dTask.modified_at );
			}

			t.Notes = dTask.notes;

			t.Assignee = ToAssignee( dTask.assignee );
			
			t.Completed = dTask.completed;
			
			SchedulingStatus ss = SchedulingStatus.Inbox;
			if( Enum.TryParse<SchedulingStatus>( dTask.assignee_status, true, out ss ) )
			{
				t.AssigneeStatus = ss;
			}

			if( dTask.due_on != null )
			{
				t.DueDate = DateTime.Parse( dTask.due_on );
			}

			t.Projects = ToProjects( dTask.projects );

			t.Workspace = ToWorkspace( dTask.workspace );

			t.Followers = ToAssignees( dTask.followers );

			if( dTask.completed_at != null )
			{
				t.CompletedAt = DateTime.Parse( dTask.completed_at );
			}

			return t;
		}

		internal static Project[] ToProjects( dynamic dProjects )
		{
			if( dProjects == null )
			{
				return null;
			}

			List<Project> ret = new List<Project>();
			foreach( var item in dProjects )
			{
				ret.Add( ToProject( item ) );
			}
			return ret.ToArray();
		}

		internal static Project ToProject( dynamic dProject )
		{
			if( dProject == null )
			{
				return null;
			}

			Project p = new Project()
			{
				Id = long.Parse( dProject.id.ToString() ),
				Name = dProject.name
			};
			
			//if this is null, then we are just dealing with a list of Ids & names.
			if( dProject.created_at == null )
			{
				return p;
			}

			p.CreatedAt = DateTime.Parse( dProject.created_at );

			if( dProject.modified_at != null )
			{
				p.ModifiedAt = DateTime.Parse( dProject.modified_at );
			}

			p.Notes = dProject.notes;

			p.Workspace = ToWorkspace( dProject.workspace );

			p.Followers = ToAssignees( dProject.followers );

			return p;
		}

		internal static Story[] ToStories( dynamic dStories )
		{
			if( dStories == null )
			{
				return null;
			}

			List<Story> ret = new List<Story>();
			foreach( var item in dStories )
			{
				ret.Add( ToStory( item ) );
			}
			return ret.ToArray();
		}
		internal static Story ToStory( dynamic dStory )
		{
			if( dStory == null )
			{
				return null;
			}
			/*
			{
				"id": 991498733530,
				"created_at": "2012-06-02T16:02:28.692Z",
				"type": "system",
				"text": "added to Api-Test-Project",
				"created_by": {
					"id": 752517793331,
					"name": "Chris Barbara"
				}
			}
			*/

			Story s = new Story()
			{
				Id = long.Parse( dStory.id.ToString() ),
				CreatedAt = DateTime.Parse( dStory.created_at ),
				Text = dStory.text,
				CreatedBy = ToAssignee( dStory.created_by ),
			};

			StoryType st = StoryType.System;
			if( Enum.TryParse<StoryType>( dStory.type, true, out st ) )
			{
				s.Type = st;
			}

			/*
	[
		{
			"target": {
				"id": 991498733529,
				"name": "Make a create c# project"
			},
			"source": "web",
		}
	]
			 */

			StorySource ss = StorySource.unknown;
			if( Enum.TryParse<StorySource>( dStory.source, true, out ss ) )
			{
				s.Source = ss;
			}

			if( dStory.target != null )
			{
				//HACK: this is how we can tell if it is a Task or a Project
				if( dStory.target.completed == null )
				{
					s.Target = ToProject( dStory.target );
				}
				else
				{
					s.Target = ToTask( dStory.target );
				}
			}

			return s;
		}
	}
}
