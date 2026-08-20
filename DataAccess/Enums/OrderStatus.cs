namespace DataAccess.Enums;

public enum OrderStatus
{
    Pending = 1,      // الطلب قيد الانتظار / المراجعة
    Approved = 2,     // تم تأكيد الطلب وقبوله
    Processing = 3,   // جاري تجهيز الطلب للشحن
    Shipped = 4,      // تم تسليمه لشركة الشحن
    Delivered = 5,    // تم التوصيل للعميل بنجاح
    Cancelled = 6,    // تم إلغاء الطلب
    Refunded = 7      // تم استرجاع المبلغ
}
