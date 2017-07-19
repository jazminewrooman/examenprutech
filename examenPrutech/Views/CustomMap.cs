using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;

namespace examenPrutech
{
	public class CustomMap : Map
	{
		public static readonly BindableProperty PosicionProperty = BindableProperty.Create(nameof(Posicion), typeof(Pin), typeof(CustomMap), propertyChanged: CambiaPos);
		public Pin Posicion
		{
            get { return (Pin)GetValue(PosicionProperty); }
			set { 
                SetValue(PosicionProperty, value);
			}
		}

        /*public static readonly BindableProperty SucursalProperty = BindableProperty.Create(nameof(Sucursal), typeof(Pin), typeof(CustomMap), propertyChanged: CambiaSuc);
		public Pin Sucursal
		{
			get { return (Pin)GetValue(SucursalProperty); }
			set
			{
				SetValue(SucursalProperty, value);
			}
		}*/

        static void CambiaPos(BindableObject bindable, object oldValue, object newValue)
		{
            (bindable as CustomMap).CentraMapa(newValue as Pin);
            (bindable as CustomMap).Pins.Add(newValue as Pin);
		}

		/*static void CambiaSuc(BindableObject bindable, object oldValue, object newValue)
		{
			(bindable as CustomMap).Pins.Add(newValue as Pin);
		}*/

		public void CentraMapa(Pin p)
		{
            this.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(p.Position.Latitude, p.Position.Longitude), Distance.FromKilometers(2.0)));
		}


	}
}