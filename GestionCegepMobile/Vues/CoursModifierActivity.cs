using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using GestionCegepMobile.Utils;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour la modification d'un Cégep.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class CoursModifierActivity : AppCompatActivity
    {
        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomDepartement;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomCours;

        /// <summary>
        /// Attribut représentant le DTO du Cours
        /// </summary>
        CoursDTO cours;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtNomCoursModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtNoCoursModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtDescriptionCoursModifier;

        /// <summary>
        /// Attribut représentant le bouton pour la modification d'un Cours
        /// </summary>
        private Button btnModifierCours;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CoursModifier_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNomCours = Intent.GetStringExtra("paramNomCours");
            edtNomCoursModifier = FindViewById<EditText>(Resource.Id.edtNomInfo);
            edtNoCoursModifier = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtDescriptionCoursModifier = FindViewById<EditText>(Resource.Id.edtDescriptionInfo);
        }
    }
}