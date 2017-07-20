using System;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Refit;
using System.Linq;
using Plugin.Connectivity;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace examenPrutech
{
	public class ListaEstacionesVM : EcobiciVM
	{
		private const string apiendpoint = "https://pubsbapi.smartbike.com/api/v1";
		private const string tokenendpoint = "https://pubsbapi.smartbike.com/oauth/v2";
		private const string clientid = "1058_4rtx3vb3bmgwcks04w8gs80wg0c04kck84gco0s8ccg4kss888";
		private const string clientsecret = "3s1rl2rinc6cccgs84wwwoks00kcowosswkks4kogksco0ggoc";
		public const double EarthRadius = 6371;

		private INavigation navigation;
		private static ISettings AppSettings => CrossSettings.Current;

		public static string RefreshToken
		{
			get { return(AppSettings.GetValueOrDefault(nameof(RefreshToken), string.Empty)); }
			set { AppSettings.AddOrUpdateValue(nameof(RefreshToken), value); }
		}

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
					navigation.PushAsync(new DetalleEstacion(this));
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
			tokens tks;
			Ocupado = true;
			if (CrossConnectivity.Current.IsConnected)
			{
				var api = RestService.For<ISmartBike>(tokenendpoint);
				if (String.IsNullOrEmpty(RefreshToken))
				{
					tks = await api.GetTokens(clientid, clientsecret);
					RefreshToken = tks.refresh_token;
				}
				else
				{
					tks = await api.UpdTokens(clientid, clientsecret, RefreshToken);
					RefreshToken = tks.refresh_token;
				}
				api = RestService.For<ISmartBike>(apiendpoint);
				var lsttmp = await api.GetEstaciones(tks.access_token);
				foreach (Station s in lsttmp.stations)
					s.distanciadelpunto = GetDistance(mPosicion, new Geo { latitude = s.location.lat, longitud = s.location.lon });
				lsttmp.stations = lsttmp.stations.OrderBy(x => x.distanciadelpunto).Take(10).ToList();
				ListaEstaciones = lsttmp;
			}
			else
				await UserDialogs.Instance.AlertAsync("No hay conexión a internet, revise su configuración WiFi o 3G", "Aviso", "ok");
			Ocupado = false;
		}

		public static double GetDistance(Geo p1, Geo p2)
		{
			double distance = 0;
			try
			{
				double Lat = (p2.latitude - p1.latitude) * (Math.PI / 180);
				double Lon = (p2.longitud - p1.longitud) * (Math.PI / 180);
				double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(p1.latitude * (Math.PI / 180)) * Math.Cos(p2.latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
				double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
				distance = EarthRadius * c;
			}
			catch
			{
			}
			return distance*1000;
		}
	}
}
