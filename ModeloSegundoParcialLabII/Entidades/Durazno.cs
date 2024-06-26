using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Durazno : Fruta
    {
        protected int _cantPelusa;

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
                return true;
            }
        }

        /// <summary>
        /// propiedad publica de lectura, devuelve el nombre de la fruta
        /// </summary>
        public string Nombre
        {
            get
            {
                return "Durazno";
            }
        }

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public Durazno()
        {

        }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        public Durazno(string color, double peso, int cantidadPelusa)
            : base(color, peso)
        {
            _cantPelusa = cantidadPelusa;
        }

        /// <summary>
        /// metodo protegido, retorna informacion
        /// </summary>
        /// <returns> cadena de texto con la informacion de la fruta</returns>
        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {Nombre}");
            sb.AppendLine($"Cantidad pelusa: {_cantPelusa}");
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
