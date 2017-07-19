using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace examenPrutech
{
	public interface ISmartBike
	{
		[Get("/stations.json?access_token={token}")]
		Task<StationList> GetEstaciones(string token);	
	}
}
