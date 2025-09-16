namespace RMS.Models.Common
{
    public class BoardEnum
    {
        public static class SizeCircle
        {
            public const int Diameter300 = 300;
            public const int Diameter450 = 450;
            public const int Diameter600 = 600;
            public const int Diameter750 = 750;
            public const int Diameter900 = 900;
            public const int Diameter1200 = 1200;
            public const int Diameter1500 = 1500;
        }

        public static class SizeOctagon
        {
            public const int Octagon600 = 600;
            public const int Octagon750 = 750;
            public const int Octagon900 = 900;
            public const int Octagon1200 = 1200; 
        }

        public static class SizeTriangle
        {
            public const int Triangle600 = 600;
            public const int Triangle750 = 750;
            public const int Triangle900 = 900;
            public const int Triangle1200 = 1200;
            public const int Triangle1500 = 1500;
        }

        public static class Material
        {
            public const string Ruqani = "روغنی";
            public const string Galvanize = "گالوانیزه";
        }

         public static class Thikness
        {
            public const decimal Thikness125 = 1.25m;
            public const decimal Thikness150 = 1.5m;
            public const decimal Thikness200 = 2m;
        }

        public static class BoardTypes
        {
            public const string Simple = "ساده";
            public const string Edge = "لبه دار";
            public const string Edge3Dot = "رخ دار (دارای لبه داخلی)";
        }

        public static class PrintTypes
        {
            public const string EPG = "شبرنگ EPG (رده مهندسی)";
            public const string HIP = "شبرنگ HIP (پر بازتاب)";
            public const string POP = "شبرنگ POP (فلورسنت سبز زرد)";
        }

        public static class MaterialInfo
        {
            public const string VaraqGalvanize = "ورق گالوانیزه";
            public const string RailGalvanize = "ریل گالوانیزه";
            public const string VaraqRoqani = "ورق روغنی";
            public const string RailAlomenumi = "ریل آلومنیومی";
        }
        public static class ZakhamatVaraqInfo
        {
            public const decimal ZakhamatVaraq125 = 1.25m;
            public const decimal ZakhamatVaraq15 = 1.5m;
            public const decimal ZakhamatVaraq2 = 2;
            public const decimal ZakhamatVaraq3 = 3;
        }

        public static class PrintTypeInfo
        {
            public const string EPG = "شبرنگ EPG (رده مهندسی)";
            public const string HIP = "شبرنگ HIP (پر بازتاب)";
            public const string DIG = "شبرنگ DIG (رده الماس)";
            public const string POP = "شبرنگ POP (فلورسنت سبز زرد)";
        }

        public static class BoardStandType
        {
            public const string BSInfoKaftar = "پایه تابلو مسیر نما و بال کبوتری";
            public const string BSInfoCircle = "پایه تابلو دایره ای مثلثی";
            public const string BSInfoSmall = "پایه تابلو اطلاعاتی با سطح کوچک";
            public const string BSInfo = "پایه تابلو اطلاعاتی";
        }


    }
}
