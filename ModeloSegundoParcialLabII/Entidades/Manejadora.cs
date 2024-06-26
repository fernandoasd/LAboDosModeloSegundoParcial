using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ManejadoraEventos
    {
        public void ImprimirArchivoTexto(Object obj)
        {
            DateTime date = new DateTime();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string nombreCarpeta = "SerializacionCajonPunto5";
            string pathDirectorio = Path.Combine(pathDesktop, nombreCarpeta);
            Directory.CreateDirectory(pathDirectorio);
            string nombreArchivo = "Serializacion.txt";
            string path = Path.Combine(pathDirectorio, nombreArchivo);


            try
            {
                //El using abre y cierra solo el sw (el segundo parametro true -> agregar nuevas lineas)
                //                                  (el segundo parametro false -> sobreescribe archivo)
                using (StreamWriter streamWriter = new StreamWriter(path, true))
                {
                    streamWriter.WriteLine("HOlA");
                }
            }
            catch (Exception)
            {
                throw;
            }
            

        }
    }
}
