using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Http;

namespace Ejemplo1
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private HttpClient httpclient;
        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:7004";
            httpclient = new HttpClient();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Thread.Sleep(5000);
            
            gif.Visible = true;
            //await Task.Delay(5000);
            await Esperar();
            var nombre = textBox1.Text;
            try
            {
                var saludo = await DevolverSaludo(nombre);
                MessageBox.Show(saludo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            gif.Visible = false;
        }
        private async Task Esperar()
        { 
            await Task.Delay(5000);
        }
        private async Task <string> DevolverSaludo(string nombre)
        {
            using (var respuesta=await httpclient.GetAsync($"{apiURL}/saludos/{nombre}"))
            {
                respuesta.EnsureSuccessStatusCode();
                var saludo = await respuesta.Content.ReadAsStringAsync();
                return saludo;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
