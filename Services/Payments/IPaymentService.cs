using HamburgueriaBlazor.Data;

namespace HamburgueriaBlazor.Services.Payments
{
    public interface IPaymentService
    {
        /// <summary>
        /// Process a payment for the given order header.
        /// Implementations throw domain-specific exceptions when something is invalid.
        /// </summary>
        Task<string> ProcessPaymentAsync(OrderHeader order);
    }
}
