using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    public abstract class Fruta
    {
        protected string _color;
        protected double _peso;

        /// <summary>
        /// propiedad abstracta publica de lectura y escritura para _color
        /// </summary>
        public abstract string Color
        {
            get;
            set;
        }
        /// <summary>
        /// propiedad abstracta publica de lectura y escritura para _peso
        /// </summary>
        public abstract double Peso
        {
            get;
            set;
        }

        public abstract bool TieneCarozo
        {
            get;
        }

        /// <summary>
        /// constructor publico vacío
        /// </summary>
        public Fruta()
        {

        }

        /// <summary>
        /// Constructor que inicializa atributos
        /// </summary>
        protected Fruta(string color, double peso)
        {
            Color = color;
            Peso = peso;
        }

        /// <summary>
        /// metodo que retorna información
        /// </summary>
        /// <returns>string con informacion del objeto</returns>
        protected virtual string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Color: {Color}");
            sb.AppendLine($"Peso: {Peso}");

            return sb.ToString();
        }
    }
}
