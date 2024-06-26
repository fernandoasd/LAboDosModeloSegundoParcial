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
        public void ImprimirArchivoTexto(object obj)
        {
            DateTime fechaActual = DateTime.Now;

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //string nombreCarpeta = "SerializacionPunto5";
            //string pathDirectorio = Path.Combine(pathDesktop, nombreCarpeta);
            //Directory.CreateDirectory(pathDirectorio);
            string nombreArchivo = "Serializacion.txt";
            string path = Path.Combine(pathDesktop, nombreArchivo);

            try
            {
                //El using abre y cierra solo el sw (el segundo parametro true -> agregar nuevas lineas)
                //                                  (el segundo parametro false -> sobreescribe archivo)
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    //streamWriter.WriteLine($"{fechaActual.Hour}:{fechaActual.Minute}:{fechaActual.Second}");
                    streamWriter.WriteLine("fecha {0:yy/mm/dd h:mm:ss}", fechaActual);

                    streamWriter.WriteLine($"Precio total del cajón: ${obj:#,###.00}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
