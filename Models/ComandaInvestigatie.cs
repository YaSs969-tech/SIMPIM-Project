namespace SimPim.Api.Models;

public class ComandaInvestigatie
{
    public int Id { get; set; }                           // ID automat
    public int PatientId { get; set; }                    // pacientul care a cerut analiza
    public int InvestigatieId { get; set; }               // tipul de analiză 
    public string CodInvestigatie { get; set; } = default!; // cod investigație
    public string DenumireInvestigatie { get; set; } = default!; // ex. Analiza generală a sângelui
    public DateTime DataComanda { get; set; } = DateTime.Now;   // data cererii
    public DateTime? DataInvestigatie { get; set; }           // data efectuării investigației
    public DateTime? DataRezultate { get; set; }              // data introducerii rezultatelor
    public string Status { get; set; } = "Created";       // Statut comandă
    public List<RezultatInvestigatie> Rezultate { get; set; } = new();     // Legătura  cu RezultatInvestigatie
}
