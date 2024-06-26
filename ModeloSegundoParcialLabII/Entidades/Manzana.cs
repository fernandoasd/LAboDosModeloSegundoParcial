using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Entidades
{
    [XmlInclude(typeof(Fruta))]

    public class Manzana : Fruta, ISerializar, IDeserializar
    {
        protected string _provinciaOrigen;

        /// <summary>
        /// propiedad publica de lectura, devuelve el nombre de la fruta
        /// </summary>
        public string Nombre
        {
            get
            {
                return "Manzana";
            }
        }

        /// <summary>
        /// propiedad publica de lectura y escritura para _precio
        /// </summary>
        public string ProvinciaOrigen
        {
            get
            {
                return _provinciaOrigen;
            }
            set
            {
                _provinciaOrigen = value;
            }
        }


        /// <summary>
        /// Retorna booleado si tiene carozo
        /// </summary>
        public override bool TieneCarozo
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// propiedad publica de lectura y escritura para _color
        /// </summary>
        public override string Color {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// propiedad publica de lectura y escritura para _peso
        /// </summary>
        public override double Peso
        {
            get
            {
                return _peso;
            }
            set
            {
                _peso = value;
            }

        }

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public Manzana()
        {

        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Manzana(string color, double peso, string provinciaOrigen)
            : base(color, peso)
        {
            ProvinciaOrigen = provinciaOrigen;
        }

        /// <summary>
        /// metodo protegido, retorna informacion
        /// </summary>
        /// <returns> cadena de texto con la informacion de la fruta</returns>
        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {Nombre}");
            sb.AppendLine($"Provincia Origen: {ProvinciaOrigen}");
            sb.AppendFormat("Tiene carozo: {0}\n", TieneCarozo ? "SI" : "NO");

            sb.AppendLine(base.FrutasToString());


            return sb.ToString();
        }

        /// <summary>
        /// metodo publico, totorna informacion
        /// </summary>
        /// <returns> cadena de texto con la informacion de la fruta</returns>
        public override string ToString()
        {
            return this.FrutasToString();
        }

        /// <summary>
        /// retorna bool si se deserializó correctamente
        /// </summary>
        /// <param name="path"> ubicacion del archivo a deserializar</param>
        /// <param name="fruta"> fruta deserializada</param>
        /// <returns></returns>
        bool IDeserializar.Xml(string path, out Fruta fruta)
        {
            fruta = null;
            try
            {

                //Desesealización:
                XmlTextReader reader = new XmlTextReader(path);

                //Hace falta un constructor vacío para instanciar la serializacion:
                XmlSerializer ser = new XmlSerializer(typeof(Manzana));

                fruta = (Manzana)ser.Deserialize(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                
            }
            

            if (!(fruta is null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// serializa la manzana es la ubicacion de parametro
        /// </summary>
        /// <param name="path"></param>
        /// <returns> ubicacion a serializar</returns>
        public bool Xml(string path)
        {
            try
            {
                //Para serializar XML necesita metodos publicos
                //Se crea un xmlTextWriter para escribir en el archivo XML
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    //Se indica el tipo de objeto a serializar:
                    XmlSerializer serializer = new XmlSerializer(typeof(Manzana));

                    //Se serializa el objeto this (Manzana) en el archivo contenido en el Writer
                    serializer.Serialize(writer, this);
                }
                //Console.WriteLine("Se ha serializado correctamente en el path: {0}", path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
