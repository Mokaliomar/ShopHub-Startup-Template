namespace DataAccess.Enums;

public enum PaymentStatus
{
    Pending = 1,     // قيد الانتظار
    Approved = 2,    // تم الدفع بنجاح
    Failed = 3,      // فشلت عملية الدفع
    Refunded = 4     // تم استرجاع المبلغ
}
