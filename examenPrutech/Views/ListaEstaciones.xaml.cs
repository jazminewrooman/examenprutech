using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace examenPrutech
{
	public partial class ListaEstaciones : ContentPage
	{
		public ListaEstaciones()
		{
			InitializeComponent();
			this.BindingContext = new ListaEstacionesVM(UserDialogs.Instance, Navigation);


			/*lstSucursales.ItemSelected += (s, e) =>

			{
	lstSucursales.SelectedItem = nu    };
		}*/
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await (BindingContext as ListaEstacionesVM).IniciaGPS();
			await(BindingContext as ListaEstacionesVM).CargaEstaciones();
		}
	}
}
