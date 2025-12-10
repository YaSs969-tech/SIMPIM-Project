namespace SimPim.Api.Models;

public class ParametruInvestigatie
{
    public int Id { get; set; }                         // ID automat
    public int InvestigatieId { get; set; }             // legatura cu analiza parinte
    public string CodParametru { get; set; } = default!; // ex. WBC
    public string Denumire { get; set; } = default!;     // ex. Leucocite
    public string Unitate { get; set; } = default!;      // ex. 10⁹/L
    public double? ValoareMin { get; set; }              // valori admisibile
    public double? ValoareMax { get; set; }              // valori admisibile
}
