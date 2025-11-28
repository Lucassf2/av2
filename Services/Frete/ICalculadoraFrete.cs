namespace HamburgueriaBlazor.Services.Frete
{
    public interface ICalculadoraFrete
    {
        /// <summary>
        /// Calcula o valor do frete com base no subtotal (preco total dos itens) e na região.
        /// </summary>
        decimal CalcularFrete(decimal subtotal, string region);
    }
}
