using Microsoft.VisualStudio.TestTools.UnitTesting;
using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Tests
{
    [TestClass()]
    public class TaroloTests
    {
        #region Public Constructors

#pragma warning disable S3963 // "static" fields should be initialized inline
        static TaroloTests()
        {
            kapacitas.Add(Tipusok.Tarolhatok.Agyag, kapacitasok);
            tarolo = new Tarolo(mezotipus, kapacitas);
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        #endregion Public Constructors

        #region Public Methods

        [TestMethod()]
        public void MegVanTelveTest()
        {
            Assert.AreEqual(false, tarolo.MegVanTelve(Tipusok.Tarolhatok.Agyag));
        }

        #endregion Public Methods

        #region Private Fields

        private static readonly int[] kapacitasok = new int[] { 0, 1 };
        private static readonly Tipusok.MezoTipusok mezotipus = Tipusok.MezoTipusok.Agyagbanya;
        private static Dictionary<Tipusok.Tarolhatok, int[]> kapacitas = new Dictionary<Tipusok.Tarolhatok, int[]>();
        private static Tarolo tarolo;

        #endregion Private Fields

        #region Public Methods

        [TestMethod()]
        public void TaroloTest()
        {
            Assert.AreEqual(0, tarolo.ID);
            Assert.AreEqual(mezotipus, tarolo.MezoTipus);
            Assert.AreEqual(kapacitas, tarolo.Kapacitas);
        }

        #endregion Public Methods

        [TestMethod()]
        [ExpectedException(typeof(NincsElegTarolhatoException))]
        public void KiveszTestNincsEleg()
        {
            Tipusok.Tarolhatok tarolhatok = Tipusok.Tarolhatok.Agyag;
            int mennyit = 2;
            int kivett = tarolo.Kivesz(tarolhatok, mennyit);
            Assert.AreEqual(2, kivett);
        }
        [TestMethod()]
        [ExpectedException(typeof(NemTartalmazTarolhatotException))]
        public void KiveszTestNemTartalmaz()
        {
            Tipusok.Tarolhatok tarolhatok = Tipusok.Tarolhatok.Buza;
            int mennyit = 2;
            int kivett = tarolo.Kivesz(tarolhatok, mennyit);
            Assert.AreEqual(2, kivett);
        }
        [TestMethod()]
        public void KiveszTest()
        {
            Tipusok.Tarolhatok tarolhatok = Tipusok.Tarolhatok.Agyag;
            int mennyit = 0;
            int kivett = tarolo.Kivesz(tarolhatok, mennyit);
            Assert.AreEqual(0, kivett);
        }
    }
}
