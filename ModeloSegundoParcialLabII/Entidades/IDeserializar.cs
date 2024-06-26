using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Entidades
{
    public interface IDeserializar
    {
        bool Xml(string path, out Fruta fruta);

    }
}
