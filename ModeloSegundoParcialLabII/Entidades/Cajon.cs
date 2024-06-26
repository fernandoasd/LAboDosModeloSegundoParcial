using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Entidades
{
    public delegate void PrecioExtendido(object obj);
    public class Cajon<T> : ISerializar
    {
        protected int _capacidad;
        protected List<T> _elementos;
        protected double _precioUnitario;

        public event PrecioExtendido eventoPrecio;

        protected static SqlConnection conexion;
        protected static SqlCommand sqlCom;

        /// <summary>
        /// propiedad publica de lectura, devuelve la lista _elementos
        /// </summary>
        public List<T> Elementos
        {
            get
            {
                return _elementos;
            }
        }

        /// <summary>
        /// propiedad publica de lectura y escritura, devuelve el precio total de los productos
        /// </summary>
        public double PrecioTotal
        {
            get
            {
                double precioTotal = Elementos.Count * _precioUnitario;
                if (precioTotal > 55)
                {
                    if (!(eventoPrecio is null))
                    {
                        eventoPrecio.Invoke(precioTotal);
                    }
                }
                return precioTotal;
            }
        }

        /// <summary>
        /// constructor publico que inicializa la lista
        /// </summary>
        public Cajon()
        {
            _elementos = new List<T>();
        }


        /// <summary>
        /// constructor publico con parametros
        /// </summary>
        /// <param name="precio"></param>
        /// <param name="capacidad"></param>
        public Cajon(double precio, int capacidad)
            : this(capacidad)
        {
            _precioUnitario = precio;
            _capacidad = capacidad;
        }

        /// <summary>
        /// constructor publico con parametros
        /// </summary>
        /// <param name="capacidad"></param>
        public Cajon(int capacidad)
            : this()
        {
            _capacidad = capacidad;
        }

        /// <summary>
        /// metodo publico que devuelve informacion
        /// </summary>
        /// <returns>cadena de informacion</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Capacidad: {_capacidad}");
                sb.AppendLine($"Cantidad de elementos: {Elementos.Count}");
                sb.AppendLine($"Precio toal: {PrecioTotal}");
                sb.AppendLine($"Lista:");


                foreach (object item in Elementos)
                {
                    try
                    {
                        sb.AppendLine(item.ToString());
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return sb.ToString();
            
        }

        /// <summary>
        /// serializa el objeto en el path como parametro
        /// </summary>
        /// <param name="path"></param>
        /// <returns> true si serializo correctamente, false caso contrario</returns>
        public bool Xml(string path)
        {
            try
            {
                //Para serializar XML necesita metodos publicos
                //Se crea un xmlTextWriter para escribir en el archivo XML
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    //Se indica el tipo de objeto a serializar:
                    XmlSerializer serializer = new XmlSerializer(typeof(Cajon<T>));

                    //Se serializa el objeto this (Manzana) en el archivo contenido en el Writer
                    serializer.Serialize(writer, this);
                }
                //Console.WriteLine("Se ha serializado correctamente en el path: {0}", path);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// operador publicod e adicion de elementos a la lista
        /// </summary>
        /// <param name="cajon"></param>
        /// <param name="elem"></param>
        /// <returns>retorna cajon actualizado</returns>
        public static Cajon<T> operator +(Cajon<T> cajon, T elem)
        {
            try
            {
                if (cajon.Elementos.Count < cajon._capacidad)
                {
                    cajon._elementos.Add(elem);
                    return cajon;
                }
                else
                {
                    throw new CajonLlenoException("El cajón ya se encuentra lleno");
                }
            }
            catch (CajonLlenoException cajonLLenoExc)
            {
                throw cajonLLenoExc;
            }
        }

        //Esto funciona, pero tiene que estar en el formulario!!!!!!!!!
        //public bool AgregarFrutas()
        //{
        //    Random rand = new Random();
        //    string nombre = "";
        //    double peso = 0;
        //    try
        //    {

        //        foreach (T fruta in this.Elementos)
        //        {
        //            if (fruta is Manzana manzana)
        //            {
        //                nombre = manzana.Nombre;
        //                peso = manzana.Peso;
        //            }
        //            else if (fruta is Banana banana)
        //            {
        //                nombre = banana.Nombre;
        //                peso = banana.Peso;
        //            }
        //            else if (fruta is Durazno durazno)
        //            {
        //                nombre = durazno.Nombre;
        //                peso = durazno.Peso;
        //            }
        //            sqlCom.Parameters.Clear();
        //            conexion.Open();
        //            sqlCom.CommandText = $"INSERT INTO frutas VALUES (@nombre, @peso, @precio)";
        //            sqlCom.Parameters.AddWithValue("@nombre", nombre);
        //            sqlCom.Parameters.AddWithValue("@peso", peso);
        //            sqlCom.Parameters.AddWithValue("@precio", rand.Next(1, 10));

        //            sqlCom.ExecuteNonQuery();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
    }
}
