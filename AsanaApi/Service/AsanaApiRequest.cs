using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;

namespace AsanaApi.Service
{
	public abstract class AsanaApiRequest
	{
		const string URL_BASE = @"https://app.asana.com/api/1.0";
		protected readonly string _apiKey;
		readonly string _basicAuthenticationString;

		public AsanaApiRequest( string ApiKey )
		{
			this._apiKey = ApiKey;
			this._basicAuthenticationString = Convert.ToBase64String( new UTF8Encoding().GetBytes( _apiKey + ":" ) );
		}

		HttpWebRequest CreateClient( string urlPath )
		{
			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create( URL_BASE + urlPath );
			httpRequest.Method = "GET";
			httpRequest.AllowAutoRedirect = false;
			httpRequest.UseDefaultCredentials = false;
			httpRequest.UserAgent = ".NET AsanaApi";
			httpRequest.Headers[ "Authorization" ] = "Basic " + _basicAuthenticationString;
			httpRequest.Accept = "application/json, text/json, text/x-json, text/javascript";
			httpRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			httpRequest.KeepAlive = false;
			return httpRequest;
		}

		protected dynamic ExecuteRequest( string urlPath )
		{
			return ExecuteRequest( urlPath, null, null );
		}
		protected dynamic ExecuteRequest( string urlPath, AsanaApi.Models.OptionalFields[] returnedFields, string fieldNamesToAppendToOptionalFieldNames )
		{
			HttpWebRequest httpRequest = CreateClient( AppendOptionalFields( urlPath, returnedFields, fieldNamesToAppendToOptionalFieldNames ) );
			AsanaApiResponse response = new AsanaApiResponse().Execute( httpRequest ).CheckForError();

			return response.GetContentAsDynamicObject( false );
		}

		private string AppendOptionalFields( string urlPath, Models.OptionalFields[] returnedFields, string fieldNamesToAppendToOptionalFieldNames )
		{
			if( returnedFields == null )
			{
				return urlPath;
			}

			string fieldNames = AsanaApi.Models.OptionFieldsHelper.ToOptionalFieldNames( returnedFields );
			if( string.IsNullOrEmpty( fieldNames ) )
			{
				return urlPath;
			}
			if( string.IsNullOrEmpty( fieldNamesToAppendToOptionalFieldNames ) == false )
			{
				fieldNames += fieldNamesToAppendToOptionalFieldNames;
			}
			
			if( urlPath.Contains( "?" ) == false )
			{
				urlPath += "?";
			}
			else
			{
				urlPath += "&";
			}

			return ( urlPath + "opt_fields=" + fieldNames );
		}
	}
}
