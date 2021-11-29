using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using GestionCegepMobile.Utils;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlertDialog = Android.App.AlertDialog;

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
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        string paramNomDepartement;

        /// <summary>
        /// attribut representant le DTO du departement
        /// </summary>
        DepartementDTO leDepartement;

        /// <summary>
        /// Attribut représentant le numero du departement
        /// </summary>
        private TextView lblNoDepartement;

        /// <summary>
        /// Attribut représentant le nom du departement
        /// </summary>
        private TextView lblNomDepartement;

        /// <summary>
        /// Attribut représentant la description du departement
        /// </summary>
        private TextView lblDescriptionDepartement;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DepartementDetails_Activity);

            lblNoDepartement = FindViewById<TextView>(Resource.Id.lblNoDepartInfo);
            lblNomDepartement = FindViewById<TextView>(Resource.Id.lblNomDepartInfo);
            lblDescriptionDepartement = FindViewById<TextView>(Resource.Id.lblDescDepartInfo);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            leDepartement = CegepControleur.Instance.ObtenirDepartement(paramNomCegep, paramNomDepartement);

            lblNoDepartement.Text = leDepartement.No;
            lblNomDepartement.Text = leDepartement.Nom;
            lblDescriptionDepartement.Text = leDepartement.Description;
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
        /// Méthode permettant de rafraichir les informations du departement ...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                leDepartement = CegepControleur.Instance.ObtenirDepartement(paramNomCegep, paramNomDepartement);
                lblNoDepartement.Text = leDepartement.No;
                lblNomDepartement.Text = leDepartement.Nom;
                lblDescriptionDepartement.Text = leDepartement.Description;
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
            MenuInflater.Inflate(Resource.Menu.DepartementDetails_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Enseignants:
                    Intent activiteEnseignants = new Intent(this, typeof(EnseignantActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteEnseignants.PutExtra("paramNomCegep", paramNomCegep);
                    activiteEnseignants.PutExtra("paramNomDepartement", leDepartement.Nom);
                    //On démarre la nouvelle activité.
                    StartActivity(activiteEnseignants);
                    break;

                case Resource.Id.Cours:
                    Intent activiteCours = new Intent(this, typeof(CoursActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteCours.PutExtra("paramNomCegep", paramNomCegep);
                    activiteCours.PutExtra("paramNomDepartement", leDepartement.Nom);
                    //On démarre la nouvelle activité.
                    StartActivity(activiteCours);
                    break;

                case Resource.Id.Supprimer:
                    try
                    {
                        Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetPositiveButton("Non", (send, args) => { });
                        builder.SetNegativeButton("Oui", (send, args) =>
                        {
                            try
                            {
                                CegepControleur.Instance.SupprimerDepartement(paramNomCegep, paramNomDepartement);
                                Finish();
                            }
                            catch (Exception ex)
                            {
                                DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                            }

                        });
                        AlertDialog dialog = builder.Create();
                        dialog.SetTitle("Suppression");
                        dialog.SetMessage("Voulez-vous vraiment supprimer le Cégep ?");
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