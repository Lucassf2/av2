namespace HamburgueriaBlazor.Services.Frete
{
    public class FretePadrao : ICalculadoraFrete
    {
        public decimal CalcularFrete(decimal subtotal, string region)
        {
            // Frete fixo mínimo + percentual sobre subtotal
            decimal baseFee = 5.00m;
            decimal pct = 0.05m;
            return Math.Round(baseFee + subtotal * pct, 2);
        }
    }
}
