using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GestionCegepMobile.Adapters;
using GestionCegepMobile.Utils;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour l'affichage des Cégeps et l'ajout d'un Enseignant.
    /// </summary>
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    class EnseignantActivity : AppCompatActivity
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
        /// Liste des Enseignants.
        /// </summary>
        EnseignantDTO[] listeEnseignant;

        /// <summary>
        /// adapteur de la liste Enseignant.
        /// </summary>
        ListeEnseignantAdapter adapteurListeEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du numero de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtNoEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtNomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du prenom de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtPrenomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de l'adresse de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtAdresseEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la ville de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtVilleEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la province de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtProvinceEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du code postal de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du téléphone de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du courriel de l'Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtCourrielEnseignant;

        /// <summary>
        /// Attribut représentant le bouton pour l'ajout d'un Enseignant.
        /// </summary>
        private Button btnAjouterEnseignant;

        /// <summary>
        /// Attribut représentant le listView pour la liste des Enseignant.
        /// </summary>
        private ListView listeViewEnseignant;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Enseignant_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");

            listeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(paramNomCegep, paramNomDepartement).ToArray();
            
            edtNoEnseignant = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtNomEnseignant = FindViewById<EditText>(Resource.Id.edtNomInfo);
            edtPrenomEnseignant = FindViewById<EditText>(Resource.Id.edtPrenomInfo);
            edtAdresseEnseignant = FindViewById<EditText>(Resource.Id.edtAdresseInfo);
            edtVilleEnseignant = FindViewById<EditText>(Resource.Id.edtVilleInfo);
            edtProvinceEnseignant = FindViewById<EditText>(Resource.Id.edtProvinceInfo);
            edtCodePostalEnseignant = FindViewById<EditText>(Resource.Id.edtCodePostalInfo);
            edtCourrielEnseignant = FindViewById<EditText>(Resource.Id.edtCourrielInfo);
            edtTelephoneEnseignant = FindViewById<EditText>(Resource.Id.edtTelephoneInfo);
            btnAjouterEnseignant = FindViewById<Button>(Resource.Id.btnAjouter);
            btnAjouterEnseignant.Click += delegate
            {
                if (edtNoEnseignant.Text.Length>0 && edtNomEnseignant.Text.Length>0 && edtPrenomEnseignant.Text.Length>0 && edtAdresseEnseignant.Text.Length>0 && edtVilleEnseignant.Text.Length>0 && edtProvinceEnseignant.Text.Length>0 && edtCodePostalEnseignant.Text.Length>0 && edtCourrielEnseignant.Text.Length>0 && edtTelephoneEnseignant.Text.Length>0)
                {
                    try
                    {
                        EnseignantDTO lEnseignant = new EnseignantDTO(int.Parse(edtNoEnseignant.Text), edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text);
                        CegepControleur.Instance.AjouterEnseignant(paramNomCegep, paramNomDepartement, lEnseignant);
                        RafraichirInterfaceDonnees();
                        DialoguesUtils.AfficherToasts(this, lEnseignant.Nom + " ajouté !!!");
                        edtNoEnseignant.Text = edtNomEnseignant.Text = edtPrenomEnseignant.Text = edtAdresseEnseignant.Text = edtVilleEnseignant.Text = edtProvinceEnseignant.Text = edtCodePostalEnseignant.Text = edtCourrielEnseignant.Text = edtTelephoneEnseignant.Text = "";
                        edtNoEnseignant.RequestFocus();
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


            listeViewEnseignant = FindViewById<ListView>(Resource.Id.listViewEnseignant);
            listeViewEnseignant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteEnseignantDetails = new Intent(this, typeof(EnseignantDetailsActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteEnseignantDetails.PutExtra("paramNomCegep", paramNomCegep);
                activiteEnseignantDetails.PutExtra("paramNomDepartement", paramNomDepartement);
                activiteEnseignantDetails.PutExtra("paramNoEnseignant", listeEnseignant[e.Position].NoEmploye.ToString());
                //On démarre la nouvelle activité.
                StartActivity(activiteEnseignantDetails);
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
        /// Méthode permettant de rafraichir la liste des Enseignants
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                // Pour afficher la liste des enseignants
                listeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(paramNomCegep, paramNomDepartement).ToArray();
                adapteurListeEnseignant = new ListeEnseignantAdapter(this, listeEnseignant);
                listeViewEnseignant.Adapter = adapteurListeEnseignant;
            }
            catch (Exception ex)
            {
                DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
            }
        }

        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Enseignant_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Retour:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}