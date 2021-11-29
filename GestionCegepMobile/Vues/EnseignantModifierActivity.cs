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

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour la modification d'un Enseignant.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class EnseignantModifierActivity : AppCompatActivity
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
        /// Attribut représentant le champ d'édition du numero de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtNoEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtNomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du prenom de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtPrenomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de l'adresse de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtAdresseEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la ville de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtVilleEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la province de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtProvinceEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du code postal de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du téléphone de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du courriel de l'Enseignant pour la modification d'un Enseignant.
        /// </summary>
        private EditText edtCourrielEnseignant;

        /// <summary>
        /// Attribut représentant le bouton pour la modification d'un Enseignant.
        /// </summary>
        private Button btnModifierEnseignant;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EnseignantModifier_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNoEnseignant = int.Parse(Intent.GetStringExtra("paramNoEnseignant"));
            edtNoEnseignant = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtNomEnseignant = FindViewById<EditText>(Resource.Id.edtNomInfo);
            edtPrenomEnseignant = FindViewById<EditText>(Resource.Id.edtPrenomInfo);
            edtAdresseEnseignant = FindViewById<EditText>(Resource.Id.edtAdresseInfo);
            edtVilleEnseignant = FindViewById<EditText>(Resource.Id.edtVilleInfo);
            edtProvinceEnseignant = FindViewById<EditText>(Resource.Id.edtProvinceInfo);
            edtCodePostalEnseignant = FindViewById<EditText>(Resource.Id.edtCodePostalInfo);
            edtTelephoneEnseignant = FindViewById<EditText>(Resource.Id.edtTelephoneInfo);
            edtCourrielEnseignant = FindViewById<EditText>(Resource.Id.edtCourrielInfo);

            btnModifierEnseignant = FindViewById<Button>(Resource.Id.btnModifier);
            btnModifierEnseignant.Click += delegate
            {
                if (edtNoEnseignant.Text.Length > 0 && edtNomEnseignant.Text.Length > 0 && edtPrenomEnseignant.Text.Length > 0 && edtAdresseEnseignant.Text.Length > 0 && edtVilleEnseignant.Text.Length > 0 && edtProvinceEnseignant.Text.Length > 0 && edtCodePostalEnseignant.Text.Length > 0 && edtCourrielEnseignant.Text.Length > 0 && edtTelephoneEnseignant.Text.Length > 0)
                {
                    try
                    {
                        CegepControleur.Instance.ModifierEnseignant(paramNomCegep, paramNomDepartement, new EnseignantDTO(int.Parse(edtNoEnseignant.Text), edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text));
                        DialoguesUtils.AfficherToasts(this, "Enseignant modifié !!!");
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
        /// Méthode permettant de rafraichir les informations du Cégep...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                enseignant = CegepControleur.Instance.ObtenirEnseignant(paramNomCegep, paramNomDepartement, new EnseignantDTO(paramNoEnseignant));
                edtNoEnseignant.Text = enseignant.NoEmploye.ToString();
                edtNomEnseignant.Text = enseignant.Nom;
                edtPrenomEnseignant.Text = enseignant.Prenom;
                edtAdresseEnseignant.Text = enseignant.Adresse;
                edtVilleEnseignant.Text = enseignant.Ville;
                edtProvinceEnseignant.Text = enseignant.Province;
                edtCodePostalEnseignant.Text = enseignant.CodePostal;
                edtCourrielEnseignant.Text = enseignant.Courriel;
                edtTelephoneEnseignant.Text = enseignant.Telephone;
            }
            catch (Exception)
            {
                Finish();
            }
        }
    }
}