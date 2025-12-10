using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Creational;


/// Implementare concretă de Factory Method pentru comenzi standard / urgente.

public class StandardOrderFactory : IOrderFactory
{
    public ComandaInvestigatie CreateOrder(Patient patient, Investigatie investigation, bool isUrgent)
    {
        return new ComandaInvestigatie
        {
            PatientId = patient.Id,
            InvestigatieId = investigation.Id,
            CodInvestigatie = investigation.Cod,
            DenumireInvestigatie = investigation.Denumire,
            DataComanda = DateTime.Now,
            Status = isUrgent ? "Created-Urgent" : "Created"
        };
    }
}
