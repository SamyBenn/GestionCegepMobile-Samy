using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using GestionCegepMobile.Utils;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using GestionCegepMobile.Adapters;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour la gestion d'un Cégep.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class CoursDetailsActivity : AppCompatActivity
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
        /// Attribut représentant l'étiquette pour l'affichage du nom du cours
        /// </summary>
        private TextView lblNomCours;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du numero du cours
        /// </summary>
        private TextView lblNoCours;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la description du cours
        /// </summary>
        private TextView lblDescriptionCours;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CoursDetails_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNomCours = Intent.GetStringExtra("paramNomCours");
            lblNomCours = FindViewById<TextView>(Resource.Id.lblNomAfficher);
            lblNoCours = FindViewById<TextView>(Resource.Id.lblNoAfficher);
            lblDescriptionCours = FindViewById<TextView>(Resource.Id.lblDescriptionAfficher);
            cours = CegepControleur.Instance.ObtenirCours(paramNomCegep, paramNomDepartement, new CoursDTO("", paramNomCours));
            lblNomCours.Text = cours.Nom;
            lblNoCours.Text = cours.No;
            lblDescriptionCours.Text = cours.Description;
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
        /// Méthode permettant de rafraichir les informations du cours ...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                cours = CegepControleur.Instance.ObtenirCours(paramNomCegep, paramNomDepartement, new CoursDTO("", paramNomCours));
                lblNomCours.Text = cours.Nom;
                lblNoCours.Text = cours.No;
                lblDescriptionCours.Text = cours.Description;
            }
            catch (Exception)
            {
                Finish();
            }
        }

        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité principale.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.CoursDetails_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Modifier:
                    Intent activiteModifier = new Intent(this, typeof(CoursModifierActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteModifier.PutExtra("paramNomCegep", paramNomCegep);
                    activiteModifier.PutExtra("paramNomDepartement", paramNomDepartement);
                    activiteModifier.PutExtra("paramNomCours", paramNomCours);
                    //On démarre la nouvelle activité.
                    StartActivity(activiteModifier);
                    break;

                case Resource.Id.Supprimer:
                    try
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetPositiveButton("Non", (send, args) => { });
                        builder.SetNegativeButton("Oui", (send, args) =>
                        {
                            try
                            {
                                CegepControleur.Instance.SupprimerCours(paramNomCegep, paramNomDepartement, cours);
                                Finish();
                            }
                            catch (Exception ex)
                            {
                                DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                            }

                        });
                        AlertDialog dialog = builder.Create();
                        dialog.SetTitle("Suppression");
                        dialog.SetMessage("Voulez-vous vraiment supprimer le Cours ?");
                        dialog.Window.SetGravity(GravityFlags.Bottom);
                        dialog.Show();
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                    break;

                case Resource.Id.Retour:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}