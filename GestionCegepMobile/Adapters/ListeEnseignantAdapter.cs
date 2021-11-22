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
/// Namespace pour les adapteurs.
/// </summary>
namespace GestionCegepMobile.Adapters
{
    /// <summary>
    /// Classe représentant un adapteur pour une liste d'enseignants.
    /// </summary>
    class ListeEnseignantAdapter : BaseAdapter<EnseignantDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
        private Activity context;

        /// <summary>
        /// Attribut représentant la liste d'enseignants.
        /// </summary>
        private EnseignantDTO[] listeEnseignant;

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="context">Contexte.</param>
        /// <param name="acteurs">Liste des enseignants.</param>
        public ListeEnseignantAdapter(Activity unContext, EnseignantDTO[] uneListeEnseignant)
        {
            context = unContext;
            listeEnseignant = uneListeEnseignant;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste d'enseignants selon un index.
        /// </summary>
        /// <param name="index">Index de la garderie.</param>
        /// <returns>Retourne un EnseignantDTO contenant les informations de l'enseignant selon l'index passé en paramètre.</returns>
		public override EnseignantDTO this[int index]
        {
            get { return listeEnseignant[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le Id d'un enseignant selon une position.
        /// </summary>
        /// <param name="position">Position de l'enseignant.</param>
        /// <returns>Retourne le ID de l'enseignant à la position demandée.</returns>
		public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre d'enseignants dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre d'enseignants dans la liste.</returns>
		public override int Count
        {
            get { return listeEnseignant.Length; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le visuel d'un enseignant.
        /// </summary>
        /// <param name="position">Position de l'enseignant.</param>
        /// <param name="convertView">Vue.</param>
        /// <param name="parent">Parent de la vue.</param>
        /// <returns>Retourne une vue construite avec les données d'un enseignant.</returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeCegepItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomCegep).Text = listeEnseignant[position].Nom;

            return view;
        }
    }
}