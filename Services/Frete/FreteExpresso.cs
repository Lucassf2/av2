namespace HamburgueriaBlazor.Services.Frete
{
    public class FreteExpresso : ICalculadoraFrete
    {
        public decimal CalcularFrete(decimal subtotal, string region)
        {
            decimal baseFee = 12.00m;
            decimal pct = 0.10m;
            return Math.Round(baseFee + subtotal * pct, 2);
        }
    }
}
