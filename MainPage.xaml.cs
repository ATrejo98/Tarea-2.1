using Ejercicio2_1.Controller;
using Ejercicio2_1.Models;
using Ejercicio2_1.Views;

namespace Ejercicio2_1
{
    public partial class MainPage : ContentPage
    {
        RestApi PaisesRestapi;
        List<Countries> ListaPaises;

        public MainPage()
        {
            InitializeComponent();
            PaisesRestapi = new RestApi();
            ListaPaises = new List<Countries>();

        }



        private async void ComboRegiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaisesRestapi = new RestApi();
            ListaPaises = new List<Countries>();
    
            if (ComboRegiones == null || ComboRegiones.SelectedItem == null)
                return;

            var region = ComboRegiones.SelectedItem.ToString();

            var internetAccess = Connectivity.NetworkAccess;
            if (internetAccess == NetworkAccess.Internet)
            {
                try
                {
                    if (ListaPaisesRest != null && PaisesRestapi != null)
                    {
                        ListaPaisesRest.ItemsSource = null;

                        ListaPaises = await PaisesRestapi.GetCountries(region);
                        ListaPaisesRest.ItemsSource = ListaPaises;
                    }
                    else
                    {
                        Console.WriteLine("ListaPaisesRest o PaisesRestapi es nulo.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar países: {ex.Message}");
                }
            }
            else
            {
                await DisplayAlert("Error", "La Aplicacion no tiene acceso a Internet! Revise su acceso", "OK");
            }
        }


        private async void ListaPaisesRest_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var country = (Countries)e.Item;
            VerMapa pageDetailCountry = new VerMapa(country);
            pageDetailCountry.BindingContext = country;
            await Navigation.PushAsync(pageDetailCountry);

            // Después de navegar, deselecciona el elemento
            if (ListaPaisesRest != null)
            {
                ListaPaisesRest.SelectedItem = null;
            }
        }
    }
}