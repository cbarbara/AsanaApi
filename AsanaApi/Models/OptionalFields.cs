using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public enum OptionalFields
	{
		User_Email,
		User_Name,
		User_Workspaces,
		User_Workspaces_Name,

		Project_CreatedAt,
		Project_ModifiedAt,

		Story_Source,
		Story_Target,
		Story_Target_Name,
	}

	internal class OptionFieldsHelper
	{
		internal static string ToOptionalFieldNames( OptionalFields[] returnedFields )
		{
			StringBuilder sb = new StringBuilder( returnedFields.Length * 10 );
			for( int i = 0; i < returnedFields.Length; i++ )
			{
				if( i > 0 )
				{
					sb.Append( "," );
				}
				sb.Append( ToOptionalFieldName( returnedFields[ i ] ) );
			}
			return sb.ToString();
		}

		private static string ToOptionalFieldName( OptionalFields optionalField )
		{
			switch( optionalField )
			{
				case OptionalFields.User_Email:
					return "email";
					
				case OptionalFields.User_Name:
					return "name";

				case OptionalFields.User_Workspaces:
					return "workspaces";

				case OptionalFields.User_Workspaces_Name:
					return "workspaces.name";

				case OptionalFields.Project_CreatedAt:
					return "created_at";

				case OptionalFields.Project_ModifiedAt:
					return "modified_at";

				case OptionalFields.Story_Source:
					return "source";

				case OptionalFields.Story_Target:
					return "target";

				case OptionalFields.Story_Target_Name:
					return "target.name";

			}
			return "";
		}
	}
}
