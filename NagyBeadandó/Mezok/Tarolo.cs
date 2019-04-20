using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagyBeadandó.Mezok
{
    /// <summary>
    /// Feladata:
    /// Tárolni a különböző tárolható típusokat
    /// Lekérni a különböző tárolható típusokból
    /// </summary>
    public class Tarolo : Mezo
    {
        #region Public Methods

        /// <summary>
        /// A Kapacitás tároló részéhez [0] hozzáad mennyit-nyit
        /// Ha így nagyobb akkor teletölti, majd dob egy TaroloTulCsordultException-t
        /// </summary>
        /// <param name="tarolhato">A tárolható típusa, amelyik rekeszbe szeretnék tenni a mennyit</param>
        /// <param name="mennyit">A mennyiség, amennyit be szeretnénk tenni a rekeszbe</param>
        public void Betesz(Tipusok.Tarolhatok tarolhato, int mennyit)
        {
            if (!Kapacitas.ContainsKey(tarolhato))
            {
                throw new NemTartalmazTarolhatotException();
            }
            int tarolo_maradekhely = Kapacitas[tarolhato][1] - Kapacitas[tarolhato][0];
            if (mennyit > tarolo_maradekhely)
            {
                Kapacitas[tarolhato][0] += tarolo_maradekhely;
                throw new TaroloTulCsordultException();
            }
            Kapacitas[tarolhato][0] += mennyit;
        }
        /// <summary>
        /// Visszaadja mmenyit-nyit a Tárolhato-ból
        /// </summary>
        /// <param name="tarolhato">A típus, amiből lekérjük a mennyiséget</param>
        /// <param name="mennyit">A mennyiség, hogy mennyit szeretne kivenni a tárolóból</param>
        /// <returns>Visszaad mennyit-nyit, ha van belőle mennyit-nyi a kapacitásban, ellenkező esetben dob egy NincsElégTarolhatoException-t</returns>
        public void Kivesz(Tipusok.Tarolhatok tarolhato, int mennyit)
        {
            if (!Kapacitas.TryGetValue(tarolhato, out int[] tarolt_mennyiseg))
            {
                throw new NemTartalmazTarolhatotException();
            }
            else if (tarolt_mennyiseg[0] < mennyit)
            {
                throw new NincsElegTarolhatoException();
            }
            tarolt_mennyiseg[0] -= mennyit;
        }

        #endregion Public Methods

        #region Public Constructors

        // <summary>
        /// Inicializalja az osszes mezejet
        /// </summary>
        /// <param name="id">Mezo ID-je</param>
        /// <param name="mezotipus">Mezo tipusa</param>
        /// <param name="kapacitas">Mezo tarolhatok szerinti tipusa</param>
        public Tarolo(Tipusok.MezoTipusok mezotipus, List<Tipusok.Tarolhatok> tarolhatok) : base(mezotipus)
        {
            VanBennePublikusMetodus = Metodusok.Count != 0;
            Random rnd = new Random();
            foreach (Tipusok.Tarolhatok item in tarolhatok)
            {
                int[] kapacitasok = new int[2] { 0, rnd.Next(500, 1000) };
                Kapacitas.Add(item, kapacitasok);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Minden típushoz tárolja, hogy mennyit tárol és hogy mennyit tárolhat
        /// A tömb első eleme a mennyiség
        /// Második eleme a maximum kapacitás
        /// </summary>
        /// Meg lehetne oldani, hogy a value egy objektum tömb legyen
        /// Így egyszerre meghatározva az objektumok listáját
        /// És a maximum kapacitást
        /// Ezzel leegszerűsítve a modellt
        /// (Hiba: Csak a Lakosnak van osztálya, a nyersanyagok-nak egyáltalán nincs)
        public Dictionary<Tipusok.Tarolhatok, int[]> Kapacitas { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int[]>();
        /// <summary>
        /// Visszaadja a Tarolo Parametereit:
        /// Nevét, Tárolt Tipusok szerint mennyiséget, kapacitást, típust
        /// </summary>
        public override string Parameterek
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Név : " + Nev);
                stringBuilder.AppendLine();
                stringBuilder.Append("Tárolt típusok: ");
                stringBuilder.AppendLine();
                foreach (Tipusok.Tarolhatok item in Kapacitas.Keys)
                {
                    stringBuilder.Append("Típus: ");
                    stringBuilder.Append(item.ToString());
                    stringBuilder.Append(" Mennyiség: ");
                    Kapacitas.TryGetValue(item, out int[] value);
                    stringBuilder.Append(value[0].ToString());
                    stringBuilder.Append(", Kapacitás: ");
                    stringBuilder.Append(value[1].ToString());
                    stringBuilder.Append(";");
                    stringBuilder.AppendLine();
                }
                return stringBuilder.ToString();
            }
        }

        #endregion Public Properties
    }
}
