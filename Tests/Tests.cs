using App.Entidades;

namespace Tests
{
    public class Tests
    {
        private Biblioteca _biblioteca;
        private Libro _libro1;
        private Libro _libro2;

        [SetUp]
        public void Setup()
        {
            _biblioteca = new Biblioteca();
            _libro1 = new Libro("1984", "George Orwell");
            _libro2 = new Libro("El Principito", "Antoine de Saint-Exupéry");
            _biblioteca.AgregarLibro(_libro1);
            _biblioteca.AgregarLibro(_libro2);
        }

        [Test]
        public void PrestarLibro_LibroDisponible_PrestaLibroCorrectamente()
        {
            // Act
            _biblioteca.PrestarLibro("1984");

            // Assert
            Assert.IsTrue(_libro1.EstaPrestado, "El libro debería estar marcado como prestado");
        }

        [Test]
        public void PrestarLibro_LibroNoDisponible_LanzaExcepcion()
        {
            // Arrange
            _biblioteca.PrestarLibro("1984");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _biblioteca.PrestarLibro("1984");
            }, "Debería lanzar excepción al intentar prestar un libro ya prestado");
        }

        [Test]
        public void PrestarLibro_LibroInexistente_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _biblioteca.PrestarLibro("No existe");
            }, "Debería lanzar excepción si el libro no está en la biblioteca");
        }

        [Test]
        public void DevolverLibro_LibroPrestado_DevuelveLibroCorrectamente()
        {
            // Arrange
            _biblioteca.PrestarLibro("El Principito");

            // Act
            _biblioteca.DevolverLibro("El Principito");

            // Assert
            Assert.IsFalse(_libro2.EstaPrestado, "El libro debería estar disponible después de devolverlo");
        }

        [Test]
        public void DevolverLibro_LibroNoPrestado_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _biblioteca.DevolverLibro("1984");
            }, "Debería lanzar excepción si se intenta devolver un libro que no estaba prestado");
        }

        [Test]
        public void DevolverLibro_LibroInexistente_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _biblioteca.DevolverLibro("No existe");
            }, "Debería lanzar excepción si el libro no está en la biblioteca");
        }

        [Test]
        public void ObtenerLibros_RetornaListaDeLibros()
        {
            // Act
            var libros = _biblioteca.ObtenerLibros();

            // Assert
            Assert.AreEqual(2, libros.Count, "La biblioteca debería contener 2 libros");
            CollectionAssert.Contains(libros, _libro1);
            CollectionAssert.Contains(libros, _libro2);
        }
    }
}