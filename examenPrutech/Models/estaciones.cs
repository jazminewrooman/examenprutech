using System;
using System.Collections.Generic;

namespace examenPrutech
{
	public class Location
	{
		public double lat { get; set; }
		public double lon { get; set; }
	}

	public class Station
	{
		public int id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string addressNumber { get; set; }
		public string zipCode { get; set; }
		public string districtCode { get; set; }
		public string districtName { get; set; }
		public object altitude { get; set; }
		public IList<int> nearbyStations { get; set; }
		public Location location { get; set; }
		public string stationType { get; set; }
		public double distanciadelpunto { get; set; }
	}

	public class StationList
	{
		public IList<Station> stations { get; set; }
	}

	public class tokens
	{
		public string access_token { get; set; }
		public int expires_in { get; set; }
		public string token_type { get; set; }
		public object scope { get; set; }
		public string refresh_token { get; set; } 
	}
}
