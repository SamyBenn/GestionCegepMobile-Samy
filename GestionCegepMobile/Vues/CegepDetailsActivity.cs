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
    public class CegepDetailsActivity : AppCompatActivity
    {
        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le DTO du Cégep.
        /// </summary>
        private CegepDTO leCegep;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du nom du Cégep.
        /// </summary>
        private TextView lblNomCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de l'adresse du Cégep.
        /// </summary>
        private TextView lblAdresseCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la ville du Cégep.
        /// </summary>
        private TextView lblVilleCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la province du Cégep.
        /// </summary>
        private TextView lblProvinceCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du code postal du Cégep.
        /// </summary>
        private TextView lblCodePostalCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du téléphone du Cégep.
        /// </summary>
        private TextView lblTelephoneCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du courriel du Cégep.
        /// </summary>
        private TextView lblCourrielCegepAfficher;

        /// <summary>
        /// Attribut représentant la liste des département du Cégep.
        /// </summary>
        private ListView listViewDepartement;

        /// <summary>
        /// liste des departements a afficher.
        /// </summary>
        private DepartementDTO[] listeDepartement;

        /// <summary>
        /// Adapteur de la liste des departements
        /// </summary>
        private ListeDepartementAdapter adapteurListeDepartement;

        /// <summary>
        /// Attribut représentant le champ d'édition du numero de departement
        /// </summary>
        private EditText edtNoDepartement;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de departement
        /// </summary>
        private EditText edtNomDepartement;

        /// <summary>
        /// Attribut représentant le champ d'édition de la description du departement
        /// </summary>
        private EditText edtDescriptionDepartement;

        /// <summary>
        /// Attribut représentant le boutton pour l'ajout d'un departement
        /// </summary>
        private Button btnAjouterDepartement;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CegepDetails_Activity);

            lblNomCegepAfficher = FindViewById<TextView>(Resource.Id.lblNomAfficher);
            lblAdresseCegepAfficher = FindViewById<TextView>(Resource.Id.lblAdresseAfficher);
            lblVilleCegepAfficher = FindViewById<TextView>(Resource.Id.lblVilleAfficher);
            lblProvinceCegepAfficher = FindViewById<TextView>(Resource.Id.lblProvinceAfficher);
            lblCodePostalCegepAfficher = FindViewById<TextView>(Resource.Id.lblCodePostalAfficher);
            lblTelephoneCegepAfficher = FindViewById<TextView>(Resource.Id.lblTelephoneAfficher);
            lblCourrielCegepAfficher = FindViewById<TextView>(Resource.Id.lblCourrielAfficher);
            listViewDepartement = FindViewById<ListView>(Resource.Id.listViewDepartementAfficher);
            listViewDepartement.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteDepartement = new Intent(this, typeof(DepartementDetailsActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteDepartement.PutExtra("paramNomDep", listeDepartement[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteDepartement);
            };

            edtNoDepartement = FindViewById<EditText>(Resource.Id.edtNoDepInfo);
            edtNomDepartement = FindViewById<EditText>(Resource.Id.edtNomDepInfo);
            edtDescriptionDepartement = FindViewById<EditText>(Resource.Id.edtDescriptionDepInfo);
            paramNomCegep = Intent.GetStringExtra("paramNomCegep");

            btnAjouterDepartement = FindViewById<Button>(Resource.Id.btnAjouterDepartement);
            btnAjouterDepartement.Click += delegate
            {
                if ((edtNoDepartement.Text.Length > 0) && (edtNomDepartement.Text.Length > 0) && (edtDescriptionDepartement.Text.Length > 0))
                {
                    string nomCegep = lblNomCegepAfficher.Text;
                    DepartementDTO monDep = new DepartementDTO(edtNoDepartement.Text, edtNomDepartement.Text, edtDescriptionDepartement.Text);
                    CegepControleur.Instance.AjouterDepartement(nomCegep, monDep);
                    RafraichirInterfaceDonnees();
                    DialoguesUtils.AfficherToasts(this, edtNomDepartement.Text + " ajouté !!!");
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
        /// Méthode permettant de rafraichir les informations du Cégep...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                leCegep = CegepControleur.Instance.ObtenirCegep(paramNomCegep);
                lblNomCegepAfficher.Text = leCegep.Nom;
                lblAdresseCegepAfficher.Text = leCegep.Adresse;
                lblVilleCegepAfficher.Text = leCegep.Ville;
                lblProvinceCegepAfficher.Text = leCegep.Province;
                lblCodePostalCegepAfficher.Text = leCegep.CodePostal;
                lblTelephoneCegepAfficher.Text = leCegep.Telephone;
                lblCourrielCegepAfficher.Text = leCegep.Courriel;

                // Pour afficher la liste des departements
                listeDepartement = CegepControleur.Instance.ObtenirListeDepartement(leCegep.Nom).ToArray();
                adapteurListeDepartement = new ListeDepartementAdapter(this, listeDepartement);
                listViewDepartement.Adapter = adapteurListeDepartement;
                
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
            MenuInflater.Inflate(Resource.Menu.CegepDetails_ActivityMenu, menu);
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
                    Intent activiteModifier = new Intent(this, typeof(CegepModifierActivity));
                    activiteModifier.PutExtra("paramNomCegep", leCegep.Nom);
                    StartActivity(activiteModifier);
                    break;

                case Resource.Id.Supprimer:
                    try
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetPositiveButton("Non", (send, args) =>{});
                        builder.SetNegativeButton("Oui", (send, args) =>
                        {
                            try
                            {
                                CegepControleur.Instance.SupprimerCegep(leCegep.Nom);
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