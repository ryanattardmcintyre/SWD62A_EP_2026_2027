using Common.Interfaces;

namespace WebApplication1.Services.BookingPolicies
{
    public class WinterBookingPolicy : IBookingPolicy
    {
        public DateTime CalculateDueDate(DateTime? startDate)
        {
            if (startDate != null) return startDate.Value.AddDays(7);
            
            return DateTime.Now.AddDays(7);
        }
    }
}
