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
    /// Classe de type Activité pour la modification d'un Departement.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    class DepartementModifierActivity : AppCompatActivity
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
        /// Attribut représentant le champ d'édition du nom du Departement pour la modification d'un Departement. 
        /// </summary>
        private DepartementDTO leDepartement;

        /// <summary>
        /// Attribut représentant le numero du departement
        /// </summary>
        EditText edtNoDepartement;

        /// <summary>
        /// Attribut représentant le nom du departement
        /// </summary>
        EditText edtNomDepartement;

        /// <summary>
        /// Attribut représentant la description du departement
        /// </summary>
        EditText edtDescriptionDepartement;

        /// <summary>
        /// Attribut représentant le bouton de modification.
        /// </summary>
        Button btnModifier;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DepartementModifier_Activity);

            edtNoDepartement = FindViewById<EditText>(Resource.Id.edtNoDepartement);
            edtNomDepartement = FindViewById<EditText>(Resource.Id.edtNomDepartement);
            edtDescriptionDepartement = FindViewById<EditText>(Resource.Id.edtDescriptionDepartement);
            btnModifier = FindViewById<Button>(Resource.Id.btnModifier);
            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");

            btnModifier.Click += delegate
            {
                if (edtNoDepartement.Text.Length > 0 && edtNomDepartement.Text.Length > 0 && edtDescriptionDepartement.Text.Length > 0)
                {
                    try
                    {
                        CegepControleur.Instance.ModifierDepartement(paramNomCegep, leDepartement);
                        DialoguesUtils.AfficherToasts(this, paramNomDepartement + " modifié !!!");
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

        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité principale.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DepartementModifier_ActivityMenu, menu);
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