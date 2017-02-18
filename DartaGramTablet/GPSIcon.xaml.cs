using Bing.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DartaGramTablet
{
    public sealed partial class GPSIcon : UserControl
    {
        private Map _map;
        private double _radius;
        private const double EARTH_RADIUS_METERS = 6378137;

        public GPSIcon(Map map)
        {
            this.InitializeComponent();
            _map = map;

            //Add a View Changed event to the map to update the accuracy circle
            _map.ViewChanged += (s, e) =>
            {
                //Update the accuracy circle
                UpdateAccuracyCircle();
            };
        }


        public void SetRadius(double radiusInMeters)
        {
            //Store the radius value
            _radius = radiusInMeters;

            //Update the accuracy circle
            UpdateAccuracyCircle();
        }
        private void UpdateAccuracyCircle()
        {
            if (_map != null && _radius >= 0)
            {
                //Calculate the ground resolution in meters/pixel
                //Math based on http://msdn.microsoft.com/en-us/library/bb259689.aspx
                double groundResolution = Math.Cos(_map.Center.Latitude * Math.PI / 180) *
                    2 * Math.PI * EARTH_RADIUS_METERS / (256 * Math.Pow(2, _map.ZoomLevel));

                //Calculate the radius of the accuracy circle in pixels
                double pixelRadius = _radius / groundResolution;

                //Update the accuracy circle dimensions
                AccuracyCircle.Width = pixelRadius;
                AccuracyCircle.Height = pixelRadius;

                //Use the margin property to center the accuracy circle
                AccuracyCircle.Margin = new Windows.UI.Xaml.Thickness(-pixelRadius / 2, -pixelRadius / 2, 0, 0);
            }
        }

    }
}
