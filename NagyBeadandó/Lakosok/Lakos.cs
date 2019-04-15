namespace NagyBeadandó.Lakosok
{
    public class Lakos
    {
        #region Public Properties

        public Lakos()
        {
        }
        public Lakos(int id, int _Fogyasztas)
        {
            Fogyasztas = _Fogyasztas;
            ID = id;
        }
        public int Fogyasztas { get; protected set; }
        public int ID { get; protected set; }
        public bool MegVanEtetve { get; set; } = false;
        public int VedoErtek { get; protected set; } = 1;

        #endregion Public Properties
    }
}
