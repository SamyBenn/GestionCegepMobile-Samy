using Android.App;
using Android.Views;
using Android.Widget;
using ProjetCegep.DTOs;

/// <summary>
/// Namespace pour les adapteurs.
/// </summary>
namespace GestionCegepMobile.Adapters
{
    /// <summary>
    /// Classe représentant un adapteur pour une liste de Cours.
    /// </summary>
    class ListeCoursAdapter : BaseAdapter<CoursDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
		private Activity context;

        /// <summary>
        /// Attribut représentant la liste de Cegeps.
        /// </summary>
        private CoursDTO[] listeCours;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unContext">Context</param>
        /// <param name="uneListeCours">Liste des cours</param>
        public ListeCoursAdapter(Activity unContext, CoursDTO[] uneListeCours)
        {
            context = unContext;
            listeCours = uneListeCours;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste de Cours selon un index.
        /// </summary>
        /// <param name="index">Position du Cours</param>
        /// <returns>Retourne un CoursDTO contenant les informations du Cours selon l'index passé en paramètre.</returns>
        public override CoursDTO this[int index]
        {
            get { return listeCours[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le Id d'un Cours selon une position.
        /// </summary>
        /// <param name="position">Position du Cours.</param>
        /// <returns>Retourne le ID du Cours à la position demandée.</returns>
		public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de Cours dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre de Cours dans la liste.</returns>
		public override int Count
        {
            get { return listeCours.Length; }
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

            view.FindViewById<TextView>(Resource.Id.tvNomCegep).Text = listeCours[position].Nom;

            return view;
        }
    }
}