using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace examenPrutech
{
	public class DetalleEstacionVM : EcobiciVM
	{
		private Pin mPinEstacion;
		public Pin PinEstacion
		{
			get { return (mPinEstacion); }
			set
			{
				if (value != mPinEstacion)
				{
					mPinEstacion = value;
					OnPropertyChanged("PinEstacion");
				}
			}
		}

		private Station mEstacion;
		public Station Estacion
		{
			get { return (mEstacion); }
			set
			{
				if (value != mEstacion)
				{
					mEstacion = value;
					Pin pin = new Pin
					{
						Type = PinType.Place,
						Position = new Position(value.location.lat, value.location.lon),
						Label = value.name,
					};
					PinEstacion = pin;
					OnPropertyChanged("Estacion");
				}
			}
		}

		private Pin mPinPosicion;
		public Pin PinPosicion
		{
			get { return (mPinPosicion); }
			set
			{
				if (value != mPinPosicion)
				{
					mPinPosicion = value;
					OnPropertyChanged("PinPosicion");
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
					Title = "Detalle";
					Pin pin = new Pin
					{
						Type = PinType.Place,
						Position = new Position(value.latitude, value.longitud),
						Label = "Ud esta aqui",
					};
					PinPosicion = pin;
					OnPropertyChanged("Posicion");
				}
			}
		}

		public DetalleEstacionVM(IUserDialogs diag) : base(diag)
		{

		}
	}
}

