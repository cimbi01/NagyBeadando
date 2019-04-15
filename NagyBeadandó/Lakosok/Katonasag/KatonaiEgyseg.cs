using System.Collections.Generic;

namespace NagyBeadandó.Lakosok.Katonasag
{
    public class KatonaiEgyseg
    {
        #region Public Constructors

        public KatonaiEgyseg(bool tamad, List<Katona> katonak)
        {
            Katonak = katonak;
            Tamad = tamad;
            EroSzamitas();
        }

        #endregion Public Constructors

        #region Public Properties

        public int Erő { get; protected set; }
        public List<Katona> Katonak { get; protected set; }
        public bool Tamad { get; protected set; }

        #endregion Public Properties

        #region Private Methods

        private void EroSzamitas()
        {
            foreach (Katona item in Katonak)
            {
                Erő += item.VedoErtek;
                if (Tamad)
                {
                    Erő -= item.VedoErtek;
                    Erő += item.TamadoErtek;
                }
            }
        }

        #endregion Private Methods
    }
}
