using System;

namespace GmaoIntentApi.Models
{
    public class IntentHistoriqueStatut
    {
        public int Id { get; set; }

        public int IntentDemandeId { get; set; }

        public string AncienStatut { get; set; } = null!;
        public string NouveauStatut { get; set; } = null!;

        public string ActionPar { get; set; } = null!;

        public DateTime DateAction { get; set; }
    }
}