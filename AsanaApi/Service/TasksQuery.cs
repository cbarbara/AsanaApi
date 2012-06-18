using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Service
{
	public class TasksQuery
	{
		public TasksQuery()
		{
		}

		public long? ProjectId { get; set; }
		public long? AssigneeId { get; set; }
		public long? WorkspaceId { get; set; }

		public bool ReturnCompleteTaskRecords { get; set; }

		internal string PopulateRequestQueryString( string urlPath )
		{
			bool qsAlready = urlPath.Contains( "?" );

			if( ProjectId.HasValue )
			{
				urlPath = AddParameter( qsAlready, urlPath, "project=" + ProjectId.Value.ToString() );
				qsAlready = true;
			}
			if( AssigneeId.HasValue )
			{
				urlPath = AddParameter( qsAlready, urlPath, "assignee=" + AssigneeId.Value.ToString() );
				qsAlready = true;
			}
			if( WorkspaceId.HasValue )
			{
				urlPath = AddParameter( qsAlready, urlPath, "workspace=" + WorkspaceId.Value.ToString() );
				qsAlready = true;
			}

			return urlPath;
		}

		private string AddParameter( bool qsAlready, string urlPath, string newParameter )
		{
			if( qsAlready )
			{
				urlPath += "&";
			}
			else
			{
				urlPath += "?";
			}
			urlPath += newParameter;
			return urlPath;
		}
	}
}
