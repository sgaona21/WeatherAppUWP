﻿using System;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var position = await LocationManager.GetPosition();

            Root myWeather =
                await OpenWeatherMapProxy.GetWeather(
                    position.Coordinate.Latitude,
                    position.Coordinate.Longitude, "imperial");

            Root myWeatherCelsius =
                await OpenWeatherMapProxy.GetWeather(
                    position.Coordinate.Latitude,
                    position.Coordinate.Longitude, "metric");

            string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            ResultTextBlock.Text = "Fahrenheit: " + myWeather.name + " - " + ((int)myWeather.main.temp).ToString() + " - " + myWeather.weather[0].description;
            weatherCelsius.Text = "Celsius: " + myWeatherCelsius.name + " - " + ((int)myWeatherCelsius.main.temp).ToString() + " - " + myWeatherCelsius.weather[0].description;
            Coordinates.Text = "Your Coordinates: " + myWeather.coord.lat + " Lattitude, " + myWeather.coord.lon + " Longitude";
        }
    }
}
