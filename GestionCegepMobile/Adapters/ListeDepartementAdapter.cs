using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Namespace pour les adapteurs
/// </summary>
namespace GestionCegepMobile.Adapters
{
    /// <summary>
    /// Classe représentant un adapteur pour une liste de departements
    /// </summary>
    class ListeDepartementAdapter : BaseAdapter<DepartementDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
		private Activity context;
        /// <summary>
        /// Attribut représentant la liste de departements.
        /// </summary>
		private DepartementDTO[] listeDepartement;

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="context">Contexte.</param>
        /// <param name="acteurs">Liste des departements.</param>
		public ListeDepartementAdapter(Activity unContext, DepartementDTO[] uneListeDep)
        {
            context = unContext;
            listeDepartement = uneListeDep;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste de departements selon un index.
        /// </summary>
        /// <param name="index">Index de la garderie.</param>
        /// <returns>Retourne un DepartementDTO contenant les informations du departement selon l'index passé en paramètre.</returns>
		public override DepartementDTO this[int index]
        {
            get { return listeDepartement[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le Id d'un departement selon une position.
        /// </summary>
        /// <param name="position">Position du Cégep.</param>
        /// <returns>Retourne le ID du departement à la position demandée.</returns>
		public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de departement(s) dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre de departement(s) dans la liste.</returns>
		public override int Count
        {
            get { return listeDepartement.Length; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le visuel d'un departement.
        /// </summary>
        /// <param name="position">Position du departement.</param>
        /// <param name="convertView">Vue.</param>
        /// <param name="parent">Parent de la vue.</param>
        /// <returns>Retourne une vue construite avec les données d'un departement.</returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeCegepItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomCegep).Text = listeDepartement[position].Nom;

            return view;
        }
    }
}