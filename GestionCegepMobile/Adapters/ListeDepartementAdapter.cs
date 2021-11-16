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

namespace GestionCegepMobile.Adapters
{
    class ListeDepartementAdapter : BaseAdapter<DepartementDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
		private Activity context;
        /// <summary>
        /// Attribut représentant la liste de Cegeps.
        /// </summary>
		private DepartementDTO[] listeDep;

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="context">Contexte.</param>
        /// <param name="acteurs">Liste des Cégeps.</param>
		public ListeDepartementAdapter(Activity unContext, DepartementDTO[] uneListeDep)
        {
            context = unContext;
            listeDep = uneListeDep;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste de Cégeps selon un index.
        /// </summary>
        /// <param name="index">Index de la garderie.</param>
        /// <returns>Retourne un CegepDTO contenant les informations du Cégep selon l'index passé en paramètre.</returns>
		public override DepartementDTO this[int index]
        {
            get { return listeDep[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le Id d'un Cégep selon une position.
        /// </summary>
        /// <param name="position">Position du Cégep.</param>
        /// <returns>Retourne le ID du Cégep à la position demandée.</returns>
		public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de Cégep(s) dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre de Cégep(s) dans la liste.</returns>
		public override int Count
        {
            get { return listeDep.Length; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le visuel d'un Cégep.
        /// </summary>
        /// <param name="position">Position du Cégep.</param>
        /// <param name="convertView">Vue.</param>
        /// <param name="parent">Parent de la vue.</param>
        /// <returns>Retourne une vue construite avec les données d'un Cégep.</returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeCegepItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomCegep).Text = listeDep[position].Nom;

            return view;
        }
    }
}