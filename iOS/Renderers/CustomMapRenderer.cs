using System;
using System.Collections.Generic;
using CoreGraphics;
using CustomRenderer;
using CustomRenderer.iOS;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using examenPrutech;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace CustomRenderer.iOS
{
	/// <summary>
	/// Custom Map render para desplegar los markers con icono personalizado
	/// </summary>
	public class CustomMapRenderer : MapRenderer
	{
		UIView customPinView;

		/// <summary>
		/// Ons the element changed.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
			}
			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
			}
		}

		/// <summary>
		/// Se hace el detalle visual del marker (annotation)
		/// </summary>
		/// <returns>The view for annotation.</returns>
		/// <param name="mapView">Map view.</param>
		/// <param name="annotation">Annotation.</param>
		MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;
			if (annotation is MKUserLocation)
				return null;
			var anno = annotation as MKPointAnnotation;

			if (anno.Title == "Ud esta aqui")
			{
				annotationView = new CustomMKAnnotationView(annotation, "aqui");
				annotationView.Image = UIImage.FromFile("pinaqui.png");
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("pinaqui.png"));
			}
			else
			{
				annotationView = new CustomMKAnnotationView(annotation, "ecobici");
				annotationView.Image = UIImage.FromFile("ecobici.png");
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("ecobici.png"));
			}
			annotationView.CalloutOffset = new CGPoint(0, 0);
			annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
			((CustomMKAnnotationView)annotationView).Id = "pin";
			annotationView.CanShowCallout = true;
			return annotationView;
		}

	}

	public class CustomMKAnnotationView : MKAnnotationView
	{
		public string Id { get; set; }

		public string Url { get; set; }

		public CustomMKAnnotationView(IMKAnnotation annotation, string id)
			: base(annotation, id)
		{
		}
	}
}