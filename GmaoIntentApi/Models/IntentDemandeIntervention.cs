using System;

namespace GmaoIntentApi.Models
{
    public class IntentDemandeIntervention
    {
        public int Id { get; set; }

        public string IntentReclamationId { get; set; } = null!;

        public DateTime DateCreationIntent { get; set; }
        public DateTime DateReception { get; set; }

        public string ClientNom { get; set; } = null!;
        public string ClientEmail { get; set; } = null!;

        public string? ClientTelephone { get; set; }
        public string? ClientSociete { get; set; }

        public string? EquipementRefIntent { get; set; }
        public string? EquipementDesignation { get; set; }

        public string DescriptionPanne { get; set; } = null!;
        public string NiveauUrgence { get; set; } = null!;
        public string Statut { get; set; } = null!;

        public string? MotifRefus { get; set; }

        public int? InterventionGMAOId { get; set; }

        public string? TraiteePar { get; set; }
        public DateTime? DateTraitement { get; set; }
    }
}