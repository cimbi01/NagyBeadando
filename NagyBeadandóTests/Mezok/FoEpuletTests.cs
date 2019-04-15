using Microsoft.VisualStudio.TestTools.UnitTesting;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Tests
{
    [TestClass()]
    public class FoEpuletTests
    {
        #region Public Constructors

#pragma warning disable S3963 // "static" fields should be initialized inline
        static FoEpuletTests()
        {
            kapacitas.Add(Tipusok.Tarolhatok.Katona, kapacitasok);
            fepulet = new FoEpulet(id, mezotipus, kapacitas);
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        #endregion Public Constructors

        #region Private Fields

        private static readonly int id = 0;
        private static readonly Dictionary<Tipusok.Tarolhatok, int[]> kapacitas = new Dictionary<Tipusok.Tarolhatok, int[]>();
        private static readonly int[] kapacitasok = new int[] { 0, 1 };
        private static readonly Tipusok.MezoTipusok mezotipus = Tipusok.MezoTipusok.Kaszarnya;
        private static readonly FoEpulet fepulet;

        #endregion Private Fields

        [TestMethod()]
        public void TermelTest()
        {
            foreach (Tipusok.Tarolhatok item in fepulet.Kapacitas.Keys)
            {
                Assert.AreEqual(false, fepulet.MegVanTelve(item));
                fepulet.Termel(item);
                Assert.AreEqual(true, fepulet.MegVanTelve(item));
            }
        }
    }
}
