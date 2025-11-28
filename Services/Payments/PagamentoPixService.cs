using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Exceptions;

namespace HamburgueriaBlazor.Services.Payments
{
    public class PagamentoPixService : IPaymentService
    {
        public async Task<string> ProcessPaymentAsync(OrderHeader order)
        {
            // domain validation example
            if (order == null) throw new DomainException("Pedido inválido para pagamento via PIX.");
            if (order.OrderTotal <= 0) throw new DomainException("OrderTotal deve ser maior que zero.");

            // Simula geração de chave/qr code
            await Task.Delay(10);
            return $"PIX_QRCODE_{order.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}
