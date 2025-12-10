using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Creational;


/// Builder – construiește lista de rezultate pentru o comandă de investigație.

public class InvestigationResultsBuilder
{
    private readonly int _orderId;
    private readonly List<RezultatInvestigatie> _results = new();

    public InvestigationResultsBuilder(int orderId)
    {
        _orderId = orderId;
    }

    public InvestigationResultsBuilder Add(RezultatInvestigatie input)
    {
        _results.Add(new RezultatInvestigatie
        {
            ComandaId = _orderId,
            CodParametru = input.CodParametru,
            DenumireParametru = input.DenumireParametru,
            Valoare = input.Valoare,
            Unitate = input.Unitate,
            Interpretare = input.Interpretare
        });

        return this;
    }

    public List<RezultatInvestigatie> Build() => _results;
}
