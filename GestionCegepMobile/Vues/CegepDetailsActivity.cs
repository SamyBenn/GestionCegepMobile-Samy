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
        private ListView listeDepartements;

        /// <summary>
        /// liste des departements a afficher.
        /// </summary>
        private DepartementDTO[] listeDep;

        /// <summary>
        /// Adapteur de la liste des departements
        /// </summary>
        private ListeDepartementAdapter adapteurListeDep;

        /// <summary>
        /// Attribut représentant le champ d'édition du numero de departement
        /// </summary>
        private EditText edtNoDep;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de departement
        /// </summary>
        private EditText edtNomDep;

        /// <summary>
        /// Attribut représentant le champ d'édition de la description du departement
        /// </summary>
        private EditText edtDescDep;

        /// <summary>
        /// Attribut représentant le boutton pour l'ajout d'un departement
        /// </summary>
        private Button btnAjouterDep;

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
            listeDepartements = FindViewById<ListView>(Resource.Id.listViewCegepAfficher);
            listeDepartements.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteDepartement = new Intent(this, typeof(DepartementActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteDepartement.PutExtra("paramNomDep", listeDep[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteDepartement);
            };

            edtNoDep = FindViewById<EditText>(Resource.Id.edtNoDepInfo);
            edtNomDep = FindViewById<EditText>(Resource.Id.edtNomDepInfo);
            edtDescDep = FindViewById<EditText>(Resource.Id.edtDescriptionDepInfo);
            paramNomCegep = Intent.GetStringExtra("paramNomCegep");

            btnAjouterDep = FindViewById<Button>(Resource.Id.btnAjouterDepartement);
            btnAjouterDep.Click += delegate
            {
                if ((edtNoDep.Text.Length > 0) && (edtNomDep.Text.Length > 0) && (edtDescDep.Text.Length > 0))
                {
                    string nomCegep = lblNomCegepAfficher.Text;
                    DepartementDTO monDep = new DepartementDTO(edtNoDep.Text, edtNomDep.Text, edtDescDep.Text);
                    CegepControleur.Instance.AjouterDepartement(nomCegep, monDep);
                    RafraichirInterfaceDonnees();
                    DialoguesUtils.AfficherToasts(this, edtNomDep.Text + " ajouté !!!");
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
                listeDep = CegepControleur.Instance.ObtenirListeDepartement(leCegep.Nom).ToArray();
                adapteurListeDep = new ListeDepartementAdapter(this, listeDep);
                listeDepartements.Adapter = adapteurListeDep;
                
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