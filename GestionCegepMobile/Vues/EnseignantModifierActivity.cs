using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
    class EnseignantModifierActivity
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
        private Button btnAjouterEnseignant;
    }
}