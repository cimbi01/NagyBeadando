using Microsoft.VisualStudio.TestTools.UnitTesting;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Tests
{
    [TestClass()]
    public class NyersanyagMezoTests
    {
        #region Public Constructors

#pragma warning disable S3963 // "static" fields should be initialized inline
        static NyersanyagMezoTests()
        {
            kapacitas.Add(Tipusok.Tarolhatok.Agyag, kapacitasok);
            nymezo = new NyersanyagMezo(mezotipus, kapacitas);
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        #endregion Public Constructors

        #region Private Fields

        private static readonly Dictionary<Tipusok.Tarolhatok, int[]> kapacitas = new Dictionary<Tipusok.Tarolhatok, int[]>();
        private static readonly int[] kapacitasok = new int[] { 0, 1 };
        private static readonly Tipusok.MezoTipusok mezotipus = Tipusok.MezoTipusok.Agyagbanya;
        private static readonly NyersanyagMezo nymezo;

        #endregion Private Fields

        #region Public Methods

        [TestMethod()]
        public void TermelTest()
        {
            foreach (Tipusok.Tarolhatok item in nymezo.Kapacitas.Keys)
            {
                Assert.AreEqual(false, nymezo.MegVanTelve(item));
                nymezo.Termel(item);
                Assert.AreEqual(true, nymezo.MegVanTelve(item));
            }
        }

        #endregion Public Methods
    }
}
