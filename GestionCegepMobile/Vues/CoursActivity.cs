using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using GestionCegepMobile.Adapters;
using GestionCegepMobile.Utils;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour l'affichage des Cégeps et l'ajout d'un Cégep.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class CoursActivity : AppCompatActivity
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
        /// Liste des Cours
        /// </summary>
        private CoursDTO[] listeCours;

        /// <summary>
        /// adapteur de la liste Cours
        /// </summary>
        private ListeCoursAdapter adapteurListeCours;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtNomCours;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtNoCours;

        /// <summary>
        /// Attribut représentant le champ d'édition
        /// </summary>
        private EditText edtDescriptionCours;

        /// <summary>
        /// Attribut représentant le bouton pour l'ajout d'un Cours
        /// </summary>
        private Button btnAjouterCours;

        /// <summary>
        /// Attribut représentant le listView pour la liste des Cours
        /// </summary>
        private ListView listViewCours;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Cours_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            listViewCours = FindViewById<ListView>(Resource.Id.listViewCours);
            listViewCours.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteCoursDetails = new Intent(this, typeof(CoursDetailsActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteCoursDetails.PutExtra("paramNomCegep", paramNomCegep);
                activiteCoursDetails.PutExtra("paramNomDepartement", paramNomDepartement);
                activiteCoursDetails.PutExtra("paramNomCours", listeCours[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteCoursDetails);
            };

            edtNomCours = FindViewById<EditText>(Resource.Id.edtNomInfo);
            edtNoCours = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtDescriptionCours = FindViewById<EditText>(Resource.Id.edtDescriptionInfo);
            btnAjouterCours = FindViewById<Button>(Resource.Id.btnAjouter);
            btnAjouterCours.Click += delegate
            {
                if (edtNomCours.Text.Length > 0 && edtNoCours.Text.Length > 0 && edtDescriptionCours.Text.Length > 0)
                {
                    try
                    {
                        CegepControleur.Instance.AjouterCours(paramNomCegep, paramNomDepartement, new CoursDTO(edtNoCours.Text, edtNomCours.Text, edtDescriptionCours.Text));
                        DialoguesUtils.AfficherToasts(this, edtNomCours.Text + " ajouté !!!");
                        RafraichirInterfaceDonnees();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
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
        /// Méthode permettant de rafraichir la liste des Cours
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                listeCours = CegepControleur.Instance.ObtenirListeCours(paramNomCegep, paramNomDepartement).ToArray();
                adapteurListeCours = new ListeCoursAdapter(this, listeCours);
                listViewCours.Adapter = adapteurListeCours;
                edtNomCours.Text = edtNoCours.Text = edtDescriptionCours.Text = "";
                edtNomCours.RequestFocus();
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
            MenuInflater.Inflate(Resource.Menu.Cours_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Quitter:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}