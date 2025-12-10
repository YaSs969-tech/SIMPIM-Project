namespace SimPim.Api.Models;

public class Investigatie
{
    public int Id { get; set; }                    // ID automat
    public string Cod { get; set; } = default!;    // cod unic
    public string Denumire { get; set; } = default!; // denumire completa 
    public List<ParametruInvestigatie> Parametri { get; set; } = new();         // Legatura  cu ParametruInvestigatie
}
