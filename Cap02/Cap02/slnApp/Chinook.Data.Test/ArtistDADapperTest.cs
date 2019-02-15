using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class ArtistDADapperTest
    {
        [TestMethod]
        public void GetCountTest()
        {
            var da = new ArtistDapperDA();

            Assert.IsTrue(da.GetCount() > 0);

        }
        [TestMethod]
        public void GetArtistTest()
        {
            var da = new ArtistDapperDA();

            Assert.IsTrue(da.GetArtists().Count > 0);

        }

        [TestMethod]
        public void GetArtistByNameTest()
        {
            var da = new ArtistDapperDA();

            Assert.IsTrue(da.GetArtists("a%").Count > 0);

        }

        [TestMethod]
        public void insertArtistTest()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtistConOutput(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }

        [TestMethod]
        public void insertArtistTestTRansaccion()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtistTrans(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() }
                );


            Assert.IsTrue(nuevoArtista > 0);

        }
    }
}
