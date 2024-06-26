using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Entidades;
using static System.Net.WebRequestMethods;

namespace SP
{
    [XmlInclude(typeof(Fruta))]

    public partial class ModeloSegundoParcial : Form
    {
        private Manzana _manzana;
        private Banana _banana;
        private Durazno _durazno;

        public Cajon<Manzana> c_manzanas;
        public Cajon<Banana> c_bananas;
        public Cajon<Durazno> c_duraznos;

        protected static SqlConnection conexion;
        protected static SqlCommand sqlCom;

        public ModeloSegundoParcial()
        {
            InitializeComponent();
            conexion = new SqlConnection("Data Source=DESKTOP-B5FPUG7\\SQLEXPRESS;Initial Catalog=ModeloSegundoParcial;Integrated Security=True");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"Nombre Apellido\n\n-DIRECTORIO ACTUAL: \n\n{Environment.CurrentDirectory}", "Directorio Actual");
        }

        //Crear una instancia de cada clase e inicializar los atributos del form _manzana, _banana y _durazno. 
        private void btnPunto1_Click(object sender, EventArgs e)
        {
            this._manzana = new Manzana("verde", 2, "rio negro");
            this._banana = new Banana("amarillo", 5, "ecuador");
            this._durazno = new Durazno("rojo", 2.5, 53);

            MessageBox.Show(this._manzana.ToString());
            MessageBox.Show(this._banana.ToString());
            MessageBox.Show(this._durazno.ToString());
        }

