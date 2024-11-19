using System;
using System.Windows;

namespace Lab6
{

    [Flags]
    internal enum Precipitation : byte
    {

        Sunny = 1,

        Cloudy = 2,

        Rain = 4,

        Snow = 8

    }

    internal class WeatherControl : DependencyObject
    {

        private string __wind;

        private int __wind_speed;

        private Precipitation __precipitation;
        private static object CoerceTemperature(DependencyObject d, object base_value)
        {
            return Math.Max(Math.Min((int)base_value, 50), -50);
        }

        private static bool ValidateTemperature(object value)
        {
            int temp = (int)value;
            return (temp >= (-50)) && (temp <= 50);
        }

        public static readonly DependencyProperty TemperatureProperty;

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                "Temperature",
                typeof(int),
                typeof(WeatherControl),
                new PropertyMetadata(
                    0,
                    null,
                    CoerceTemperature
                ),
                ValidateTemperature
            );
        }

        public WeatherControl(int temperature = 20, string wind = "s", int wind_speed = 1, Precipitation precipitation = Precipitation.Cloudy | Precipitation.Rain)
        {
            this.Temperature = temperature;
            this.Wind = wind;
            this.WindSpeed = wind_speed;
            this.Precipitation = precipitation;
        }

        public int Temperature
        {
            get
            {
                return (int)this.GetValue(TemperatureProperty);
            }
            set
            {
                this.SetValue(TemperatureProperty, value);
            }
        }

        public string Wind
        {
            get
            {
                return this.__wind;
            }
            set
            {
                this.__wind = value;
            }
        }

        public int WindSpeed
        {
            get
            {
                return this.__wind_speed;
            }
            set
            {
                if((value < 0) || (value > 100))
                {
                    throw new ArgumentException("Wind speed value is incorrect", "WindSpeed");
                }
                this.__wind_speed = value;
            }
        }

        public Precipitation Precipitation
        {
            get
            {
                return this.__precipitation;
            }
            set
            {
                this.__precipitation = value;
            }
        }

    }
}
