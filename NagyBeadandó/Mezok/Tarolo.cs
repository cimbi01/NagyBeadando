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

        public void Betesz(Tipusok.Tarolhatok tarolhato, int mennyit)
        {
            if (!Kapacitas.ContainsKey(tarolhato))
            {
                throw new NemTartalmazTarolhatotException(tarolhato);
            }
            Kapacitas[tarolhato][0] += mennyit;
            if (Kapacitas[tarolhato][0] > Kapacitas[tarolhato][1])
            {
                throw new TaroloTulCsordultException(tarolhato, mennyit, Kapacitas[tarolhato][1]);
            }
        }
        /// <summary>
        /// Visszaadja mmenyit-nyit a Tárolhato-ból
        /// </summary>
        /// <param name="tarolhato">A típus, amiből lekérjük a mennyiséget</param>
        /// <param name="mennyit">A mennyiség, hogy mennyit szeretne kivenni a tárolóból</param>
        /// <returns>Visszaad mennyit-nyit, ha van belőle mennyit-nyi a kapacitásban, ellenkező esetben dob egy NincsElégTarolhatoException-t</returns>
        public int Kivesz(Tipusok.Tarolhatok tarolhato, int mennyit)
        {
            if (!Kapacitas.TryGetValue(tarolhato, out int[] tarolt_mennyiseg))
            {
                throw new NemTartalmazTarolhatotException(tarolhato);
            }
            else if (tarolt_mennyiseg[0] < mennyit)
            {
                throw new NincsElegTarolhatoException(tarolhato);
            }
            tarolt_mennyiseg[0] -= mennyit;
            return mennyit;
        }
        /// <summary>
        /// Visszaadja, hogy az adott tipusból a tárolt mennyiség egyenlő-e a maxkapacitással
        /// </summary>
        /// <param name="tarolhato">A tárolt tipus fajtája</param>
        /// <returns>Igazat ad vissza, ha a tárolható típusból maxkapacitásnyi van, minden más esetben hamist</returns>
        public virtual bool MegVanTelve(Tipusok.Tarolhatok tarolhato)
        {
            return Kapacitas[tarolhato][0] == Kapacitas[tarolhato][1];
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
        public Tarolo(Tarolo tarolo) : base(tarolo.MezoTipus)
        {
            Kapacitas = tarolo.Kapacitas;
            Parameterek = tarolo.Parameterek;
            VanBennePublikusMetodus = Metodusok.Count != 0;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Minden típushoz tárolja, hogy mennyit tárol és hogy mennyit tárolhat
        /// A tömb első eleme a mennyiség
        /// Második eleme a maximum kapacitás
        /// </summary>
        public Dictionary<Tipusok.Tarolhatok, int[]> Kapacitas { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int[]>();

        #endregion Public Properties

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
    }
}
