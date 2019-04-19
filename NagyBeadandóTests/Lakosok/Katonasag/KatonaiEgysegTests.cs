using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NagyBeadandó.Lakosok.Katonasag.Tests
{
    [TestClass()]
    public class KatonaiEgysegTests
    {
        #region Public Constructors

#pragma warning disable S3963 // "static" fields should be initialized inline
        static KatonaiEgysegTests()
        {
            for (int i = 0; i < 10; i++)
            {
                katonak.Add(new Katona(new Lakos(i), Mezok.Alapok.Tipusok.KatonaTipusok.Gyalogos));
                ero += (katonak[i] as Katona).TamadoErtek;
            }
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        #endregion Public Constructors

        #region Private Fields

        private static readonly List<Lakos> katonak = new List<Lakos>();
        private static readonly bool tamad = false;
        private static readonly int ero = 0;

        #endregion Private Fields

        #region Public Methods

        [TestMethod()]
        public void KatonaiEgysegTest()
        {
            KatonaiEgyseg katonaiEgyseg = new KatonaiEgyseg(tamad, katonak, 0);
            Assert.AreEqual(tamad, katonaiEgyseg.Tamad);
            Assert.AreEqual(katonak, katonaiEgyseg.Katonak);
            Assert.AreEqual(ero, katonaiEgyseg.Erő);
        }

        #endregion Public Methods
    }
}
