using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class GenreDADapperTest
    {
        [TestMethod]
        public void GetGenerotByNameTest()
        {
            var da = new GenreDapperDA();

            Assert.IsTrue(da.GetGenreWithSP("a%").Count > 0);

        }
        [TestMethod]
        public void insertGeneroTest()
        {
            var da = new GenreDapperDA();
            var nuevoGenero = da.InsertGenre(
                new Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoGenero > 0);

        }

        [TestMethod]
        public void insertGeneroTestTRansaccion()
        {
            var da = new GenreDapperDA();
            var nuevoArtista = da.InsertGenreTrans(
                new Genre() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }

        [TestMethod]
        public void insertGeneroTestTRansaccionDistribuida()
        {
            var da = new GenreDapperDA();
            var nuevoArtista = da.InsertGenreTransDistribuida(
                new Genre() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }

        //[TestMethod]
        //public void UpdateGeneroTest()
        //{
        //    var da = new GenreDapperDA();
        //    var nuevoGenero = da.UpdateGenre(
        //        new Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() }
        //        );


        //    Assert.IsTrue(nuevoGenero > 0);

        //}

    }
}
