using System;
using MvvmHelpers;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace examenPrutech
{
	public class EcobiciVM : BaseViewModel
	{
		private IUserDialogs Diag;

		private bool mocupado;
		public bool Ocupado
		{
			get { return mocupado; }
			set
			{
				if (mocupado != value)
				{
					if (value)
						Diag.ShowLoading("Cargando...");
					else
						Diag.HideLoading();
					mocupado = value;
					OnPropertyChanged("Ocupado");
				}
			}
		}

		public EcobiciVM(IUserDialogs diag)
		{
			Diag = diag;
		}
	}
}
