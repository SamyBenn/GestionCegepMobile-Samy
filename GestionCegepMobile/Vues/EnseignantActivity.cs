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
        /// Attribut représentant le champ d'édition du numero du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtNoEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtNomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du prenom du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtPrenomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de l'adresse du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtAdresseEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la ville du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtVilleEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la province du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtProvinceEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du code postal du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du téléphone du Enseignant pour l'ajout d'un Enseignant.
        /// </summary>
        private EditText edtTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du courriel du Enseignant pour l'ajout d'un Enseignant.
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
                    EnseignantDTO lEnseignant = new EnseignantDTO(int.Parse(edtNoEnseignant.Text), edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text);
                    CegepControleur.Instance.AjouterEnseignant(paramNomCegep, paramNomDepartement, lEnseignant);
                }
            };
        }
    }
}