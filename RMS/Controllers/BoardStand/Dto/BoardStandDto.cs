namespace RMS.Controllers.BoardStand.Dto
{
    public class BoardStandDto
    {
        public Guid BaravordId { get; set; }
        public int Tedad { get; set; }
        /// <summary>
        /// 1-پایه تابلو اطلاعاتی
        /// 2-پایه تابلو دایره ای مثلثی
        /// 3-پایه تابلو مسیر نما و بال کبوتری
        /// 4-پایه تابلو اطلاعاتی با سطح کوچک
        /// </summary>
        public int BoardStandType { get; set; }
        public int Year { get; set; }
    }
}
