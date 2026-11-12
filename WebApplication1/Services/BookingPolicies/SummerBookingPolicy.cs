using Common.Interfaces;

namespace WebApplication1.Services.BookingPolicies
{
    public class SummerBookingPolicy : IBookingPolicy
    {
        public DateTime CalculateDueDate(DateTime? startDate)
        {
            if (startDate != null) return startDate.Value.AddDays(14);

            return DateTime.Now.AddDays(14);
        }
    }
}
