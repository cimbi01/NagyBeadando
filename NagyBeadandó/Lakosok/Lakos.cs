namespace NagyBeadandó.Lakosok
{
    public class Lakos
    {
        #region Private Fields

        private static int CurrentId = 0;

        #endregion Private Fields

        #region Public Properties

        public Lakos(int _Fogyasztas = 1)
        {
            Fogyasztas = _Fogyasztas;
            ID = CurrentId++;
        }
        public Lakos(Lakos lakos)
        {
            Fogyasztas = lakos.Fogyasztas;
            ID = lakos.ID;
        }
        public int Fogyasztas { get; protected set; }
        public int ID { get; protected set; }
        public bool ItthonVan { get; set; } = true;
        public bool Katona { get; protected set; } = false;
        public bool MegVanEtetve { get; set; } = false;
        public int MenetSebesseg { get; private set; } = 5;
        public int TamadoErtek { get; private set; } = 1;
        public int VedoErtek { get; protected set; } = 1;

        #endregion Public Properties
    }
}
