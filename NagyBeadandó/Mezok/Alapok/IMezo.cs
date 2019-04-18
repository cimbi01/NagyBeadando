namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Feladata, hogy minden Mezőnek adjon egy általános Interface-t amit implementálhatnak
    /// </summary>
    public interface IMezo
    {
        #region Public Properties

        /// <summary>
        /// Azért kell, hogy mindenki beazonosithato legyen egy egyedi azonosito alapjan pl. csatánál rombolásnál, stb.
        /// </summary>
        int ID { get; }
        IInteraktivMezo InteraktivMezo { get; }
        /// <summary>
        /// Visszaadja a mező típusát
        /// </summary>
        Tipusok.MezoTipusok MezoTipus { get; }
        /// <summary>
        /// "Renderelésnél" fontos, hogy ki tudja írni a mező nevét
        /// </summary>
        string Nev { get; }
        /// <summary>
        /// "Renderelésnél" fontos, hogy ki tudja írni a mező szintjét
        /// </summary>
        string Parameterek { get; }
        /// <summary>
        /// "Renderelésnél" fontos, hogy ki tudja írni a mező szintjét
        /// </summary>
        int Szint { get; }

        #endregion Public Properties
    }
}
