namespace RMS.Models.Common;

public class EnumForEntity
{
    public enum NoeAmalyatKhakiAbnie
    {
        KhakBardari = 1,
        KhakRizi = 2,
        PayKani = 3,
    }

    public enum NoeAmalyatKhaki
    {
        LajanBardari = 10,
        KhakBardariNabati = 11,
        KhakBardariDarZaminNarm = 12,
        KhakBardariDarZaminDoj = 13,
        KhakBardariDarZaminSangi = 14,
        KhakBardariDarZaminRaza = 15,
        KhakBardariDarZaminMonbasetShavande = 16,
    }

    public enum NoeFehrestBaha
    {
        RahDari = 22,
        Abnie = 11,
        RahoBand = 21,
        Barghi = 13,
        Mekaniki = 12,
        MareMat = 16
    }

    /// <summary>
    /// نحوه محاسبه مقدار
    /// آیتم های تجمیعی
    /// </summary>
    public enum CalculateValueType
    {
        Manual = 1,
        FromItem = 2,
        Calculate = 3,
    }

    public enum NoeBarAvord
    {
        BarAvord = 1,
        PishnahadGhaimat = 2,
        Gharardad = 3,
        SooratVaziat = 4
    }

    public enum UserType
    {
        Real = 1,//حقیقی
        Legal = 2//حقوقی
    }
    public enum CompanyType
    {
        KarFarma = 1,//کارفرما
        Moshaver = 2,//مشاور
        PayManKar = 3//پیمانکار
    }

    public enum SettingType
    {
        FillValueShowHide = 1,//نمایش یا عدم نمایش مقادیر پر شده در بخش نمایش برآورد

    }
    public enum VariableType
    {
        Bool = 1,//ترو یا فالس
        Number = 2,
        String = 3

    }

    public enum NotifyType
    {
        ErrorStar = 1,//جمع مبلغ برآورد ردیف‌های ستاره‌دار، نسبت به جمع مبلغ برآورد ردیف‌های فهرست‌بها

    }

    /// <summary>
    /// نوع فرآیند
    /// </summary>
    public enum ProccesType
    {
        BarAvordAvalieh = 1,
        //BarAvordAvaliehL2 = 2,
        SooratVaziat = 2,
        PishnahadGhaimat = 3
    }

    /// <summary>
    /// نحوه دسترسی به پروژه برای کابران
    /// مربوط به بخش گردش کار    
    /// </summary>
    public enum ProjectAccessType
    {
        Owner = 1,
        Edit = 2,
        View = 3
    }

    public enum WorkflowStatus
    {
        Draft = 0,

        InProgress = 1,

        Completed = 2,

        Rejected = 3,

        Cancelled = 4
    }

    public enum WorkflowAction
    {
        Submit = 1,      // انجام مرحله

        Approve = 2,     // تایید نهایی

        Revision = 3,    // درخواست اصلاح

        Reject = 4,      // عدم پذیرش انجام کار

        Cancel = 5       // لغو فرآیند
    }

    public enum WorkflowAssignmentResult
    {
        Pending = 1,

        Submitted = 2,

        RevisionRequested = 3,

        Rejected = 4,

        Cancelled = 5
    }

    public enum WorkflowReceiverMode
    {
        FirstResponse = 1,// اولین پاسخ کافی است

        AllMustRespond = 2,  // همه باید پاسخ دهند

        MinimumCount = 3     // حداقل N نفر پاسخ دهند
    }

    public enum AssignmentStatus
    {
        Waiting = 0,

        Approved = 1,

        Rejected = 2,

        Returned = 3,

        Cancelled = 4
    }

    public enum ItemFieldType
    {
        Tedad = 1,      // تعداد
        Tool = 2,       // طول
        Arz = 3,        // عرض
        Ertefa = 4,     // ارتفاع
        Vazn = 5,      // وزن
        MeghdarJoz = 6        // مقدار جزء
    }

    public enum WorkflowInstanceStatus
    {
        /// <summary>
        /// گردش ایجاد شده ولی هنوز شروع نشده
        /// </summary>
        Created = 1,


        /// <summary>
        /// گردش فعال است و در حال انجام می‌باشد
        /// </summary>
        Running = 2,


        /// <summary>
        /// گردش متوقف شده
        /// مثلا منتظر تصمیم مدیر
        /// </summary>
        Paused = 3,


        /// <summary>
        /// گردش تکمیل شده
        /// </summary>
        Completed = 4,


        /// <summary>
        /// گردش رد شده و پایان یافته
        /// </summary>
        Rejected = 5,


        /// <summary>
        /// گردش لغو شده
        /// </summary>
        Cancelled = 6
    }
}
