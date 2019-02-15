using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
   
    [TestClass]
    public class GenreDATest
    {
        [TestMethod]
        public void GetGenerotByNameTest()
        {
            var da = new GenreDA();

            Assert.IsTrue(da.GetGenreWithSP("a%").Count > 0);

        }
        [TestMethod]
        public void insertGeneroTest()
        {
            var da = new GenreDA();
            var nuevoGenero = da.InsertGenre(
                new Genre() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoGenero > 0);

        }
    }
}
