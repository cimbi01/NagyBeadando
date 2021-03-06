﻿using System.Collections.Generic;

namespace NagyBeadandó.Lakosok.Katonasag
{
    /// <summary>
    /// A Katonak tarolasara, erejuk kiszamolasara hasznaljuk
    /// </summary>
    public class KatonaiEgyseg
    {
        #region Public Constructors

        /// <summary>
        /// Beállítja az egyseg erejét, a katonákat, és a hogy támad-e
        /// </summary>
        /// <param name="tamad">Jelzi, hogy az egység tamad vagy védekezik (ha igaz, akkor tamad)</param>
        /// <param name="katonak">A katonak, akik az egyseg tagjai</param>
        /// <param name="jatekos_id">A jatekos, aki az egyseg katonait szamlalja</param>
        public KatonaiEgyseg(bool tamad, List<Lakos> katonak, int jatekos_id)
        {
            Jatekos_Id = jatekos_id;
            Katonak = katonak;
            EroSzamitas(tamad);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Egység ereje támadás/védekezés függvényében
        /// </summary>
        public int Erő { get; protected set; }
        /// <summary>
        /// A jatekos, aki az egyseg katonait szamlalja
        /// </summary>
        public int Jatekos_Id { get; private set; }
        /// <summary>
        /// >A katonak, akik az egyseg tagjai
        /// </summary>
        public List<Lakos> Katonak { get; protected set; }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Kiszamitja a katonak összeerejét
        /// Úgy, hogy egyesével hozzadja az aktualis tamadas függvényében
        /// Az erejuket az Ero-hoz
        /// </summary>
        private void EroSzamitas(bool tamad)
        {
            foreach (Lakos item in Katonak)
            {
                Erő += item.VedoErtek;
                if (tamad)
                {
                    Erő -= item.VedoErtek;
                    Erő += item.TamadoErtek;
                }
            }
        }

        #endregion Private Methods
    }
}
