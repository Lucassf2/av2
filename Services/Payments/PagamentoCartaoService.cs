using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Exceptions;

namespace HamburgueriaBlazor.Services.Payments
{
    public class PagamentoCartaoService : IPaymentService
    {
        public async Task<string> ProcessPaymentAsync(OrderHeader order)
        {
            if (order == null) throw new DomainException("Pedido inválido para pagamento com cartão.");
            if (order.OrderTotal <= 0) throw new DomainException("OrderTotal deve ser maior que zero.");

            // Aqui poderia chamar gateway de cartão; retornamos token simulado
            await Task.Delay(10);
            return $"CARD_TXN_{order.Id}_{Guid.NewGuid().ToString().Split('-')[0]}";
        }
    }
}
