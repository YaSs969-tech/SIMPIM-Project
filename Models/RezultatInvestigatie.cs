namespace SimPim.Api.Models;

public class RezultatInvestigatie
{
    public int Id { get; set; }                         // ID automat
    public int ComandaId { get; set; }                  // legătură cu comanda părinte
    public string CodParametru { get; set; } = default!; // ex. WBC
    public string DenumireParametru { get; set; } = default!; // ex. Leucocite
    public string Valoare { get; set; } = default!;     // valoarea introdusă
    public string Unitate { get; set; } = default!;     // unitatea (ex. 10⁹/L)
    public string? Interpretare { get; set; }           // text opțional 
}
