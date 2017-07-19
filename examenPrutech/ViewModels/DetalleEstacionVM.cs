using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace examenPrutech
{
	public class DetalleEstacionVM : EcobiciVM
	{
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
					/*Pin pin = new Pin
					{
						Type = PinType.Place,
						Position = new Position(double.Parse(value.LAT), double.Parse(value.LON)),
						Label = value.SUC_NAME,
					};
					Sucursal = pin;
					mSucursalSel = value;*/

					OnPropertyChanged("SucursalSel");
					OnPropertyChanged("Posicion");
				}
			}
		}

		public DetalleEstacionVM(IUserDialogs diag) : base(diag)
		{

		}
	}
}