        //Métodos
        //ToString: Mostrará en formato de tipo string, la capacidad, la cantidad total de elementos, el precio total 
        //y el listado de todos los elementos contenidos en el cajón. Reutilizar código.
        //Sobrecarga de operador
        //(+) Será el encargado de agregar elementos al cajón, siempre y cuando no supere la capacidad del mismo.
        private void btnPunto2_Click(object sender, EventArgs e)
        {
            try
            {
                this.c_manzanas = new Cajon<Manzana>(1.58, 3);
                this.c_bananas = new Cajon<Banana>(15.96, 4);
                this.c_duraznos = new Cajon<Durazno>(21.5, 1);

                this.c_manzanas += new Manzana("roja", 1, "neuquen");
                this.c_manzanas += this._manzana;
                this.c_manzanas += new Manzana("amarilla", 3, "san juan");

                this.c_bananas += new Banana("verde", 3, "brasil");
                this.c_bananas += this._banana;

                this.c_duraznos += this._durazno;

                MessageBox.Show(this.c_manzanas.ToString());
                MessageBox.Show(this.c_bananas.ToString());
                MessageBox.Show(this.c_duraznos.ToString());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        //Implementar (implicitamente) ISerializar en Cajon y manzana
        //Implementar (explicitamente) IDeserializar en manzana
        //Los archivos .xml guardarlos en el escritorio
        private void btnPunto3_Click(object sender, EventArgs e)
        {
            // AGREGAR

            string directorioPunto3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "./Punto3");
            Directory.CreateDirectory(directorioPunto3);
            string pathFrutaSerializada = Path.Combine(directorioPunto3, "Manzana_Serializada_XML.xml");

            // Serealizacion implicita de manzana

            try
            {
                if (_manzana.Xml(pathFrutaSerializada))
                {
                    MessageBox.Show("Manzana serializada OK");
                }
                else
                {
                    MessageBox.Show("NO Serializado");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


            // Deserealizacion explicita de manzana

            try
            {
                IDeserializar manzanaExplicita = _manzana;
                if (manzanaExplicita.Xml(pathFrutaSerializada, out Fruta fruta))
                {
                    MessageBox.Show("Manzana deserializada OK");
                    MessageBox.Show(fruta.ToString());
                }
                else
                {
                    MessageBox.Show("NO Deserializado");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            


            // Serealizacion de cajon de manzanas
            try
            {
                string pathCajonSerializada = Path.Combine(directorioPunto3, "Cajon_Manzanas_Serializado_XML.xml");
                if (c_manzanas.Xml(pathCajonSerializada))
                {
                    MessageBox.Show("Cajon de Manzanas serializado OK");
                }
                else
                {
                    MessageBox.Show("NO Serializado");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        //Si se intenta agregar frutas al cajón y se supera la cantidad máxima, se lanzará un CajonLlenoException, 
        //cuyo mensaje explicará lo sucedido.
        private void btnPunto4_Click(object sender, EventArgs e)
        {
            //implementar estructura de manejo de excepciones
            // AGREGAR

            try
            {
                this.c_duraznos += this._durazno;
            }
            catch (CajonLlenoException cajonLLenoExc)
            {
                MessageBox.Show(cajonLLenoExc.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        //Si el precio total del cajon supera los 55 pesos, se disparará el evento EventoPrecio. 
        //Diseñarlo (de acuerdo a las convenciones vistas) en la clase cajon. 
        //Crear el manejador necesario para que se imprima en un archivo de texto: 
        //la fecha (con hora, minutos y segundos) y el total del precio del cajón en un nuevo renglón.


        private void btnPunto5_Click(object sender, EventArgs e)
        {
            //Asociar manejador de eventos y crearlo en la clase Manejadora (de instancia).
            // Llamar a la excepcion correspondiente
            // AGREGAR
            ManejadoraEventos manejadoraEventos = new ManejadoraEventos();

            try
            {
                c_bananas.eventoPrecio += manejadoraEventos.ImprimirArchivoTexto;

                this.c_bananas += new Banana("verde", 2, "argentina");

                this.c_bananas += new Banana("amarilla", 4, "ecuador");

                MessageBox.Show(this.c_bananas.PrecioTotal.ToString());
            }

            catch (CajonLlenoException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        //Obtener de la base de datos (ModeloSegundoParcial) el listado de frutas:
        //frutas { id(autoincremental - numérico) - nombre(cadena) - peso(numérico) - precio(numérico) }. 
        //Invocar al método ObtenerListadoFrutas.
        // Mostrarlo por mensaje de dialogo
        private void btnPunto6_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => MessageBox.Show(ObtenerListadoFrutas())));
        }

        //Agregar en la base de datos las frutas del formulario (_manzana, _banana y _durazno).
        //Invocar al metodo AgregarFrutas():bool
        private void btnPunto7_Click(object sender, EventArgs e)
        {
            List<Fruta> listaFrutas = new List<Fruta>();
            listaFrutas.Add(_banana);
            listaFrutas.Add(_durazno);
            listaFrutas.Add(_manzana);


            // AGREGAR        
            if (AgregarFrutas(this))
            {
                MessageBox.Show("Se agregaron las frutas a la Base de Datos");
            }
            else
            {
                MessageBox.Show("NO se agregaron las frutas a la Base de Datos");
            }
        }

        //Obtener de la base de datos (msp_lab_II) el listado de frutas:
        //frutas { id(autoincremental - numérico) - nombre(cadena) - peso(numérico) - precio(numérico) }. 


        /// <summary>
        /// devuelve un string con la lista de la base de datos
        /// </summary>
        /// <returns></returns>
        private static string ObtenerListadoFrutas()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sqlCom = new SqlCommand();
                sqlCom.Parameters.Clear();
                sqlCom.Connection = conexion;
                sqlCom.CommandType = System.Data.CommandType.Text;
                sqlCom.CommandText = "SELECT * FROM frutas";

                conexion.Open();

                using (SqlDataReader sqlDataReader = sqlCom.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        sb.AppendFormat("{0} ", sqlDataReader["id"].ToString());
                        sb.AppendFormat("{0} ", sqlDataReader["nombre"].ToString());
                        sb.AppendFormat("{0} ", sqlDataReader["peso"].ToString());
                        sb.AppendFormat("{0} ", sqlDataReader["precio"].ToString());
                        sb.AppendLine("\n");
                    }
                }
            }
            catch (SqlException e)
            {
                sb.Clear();
                sb.AppendLine($"Ocurrio un error al acceder a la base de datos: {e.Message}");
                throw e;
            }
            catch (Exception e)
            {
                sb.Clear();
                sb.AppendLine($"Ocurrio un error inesperado: {e.Message}");
            }
            finally
            {
                conexion.Close();
            }
            return sb.ToString();
        }

        //Agregar en la base de datos las frutas del formulario (_manzana, _banana y _durazno).

        /// <summary>
        /// Recibe un formulario y guarda las furutas en la BBDD
        /// </summary>
        /// <param name="esteFormulario"></param>
        /// <returns> true si pudo guardar todo, sino false</returns>
        private static bool AgregarFrutas(ModeloSegundoParcial esteFormulario)
        {
            Random rand = new Random();

            Action<string, double> cargarBD = (nombre, peso) =>
            {
                sqlCom.CommandText = $"INSERT INTO frutas (nombre, peso, precio) VALUES (@nombre, @peso, @precio)";
                sqlCom.Parameters.AddWithValue("@nombre", nombre);
                sqlCom.Parameters.AddWithValue("@peso", peso);
                sqlCom.Parameters.AddWithValue("@precio", (float)rand.Next(1, 100));

                sqlCom.ExecuteNonQuery();

                sqlCom.Parameters.Clear();
            };

            try
            {
                conexion.Open();
                sqlCom.Connection = conexion;

                sqlCom.Parameters.Clear();

                foreach (Banana fruta in esteFormulario.c_bananas.Elementos)
                {

                    cargarBD(fruta.Nombre, fruta.Peso);
                }

                foreach (Manzana fruta in esteFormulario.c_manzanas.Elementos)
                {
                    cargarBD(fruta.Nombre, fruta.Peso);
                }

                foreach (Durazno fruta in esteFormulario.c_duraznos.Elementos)
                {
                    cargarBD(fruta.Nombre, fruta.Peso);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}
