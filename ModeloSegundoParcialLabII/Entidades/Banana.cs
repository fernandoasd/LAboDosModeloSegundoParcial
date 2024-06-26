using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Banana : Fruta
    {
        protected string _paisOrigen;


        /// <summary>
        /// propiedad publica de lectura y escritura para _color
        /// </summary>
        public override string Color
        {
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
        /// propiedad publica de lectura, devuelve el nombre de la fruta
        /// </summary>
        public string Nombre
        {
            get
            {
                return "Banana";
            }
        }

        public Banana()
        {
            
        }

        public Banana(string color, double peso, string paisOrigen)
            : base(color, peso)
        {
            _paisOrigen = paisOrigen;
        }

        /// <summary>
        /// metodo protegido, retorna informacion
        /// </summary>
        /// <returns> cadena de texto con la informacion de la fruta</returns>
        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {Nombre}");
            sb.AppendLine($"Pais Origen: {_paisOrigen}");
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
    }
}
