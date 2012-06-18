using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace AsanaApi.Service
{
	class AsanaApiResponse
	{
		public bool Failed { get; private set; }
		public string Content { get; private set; }
		public HttpStatusCode StatusCode { get; private set; }

		internal AsanaApiResponse Execute( HttpWebRequest request )
		{
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				ReadResponse( response );
			}
			catch( WebException wex )
			{
				ReadResponse( (HttpWebResponse)wex.Response );
				
				Failed = true;
			}
			catch( Exception )
			{
				Failed = true;
			}

			return this;
		}

		private void ReadResponse( HttpWebResponse response )
		{
			using( StreamReader sr = new StreamReader( response.GetResponseStream() ) )
			{
				this.StatusCode = response.StatusCode;

				Content = sr.ReadToEnd();
				sr.Close();
			}
		}

		internal AsanaApiResponse CheckForError()
		{
			if( Failed == false )
			{
				return this;
			}

			switch( this.StatusCode )
			{
				case System.Net.HttpStatusCode.BadRequest:
					//Invalid request. This usually occurs because of a missing or malformed parameter. Check the documentation and the syntax of your request and try again.
					throw new ArgumentException( this.Content );

				case System.Net.HttpStatusCode.Unauthorized:
					//No authorization. A valid API key was not provided with the request, so the API could not associate a user with the request.
					throw new UnauthorizedAccessException( "No authorization. A valid API key was not provided with the request, so the API could not associate a user with the request" );

				case System.Net.HttpStatusCode.Forbidden:
					throw new UnauthorizedAccessException( "Access denied. The API key was valid but the user does not have the access required to complete the request. This can happen if you try to read or write to objects that the user does not have access to." );

				case System.Net.HttpStatusCode.NotFound:
					throw new Exception( "Not found. Either the request method and path supplied do not specify a known action in the API, or the object specified by the request does not exist." );

				case System.Net.HttpStatusCode.InternalServerError:
					throw new Exception( "Server error. There was a problem on Asana's end." );
			}

			throw new Exception( "Unknown client error." );
		}

		internal dynamic GetContentAsDynamicObject( bool includeDataWrapper )
		{
			string data = includeDataWrapper ? Content : ( Content.Substring( 8, Content.Length - 9 ) );

			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters( new[] { new DynamicJsonConverter() } );

			return serializer.Deserialize( data, typeof( object ) );
		}
	}
}
