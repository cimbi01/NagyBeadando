using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
    internal class TaroloTulCsordultException : TarolhatoException
    {
        #region Public Properties

        public int AdottMennyiseg { get; private set; }
        public int MaxKapacitas { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Inicializálja a kivétel tagjait
        /// </summary>
        /// <param name="tarolhatok">A tárolható típusa, ami a Tárolóba próbált tenni</param>
        /// <param name="mennyiseg">A tárolható mennyisége, amennyit a Tárolóba próbált tenni</param>
        /// <param name="kapacitas">A Tároló max kapacitása az adott tárolhatóból</param>
        public TaroloTulCsordultException(Tipusok.Tarolhatok tarolhatok, int mennyiseg, int kapacitas) : base(tarolhatok, "A tárolóban nincs több hely")
        {
            AdottMennyiseg = mennyiseg;
            MaxKapacitas = kapacitas;
        }

        #endregion Public Constructors
    }
}
