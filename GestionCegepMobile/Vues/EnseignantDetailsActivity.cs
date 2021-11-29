using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
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
{
    /// <summary>
    /// Classe de type Activité pour la gestion d'un Enseignant.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class EnseignantDetailsActivity : AppCompatActivity
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
        private int paramNoEnseignant;

        /// <summary>
        /// Attribut représentant l'enseignant selectionne.
        /// </summary>
        EnseignantDTO enseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du numero de l'Enseignant.
        /// </summary>
        private TextView lblNoEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du nom de l'Enseignant.
        /// </summary>
        private TextView lblNomEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du prenom de l'Enseignant.
        /// </summary>
        private TextView lblPrenomEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de l'adresse de l'Enseignant.
        /// </summary>
        private TextView lblAdresseEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la ville de l'Enseignant.
        /// </summary>
        private TextView lblVilleEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la province de l'Enseignant.
        /// </summary>
        private TextView lblProvinceEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du code postal de l'Enseignant.
        /// </summary>
        private TextView lblCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du téléphone de l'Enseignant.
        /// </summary>
        private TextView lblTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du courriel de l'Enseignant.
        /// </summary>
        private TextView lblCourrielEnseignant;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EnseignantDetails_activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNoEnseignant = int.Parse(Intent.GetStringExtra("paramNoEnseignant"));
            lblNoEnseignant = FindViewById<TextView>(Resource.Id.lblNoAfficher);
            lblNomEnseignant = FindViewById<TextView>(Resource.Id.lblNomAfficher);
            lblPrenomEnseignant = FindViewById<TextView>(Resource.Id.lblPrenomAfficher);
            lblAdresseEnseignant = FindViewById<TextView>(Resource.Id.lblAdresseAfficher);
            lblVilleEnseignant = FindViewById<TextView>(Resource.Id.lblVilleAfficher);
            lblProvinceEnseignant = FindViewById<TextView>(Resource.Id.lblProvinceAfficher);
            lblCodePostalEnseignant = FindViewById<TextView>(Resource.Id.lblCodePostalAfficher);
            lblTelephoneEnseignant = FindViewById<TextView>(Resource.Id.lblTelephoneAfficher);
            lblCourrielEnseignant = FindViewById <TextView>(Resource.Id.lblCourrielAfficher);

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
        /// Méthode permettant de rafraichir la liste des Enseignants
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            enseignant = CegepControleur.Instance.ObtenirEnseignant(paramNomCegep, paramNomDepartement, new EnseignantDTO(paramNoEnseignant));
            lblNoEnseignant.Text = enseignant.NoEmploye.ToString();
            lblNomEnseignant.Text = enseignant.Nom;
            lblPrenomEnseignant.Text = enseignant.Prenom;
            lblAdresseEnseignant.Text = enseignant.Adresse;
            lblVilleEnseignant.Text = enseignant.Ville;
            lblProvinceEnseignant.Text = enseignant.Province;
            lblCodePostalEnseignant.Text = enseignant.CodePostal;
            lblTelephoneEnseignant.Text = enseignant.Telephone;
            lblCourrielEnseignant.Text = enseignant.Courriel;
        }

        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité principale.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.EnseignantDetails_ActivityMenu, menu);
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
                Intent activiteModifier = new Intent(this, typeof(EnseignantModifierActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteModifier.PutExtra("paramNomCegep", paramNomCegep);
                    activiteModifier.PutExtra("paramNomDepartement", paramNomDepartement);
                    activiteModifier.PutExtra("paramNoEnseignant", enseignant.NoEmploye.ToString());
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
                                CegepControleur.Instance.SupprimerEnseignant(paramNomCegep, paramNomDepartement, enseignant);
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