using System;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Refit;
using System.Linq;

namespace examenPrutech
{
	public class ListaEstacionesVM : EcobiciVM
	{
		private const string apiendpoint = "https://pubsbapi.smartbike.com/api/v1";
		private const string apitoken = "ZDQyZWY5ZmIzNDZiMDI3YTEzNTc4M2ZmMTBhMmYwYjE2MzMyYTA2YjI5MzIyYmZmZjBhNmJhZTliNTEwNDI0OA";
		public const double EarthRadius = 6371;

		private INavigation navigation;

		private Station mSelectedStation;
		public Station SelectedStation
		{
			get { return (mSelectedStation); }
			set
			{
				if (value != mSelectedStation && value != null)
				{
					mSelectedStation = value;
					OnPropertyChanged("SelectedStation");
					navigation.PushAsync(new DetalleEstacion());
				}
			}
		}

		private Geo mPosicion;
		public Geo Posicion
		{
			get { return (mPosicion); }
			set
			{
				if (value != mPosicion)
				{
					mPosicion = value;
					OnPropertyChanged("Posicion");
				}
			}
		}

		private StationList listaestaciones;
		public StationList ListaEstaciones
		{
			get { return (listaestaciones); }
			set
			{
				if (value != listaestaciones)
				{
					listaestaciones = value;
					OnPropertyChanged("ListaEstaciones");
				}
			}
		}

		public ListaEstacionesVM(IUserDialogs diag, INavigation nav) : base(diag)
		{
			Title = "Estaciones Ecobici";
			navigation = nav;
		}

		public async Task IniciaGPS()
		{
			Ocupado = true;
			var plataforma = DependencyService.Get<IGeo>();
			if (plataforma != null)
			{
				Geo g = new Geo();
				plataforma.LatLonUpd = delegate (Geo gtmp)
				{
					Posicion = gtmp;
				};
				plataforma.GetLatLon();
			}
			Ocupado = false;
		}

		public async Task CargaEstaciones()
		{
			Ocupado = true;
			var api = RestService.For<ISmartBike>(apiendpoint);
			var lsttmp = await api.GetEstaciones(apitoken);
			foreach (Station s in lsttmp.stations)
				s.distanciadelpunto = GetDistance(mPosicion, new Geo { latitude = s.location.lat, longitud = s.location.lon });
			lsttmp.stations = lsttmp.stations.OrderBy(x => x.distanciadelpunto).Take(10).ToList();
			ListaEstaciones = lsttmp;
			Ocupado = false;
		}

		public static double GetDistance(Geo p1, Geo p2)
		{
			double distance = 0;
			double Lat = (p2.latitude - p1.latitude) * (Math.PI / 180);
			double Lon = (p2.longitud - p1.longitud) * (Math.PI / 180);
			double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(p1.latitude * (Math.PI / 180)) * Math.Cos(p2.latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			distance = EarthRadius * c;
			return distance*1000;
		}
	}
}
