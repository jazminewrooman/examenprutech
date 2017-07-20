using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace examenPrutech
{
	public interface ISmartBike
	{
		[Get("/token?client_id={clientid}&client_secret={clientsecret}&grant_type=refresh_token&refresh_token={refreshtoken}")]
		Task<tokens> UpdTokens(string clientid, string clientsecret, string refreshtoken);	

		[Get("/token?client_id={clientid}&client_secret={clientsecret}&grant_type=client_credentials")]
		Task<tokens> GetTokens(string clientid, string clientsecret);

		[Get("/stations.json?access_token={token}")]
		Task<StationList> GetEstaciones(string token);	
	}
}
