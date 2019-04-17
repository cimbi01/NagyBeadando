using Microsoft.VisualStudio.TestTools.UnitTesting;
using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Lakosok.Katonasag.Tests
{
    [TestClass()]
    public class KatonaTests
    {
        #region Public Methods

        [TestMethod()]
        public void KatonaTest()
        {
            Katona katona = new Katona(lakos, this.katona_tipus);
            Assert.AreEqual(5, katona.MenetSebesseg);
            Assert.AreEqual(10, katona.RomboloErtek);
            Assert.AreEqual(10, katona.TamadoErtek);
            Assert.AreEqual(10, katona.VedoErtek);
        }

        #endregion Public Methods

        #region Private Fields

        private static readonly int ID = 0;
        private static readonly Lakos lakos = new Lakos(ID);
        private readonly Tipusok.KatonaTipusok katona_tipus = Tipusok.KatonaTipusok.Gyalogos;

        #endregion Private Fields
    }
}
