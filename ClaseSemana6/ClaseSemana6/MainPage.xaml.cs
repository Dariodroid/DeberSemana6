using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClaseSemana6
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.16.33/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ClaseSemana6.Datos> _post;
        private string _codigo;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(Url);
            List<ClaseSemana6.Datos> posts = JsonConvert.DeserializeObject<List<ClaseSemana6.Datos>>(content);
            _post = new ObservableCollection<Datos>(posts);

            MyListView.ItemsSource = _post;
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemSelected = e.SelectedItem as ClaseSemana6.Datos;  
            if(itemSelected != null)
            {
                _codigo = itemSelected.codigo.ToString();
            }
        }

        private async void EliminarPersona()
        {
            if(_codigo == null)
            {
                await DisplayAlert("Mensaje", "Seleccione un registro para eliminar", "ok");
            }
            else
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(string.Format("http://192.168.16.33/moviles/post.php/?codigo={0}", _codigo));
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Mensaje", "Eliminacion correcta", "ok");
                }
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            EliminarPersona();
        }
    }
}
