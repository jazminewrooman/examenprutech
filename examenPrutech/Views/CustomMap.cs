using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;

namespace examenPrutech
{
	public class CustomMap : Map
	{
		public static readonly BindableProperty PinPosicionProperty = BindableProperty.Create(nameof(PinPosicion), typeof(Pin), typeof(CustomMap), propertyChanged: CambiaPos);
		public Pin PinPosicion
		{
            get { return (Pin)GetValue(PinPosicionProperty); }
			set { 
                SetValue(PinPosicionProperty, value);
			}
		}

        public static readonly BindableProperty PinEstacionProperty = BindableProperty.Create(nameof(PinEstacion), typeof(Pin), typeof(CustomMap), propertyChanged: CambiaEst);
		public Pin PinEstacion
		{
			get { return (Pin)GetValue(PinEstacionProperty); }
			set
			{
                SetValue(PinEstacionProperty, value);
			}
		}

        static void CambiaPos(BindableObject bindable, object oldValue, object newValue)
		{
            (bindable as CustomMap).CentraMapa(newValue as Pin);
            (bindable as CustomMap).Pins.Add(newValue as Pin);
		}

		static void CambiaEst(BindableObject bindable, object oldValue, object newValue)
		{
			(bindable as CustomMap).Pins.Add(newValue as Pin);
		}

		public void CentraMapa(Pin p)
		{
            this.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(p.Position.Latitude, p.Position.Longitude), Distance.FromKilometers(2.0)));
		}


	}
}