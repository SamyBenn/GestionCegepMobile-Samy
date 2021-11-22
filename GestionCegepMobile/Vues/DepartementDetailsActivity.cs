using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues

{   /// <summary>
    /// Classe de type Activité pour la gestion d'un Département.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class DepartementDetailsActivity : AppCompatActivity
    {
        /// <summary>
        /// Attribut représentant le numero du departement
        /// </summary>
        private TextView NoDep;

        /// <summary>
        /// Attribut représentant le nom du departement
        /// </summary>
        private TextView NomDep;

        /// <summary>
        /// Attribut représentant la description du departement
        /// </summary>
        private TextView DescDep;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DepartementDetails_Activity);

            NoDep = FindViewById<TextView>(Resource.Id.lblNoDepartInfo);
            NomDep = FindViewById<TextView>(Resource.Id.lblNomDepartInfo);
            DescDep = FindViewById<TextView>(Resource.Id.lblDescDepartInfo);
        }

        /// <summary>
        /// Méthode de service appelée lors du retour en avant plan de l'activité.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            RafraichirInterfaceDonnees();
        }

        private void RafraichirInterfaceDonnees()
        {

        }

        private void Initialize()
        {
        }
    }
}