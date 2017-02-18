using Bing.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DartaGramTablet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Geolocator geo = null;
        GPSIcon gpspushpin;

        public MainPage()
        {


            this.InitializeComponent();


            gpspushpin = new GPSIcon(myMap)
            {
                Visibility = Windows.UI.Xaml.Visibility.Collapsed
            };

            myMap.Children.Add(gpspushpin);

            //InitializeMap();
            geomap();
            DartaGramPin pin = null;
            int count = 3;
            //Set the  location of the pin to the center of the map. 
            //Bing.Maps.MapLayer.SetPosition(pin, myMap.Center);
            foreach(Location location in getLocationList())
            {             
                pin = new DartaGramPin(count);
                MapLayer.SetPosition(pin, location);
                myMap.Children.Add(pin);
                count++;
            }
        }

        public List<Location> getLocationList()
        {
            List<Location> locationsList = new List<Location>();
            locationsList.Add(new Location() { Latitude = 41.26706, Longitude = -96.19755 });
            locationsList.Add(new Location() { Latitude = 41.26811, Longitude = -96.19490 });
            locationsList.Add(new Location() { Latitude = 41.26557, Longitude = -96.19697 });
            return locationsList;
        }

        void geomap()
        {
            if (geo == null)
            {
                //Create an instance of the GeoLocator class.
                geo = new Geolocator();
            }

            //Add the position changed event
            geo.PositionChanged += geolocator_PositionChanged;


        }
        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            // Need to get back onto UI thread before updating location information
            this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(
            () =>
            {
                //Get the current location
                Location location = new Location(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude);

                //Update the position of the GPS pushpin
                MapLayer.SetPosition(gpspushpin, location);

                //Set the radius of the Accuracy Circle
                gpspushpin.SetRadius(args.Position.Coordinate.Accuracy);

                //Make GPS pushpin visible
                gpspushpin.Visibility = Windows.UI.Xaml.Visibility.Visible;

                //Update the map view to the current GPS location
                myMap.SetView(location, 17);
            }));
        }
    }
}
