namespace SimPim.Api.Models;

public class Patient
{
    public int Id { get; set; }                     // ID automat 
    public string Nume { get; set; } = default!;    // Nume
    public string Prenume { get; set; } = default!; // Prenume
    public string IDNP { get; set; } = default!;    // IDNP 
    public string Gen { get; set; } = default!; // M sau F
    public DateOnly DataNastere { get; set; }       // Data nașterii
    public string Adresa { get; set; } = default!;  // Adresa
    public string Telefon { get; set; } = default!; // Telefon
    public DateTime DataInregistrarii { get; set; } = DateTime.Now; // Automat
}
