using Microsoft.VisualStudio.TestTools.UnitTesting;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Tests
{
    [TestClass()]
    public class TaroloTests
    {
        #region Public Methods

        [TestMethod()]
        public void TaroloTest()
        {
            int id = 0;
            Tipusok.MezoTipusok mezotipus = Tipusok.MezoTipusok.Agyagbanya;
            Dictionary<Tipusok.Tarolhatok, int[]> kapacitas = new Dictionary<Tipusok.Tarolhatok, int[]>();
            int[] kapacitasok = new int[] { 0, 1 };
            kapacitas.Add(Tipusok.Tarolhatok.Agyag, kapacitasok);
            Tarolo tarolo = new Tarolo(id, mezotipus, kapacitas);
            Assert.AreEqual(id, tarolo.ID);
            Assert.AreEqual(mezotipus, tarolo.MezoTipus);
            Assert.AreEqual(kapacitas, tarolo.Kapacitas);
        }

        #endregion Public Methods
    }
}
