namespace NagyBeadandó.Mezok.Alapok
{
    internal interface IMezo
    {
        #region Public Properties

        /// <summary>
        /// Azért kell, hogy mindenki beazonosithato legyen egy egyedi azonosito alapjan pl. csatánál rombolásnál, stb.
        /// </summary>
        int ID { get; }
        IInteraktivMezo InteraktivMezo { get; }
        /// <summary>
        /// "Renderelésnél" fontos, hogy ki tudja írni a mező nevét
        /// </summary>
        string Név { get; }
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
