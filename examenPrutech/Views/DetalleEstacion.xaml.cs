using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace examenPrutech
{
	public partial class DetalleEstacion : ContentPage
	{
		private Geo mposicion;

		public DetalleEstacion(ListaEstacionesVM lstvm)
		{
			InitializeComponent();

			DetalleEstacionVM vm = new DetalleEstacionVM(UserDialogs.Instance);
			vm.Posicion = lstvm.Posicion;
			vm.Estacion = lstvm.SelectedStation;
			BindingContext = vm;
		}
	}
}
