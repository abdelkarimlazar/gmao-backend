using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Services
{
    public class IntentService
    {
        private readonly AppDbContext _context;

        public IntentService(AppDbContext context)
        {
            _context = context;
        }

        //  Sync 
        public async Task<List<IntentDemandeIntervention>> GetNewDemandesAsync()
        {
            var fakeDemandes = new List<IntentDemandeIntervention>
            {
                new IntentDemandeIntervention
                {
                    IntentReclamationId = Guid.NewGuid().ToString(),
                    DateCreationIntent = DateTime.UtcNow,
                    DateReception = DateTime.UtcNow,

                    ClientNom = "Client Test",
                    ClientEmail = "client@test.com",
                    ClientTelephone = "0600000000",
                    ClientSociete = "Société Test",

                    EquipementRefIntent = "EQ-123",
                    EquipementDesignation = "Machine industrielle X",

                    DescriptionPanne = "Machine en panne",
                    NiveauUrgence = "Élevé",
                    Statut = "Soumise"
                }
            };

            foreach (var demande in fakeDemandes)
            {
                bool exists = await _context.IntentDemandes
                    .AnyAsync(d => d.IntentReclamationId == demande.IntentReclamationId);

                if (!exists)
                {
                    _context.IntentDemandes.Add(demande);
                }
            }

            await _context.SaveChangesAsync();

            return fakeDemandes;
        }

        //  ACCEPT
        public async Task<bool> AcceptDemande(int id, string user)
        {
            var demande = await _context.IntentDemandes.FindAsync(id);

            if (demande == null)
                return false;

            //  règle métier 
            if (demande.Statut != "Soumise")
                return false;

            var ancienStatut = demande.Statut;

            demande.Statut = "Acceptée";
            demande.TraiteePar = user;
            demande.DateTraitement = DateTime.UtcNow;

            
            demande.InterventionGMAOId = new Random().Next(1000, 9999);

            //  Historique
            _context.IntentHistoriques.Add(new IntentHistoriqueStatut
            {
                IntentDemandeId = demande.Id,
                AncienStatut = ancienStatut,
                NouveauStatut = "Acceptée",
                ActionPar = user,
                DateAction = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return true;
        }

        //  REFUSE
        public async Task<bool> RefuseDemande(int id, string motif, string user)
        {
            var demande = await _context.IntentDemandes.FindAsync(id);

            if (demande == null)
                return false;

            //  règle métier
            if (demande.Statut != "Soumise")
                return false;

            var ancienStatut = demande.Statut;

            demande.Statut = "Refusée";
            demande.MotifRefus = motif;
            demande.TraiteePar = user;
            demande.DateTraitement = DateTime.UtcNow;

            //  Historique
            _context.IntentHistoriques.Add(new IntentHistoriqueStatut
            {
                IntentDemandeId = demande.Id,
                AncienStatut = ancienStatut,
                NouveauStatut = "Refusée",
                ActionPar = user,
                DateAction = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return true;
        }

        //  HISTORIQUE
        public async Task<List<IntentHistoriqueStatut>> GetHistorique(int id)
        {
            return await _context.IntentHistoriques
                .Where(h => h.IntentDemandeId == id)
                .OrderByDescending(h => h.DateAction)
                .ToListAsync();
        }
    }
}