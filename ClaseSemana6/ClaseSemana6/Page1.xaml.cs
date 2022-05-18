using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClaseSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo",txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);

                cliente.UploadValues("http://192.168.16.33/moviles/post.php", "POST", parametros);
                DisplayAlert("Mensaje", "Ingreso correcto", "ok");

            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", ex.Message, "ok");
            }
        }

        private async void btnListar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);

                Uri uri = new Uri(string.Format("http://192.168.16.33/moviles/post.php?codigo={0}&nombre={1}&apellido={2}&edad={3}", txtCodigo.Text, txtNombre.Text, txtApellido.Text, txtEdad.Text));

                cliente.UploadValues(uri, "PUT", parametros);
                await DisplayAlert("Mensaje", "Actualización Correcta", "ok");


                //HttpClient client = new HttpClient();
                //Datos datos = new Datos()
                //{
                //    apellido = txtApellido.Text,
                //    nombre = txtNombre.Text,
                //    edad = int.Parse(txtEdad.Text),
                //    codigo = int.Parse(txtEdad.Text)
                //};
                //Uri uri = new Uri(string.Format("http://192.168.16.33/moviles/post.php?codigo={0}&nombre={1}&apellido={2}&edad={3}",txtCodigo.Text,txtNombre.Text,txtApellido.Text,txtEdad.Text));
                //string json = JsonConvert.SerializeObject(datos);
                //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = await client.PutAsync(uri, content);
                //if (response.IsSuccessStatusCode)
                //{
                //    await DisplayAlert("Mensaje", "Actualización correcta", "ok");
                //}
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message, "ok");
            }
        }
    }
}