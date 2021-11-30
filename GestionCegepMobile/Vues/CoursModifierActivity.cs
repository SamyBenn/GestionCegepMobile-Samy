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
            edtNomCoursModifier = FindViewById<EditText>(Resource.Id.edtNomModifier);
            edtNoCoursModifier = FindViewById<EditText>(Resource.Id.edtNoModifier);
            edtDescriptionCoursModifier = FindViewById<EditText>(Resource.Id.edtDescriptionModifier);
            btnModifierCours = FindViewById<Button>(Resource.Id.btnModifier);
            btnModifierCours.Click += delegate
            {
                if (edtNomCoursModifier.Text.Length>0 && edtNoCoursModifier.Text.Length>0 && edtDescriptionCoursModifier.Text.Length>0)
                {
                    try
                    {
                        CegepControleur.Instance.ModifierCours(paramNomCegep, paramNomDepartement, new CoursDTO(edtNoCoursModifier.Text, edtNomCoursModifier.Text, edtDescriptionCoursModifier.Text));
                        DialoguesUtils.AfficherToasts(this, paramNomCours + " modifié !!!");
                        Finish();
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                }
                else
                {
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Veuillez remplir tous les champs...");
                }
            };
        }

        /// <summary>
        /// Méthode de service appelée lors du retour en avant plan de l'activité.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            RafraichirInterfaceDonnees();
        }

        /// <summary>
        /// Méthode permettant de rafraichir les informations du Cours...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                cours = CegepControleur.Instance.ObtenirCours(paramNomCegep, paramNomDepartement, new CoursDTO("", paramNomCours));
                edtNomCoursModifier.Text = cours.Nom;
                edtNoCoursModifier.Text = cours.No;
                edtDescriptionCoursModifier.Text = cours.Description;
            }
            catch (Exception)
            {
                Finish();
            }
        }

    }
}