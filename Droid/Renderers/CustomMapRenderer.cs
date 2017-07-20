using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using examenPrutech;
using examenPrutech.Droid;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace examenPrutech.Droid
{
	/// <summary>
	/// Custom render para customizar los markers
	/// </summary>
	public class CustomMapRenderer : MapRenderer
	{
        CustomMap formsMap;
        bool isDrawn;

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				formsMap = (CustomMap)e.NewElement;
				((MapView)Control).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			NativeMap = googleMap;
			NativeMap.UiSettings.ZoomControlsEnabled = Map.HasZoomEnabled;
			NativeMap.UiSettings.ZoomGesturesEnabled = Map.HasZoomEnabled;
			NativeMap.UiSettings.ScrollGesturesEnabled = Map.HasScrollEnabled;
            DibujaPines();
		}

		/// <summary>
		/// Se 
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
                DibujaPines();
		}

        private void DibujaPines(){
            if (!isDrawn)
            {
                NativeMap.Clear();
                foreach (var pin in formsMap.Pins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
                    marker.SetTitle(pin.Label);
                    marker.SetSnippet(pin.Address);
                    if (pin.Label == "Ud esta aqui")
                        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pinaqui));
                    else
						marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ecobici));
                    NativeMap.AddMarker(marker);
                }
            }
			isDrawn = true;
        }

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);
			if (changed)
			{
				isDrawn = false;
			}
		}

	}
}