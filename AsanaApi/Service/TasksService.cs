using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaApi.Service
{
	public class TasksService : AsanaApiRequest
	{
		public TasksService( string ApiKey )
			: base( ApiKey )
		{
		}

		public Models.Task[] GetTasks( TasksQuery query )
		{
			if( query == null )
			{
				throw new ArgumentNullException( "query" );
			}

			return GetTasks( "/tasks", query );
		}

		public Models.Task[] GetTasksInProject( long projectId )
		{
			return GetTasks( "/projects/" + projectId.ToString() + "/tasks", null );
		}

		private Models.Task[] GetTasks( string urlPath, TasksQuery query )
		{
			if( query != null )
			{
				urlPath = query.PopulateRequestQueryString( urlPath );
			}

			Models.Task[] nameAndIdOnly = ObjectConversions.ToTasks( ExecuteRequest( urlPath ) );
			if( nameAndIdOnly == null )
			{
				return null;
			}

			if( query == null || query.ReturnCompleteTaskRecords == false )
			{
				return nameAndIdOnly;
			}

			Models.Task[] ret = new Models.Task[ nameAndIdOnly.Length ];
			Parallel.For( 0, nameAndIdOnly.Length, i =>
			{
				ret[ i ] = GetTask( nameAndIdOnly[ i ].Id );
			} );
			return ret;
		}

		public Models.Task GetTask( long taskId )
		{
			return ObjectConversions.ToTask( ExecuteRequest( "/tasks/" + taskId.ToString() ) );
		}
	}
}
