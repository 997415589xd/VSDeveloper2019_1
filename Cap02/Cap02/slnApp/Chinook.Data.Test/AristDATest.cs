using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class ArtistDATest
    {
        [TestMethod]
        public void GetCountTest()
        {
            var da = new ArtistDA();

            Assert.IsTrue(da.GetCount() > 0);

        }

        [TestMethod]
        public void GetArtistTest()
        {
            var da = new ArtistDA();

            Assert.IsTrue(da.GetArtists().Count > 0);

        }

        [TestMethod]
        public void GetArtistByNameTest()
        {
            var da = new ArtistDA();

            Assert.IsTrue(da.GetArtists("a%").Count > 0);

        }
        [TestMethod]
        public void insertArtistTest()
        {
            var da = new ArtistDA();
            var nuevoArtista = da.InsertArtist(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }

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

        [TestMethod]
        public void insertArtistTestTRansaccion()
        {
            var da = new ArtistDA();
            var nuevoArtista = da.InsertArtistTrans(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }
    }
}
