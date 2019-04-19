using System.Collections.Generic;

namespace NagyBeadandó.Lakosok.Katonasag
{
    public class KatonaiEgyseg
    {
        #region Public Constructors

        public KatonaiEgyseg(bool tamad, List<Lakos> katonak, int jatekos_id)
        {
            Jatekos_Id = jatekos_id;
            Katonak = katonak;
            Tamad = tamad;
            EroSzamitas();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Egység ereje támadás/védekezés függvényében
        /// </summary>
        public int Erő { get; protected set; }
        public int Jatekos_Id { get; private set; }
        public List<Lakos> Katonak { get; protected set; }
        public bool Tamad { get; protected set; }

        #endregion Public Properties

        #region Private Methods

        private void EroSzamitas()
        {
            foreach (Lakos item in Katonak)
            {
                Erő += item.VedoErtek;
                if (Tamad)
                {
                    Erő -= item.VedoErtek;
                    Erő += (item as Katona).TamadoErtek;
                }
            }
        }

        #endregion Private Methods
    }
}
