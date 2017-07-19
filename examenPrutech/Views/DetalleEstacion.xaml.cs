using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace examenPrutech
{
	public partial class DetalleEstacion : ContentPage
	{
		private Geo mposicion;

		public DetalleEstacion()
		{
			InitializeComponent();

			BindingContext = new DetalleEstacionVM(UserDialogs.Instance);
		}
	}
}
