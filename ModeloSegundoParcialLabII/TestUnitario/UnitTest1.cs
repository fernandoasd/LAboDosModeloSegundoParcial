using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Entidades;

namespace TestUnitario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PrecioTotal_DebeDispararEventoPrecio_CuandoElTotalExcede55()
        {
            // Arrange
            Cajon<Manzana> cajon = new Cajon<Manzana>(10, 10);
            bool eventoDisparado = false;
            double precioTotal = 0;

            cajon.eventoPrecio += (total) =>
            {
                eventoDisparado = true;
                //precioTotal = total;
            };

            // Act
            cajon += new Manzana("roja", 1.2, "neuquen");
            cajon += new Manzana("verde", 1.3, "rio negro");
            cajon += new Manzana("amarilla", 1.1, "san juan");
            cajon += new Manzana("roja", 1.4, "mendoza");
            cajon += new Manzana("verde", 1.5, "salta");
            cajon += new Manzana("amarilla", 1.0, "jujuy");

            // Assert
            Assert.IsTrue(eventoDisparado);
            Assert.AreEqual(60, precioTotal, 0.01);
        }

        // Generar prueba que debe lanzar la excepcion cuando el cajon este lleno
        // TestMethod: AgregarFruta_DebeLanzarCajonLlenoException_CuandoElCajonEstaLleno
    }
}
