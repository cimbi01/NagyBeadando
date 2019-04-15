namespace NagyBeadandó.Lakosok
{
    public class Lakos
    {
        #region Public Properties

        public Lakos(int id, int _Fogyasztas)
        {
            Fogyasztas = _Fogyasztas;
            ID = id;
        }
        public int Fogyasztas { get; private set; }
        public int ID { get; private set; }
        public bool MegVanEtetve { get; set; } = false;

        #endregion Public Properties
    }
}
