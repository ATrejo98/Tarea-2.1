using Ejercicio2_1.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Ejercicio2_1.Views
{
    public partial class VerMapa : ContentPage
    {
        Countries PaisSeleccionado;

        public VerMapa(Countries country)
        {
            PaisSeleccionado = country;
            InitializeComponent();
            loadConfiguration(); 
        }

        private void loadConfiguration()
        {
            // Limpiar marcadores existentes
            mapa.Pins.Clear();

            if (PaisSeleccionado != null && PaisSeleccionado.latlng != null && PaisSeleccionado.latlng.Count >= 2)
            {
                var pin = new Pin()
                {
                    Type = PinType.SavedPin,
                    Location = new Location(PaisSeleccionado.latlng[0], PaisSeleccionado.latlng[1]),
                    Label = PaisSeleccionado.NameCountry.official,
                    Address = "Capital: " + PaisSeleccionado.capital
                };

                mapa.Pins.Add(pin);
                mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(PaisSeleccionado.latlng[0], PaisSeleccionado.latlng[1]), Distance.FromKilometers(41)));
            }
            else
            {
                Console.WriteLine("Datos de ubicación no válidos.");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadConfiguration();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mapa.Pins.Clear();
        }
    }
}
