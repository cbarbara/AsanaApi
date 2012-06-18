using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsanaApi.Models;

namespace AsanaApi.Service
{
	public class StoriesService : AsanaApiRequest
	{
		public StoriesService( string ApiKey )
			: base( ApiKey )
		{
		}

		static OptionalFields[] _allFields = new OptionalFields[] {OptionalFields.Story_Source, OptionalFields.Story_Target, OptionalFields.Story_Target_Name };
		public static OptionalFields[] AllOptionalFieldsForStories
		{
			get
			{
				return _allFields;
			}
		}

		public Story[] GetStories( Task target, OptionalFields[] returnedFields = null )
		{
			return GetStories( target.Id, returnedFields );
		}
		public Story[] GetStories( long taskId, OptionalFields[] returnedFields = null )
		{
			return GetStories( "/tasks/" + taskId.ToString() + "/stories", returnedFields );
		}

		private Story[] GetStories( string urlPath, OptionalFields[] returnedFields = null )
		{
			return ObjectConversions.ToStories(
				ExecuteRequest( urlPath, returnedFields, ",created_at,id,text,source,type,created_by,created_by.name,target.completed" )
			);
		}
	}
}
