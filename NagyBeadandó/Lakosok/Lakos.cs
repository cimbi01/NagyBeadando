using System;

namespace NagyBeadandó.Lakosok
{
    public class Lakos
    {
        #region Public Properties

        public Lakos()
        {
            Random rnd = new Random();
            Fogyasztas = rnd.Next(1, 10);
            MenetSebesseg = rnd.Next(1, 10);
            TamadoErtek = rnd.Next(1, 10);
            VedoErtek = rnd.Next(1, 10);
        }
        public Lakos(Lakos lakos)
        {
            Fogyasztas = lakos.Fogyasztas;
            MenetSebesseg = lakos.MenetSebesseg;
            TamadoErtek = lakos.TamadoErtek;
            VedoErtek = lakos.VedoErtek;
        }
        public int Fogyasztas { get; protected set; }
        public bool ItthonVan { get; set; } = true;
        public bool MegVanEtetve { get; set; } = false;
        public int MenetSebesseg { get; private set; }
        public int TamadoErtek { get; private set; }
        public int VedoErtek { get; protected set; }

        #endregion Public Properties
    }
}
