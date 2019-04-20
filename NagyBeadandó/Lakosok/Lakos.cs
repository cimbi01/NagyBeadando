using System;

namespace NagyBeadandó.Lakosok
{
    /// <summary>
    /// Jatekos lakosa
    /// Ő támad, védekezik
    /// </summary>
    public class Lakos
    {
        #region Public Constructors

        /// <summary>
        /// Beállítja a lakos értékeit random számokkal
        /// </summary>
        public Lakos()
        {
            Random rnd = new Random();
            Fogyasztas = rnd.Next(1, 10);
            MenetSebesseg = rnd.Next(1, 10);
            TamadoErtek = rnd.Next(1, 10);
            VedoErtek = rnd.Next(1, 10);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// A Lakos fogyasztasa, amennyit a tarolo búza résézből le kell venni, ahhoz, hogy ne haljon meg
        /// </summary>
        public int Fogyasztas { get; protected set; }
        /// <summary>
        /// Jelzi, hogy itthon van-e a lakos, vagy épp támadásba menetel
        /// </summary>
        public bool ItthonVan { get; set; } = true;
        /// <summary>
        /// Jelzi, hogy a Fogyasztását levették-e már a tarolo buza részéből, azaz megetették-e
        /// </summary>
        public bool MegVanEtetve { get; set; } = false;
        /// <summary>
        /// Beállítja, hogy a lakos mekkora távot tesz meg egy iterációs egység alatt
        /// </summary>
        public int MenetSebesseg { get; private set; }
        /// <summary>
        /// Beállítja, hogy mennyi Vedőértéknyi lakos-t tud megölni egy támadás alatt
        /// </summary>
        public int TamadoErtek { get; private set; }
        /// <summary>
        /// Beállítja, hogy mennyi Vedőértéknyi lakos-t tud megölni egy védekezés alatt
        /// </summary>
        public int VedoErtek { get; protected set; }

        #endregion Public Properties
    }
}
