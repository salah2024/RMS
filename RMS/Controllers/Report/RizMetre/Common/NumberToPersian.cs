namespace RMS.Controllers.Report.RizMetre.Common;

public static class NumberToPersian
{
    private static readonly string[] Units =
    {
        "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه",
        "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده",
        "هفده", "هجده", "نوزده"
    };

    private static readonly string[] Tens =
    {
        "", "", "بیست", "سی", "چهل", "پنجاه",
        "شصت", "هفتاد", "هشتاد", "نود"
    };

    private static readonly string[] Hundreds =
    {
        "", "یکصد", "دویست", "سیصد", "چهارصد",
        "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد"
    };

    private static readonly string[] Groups =
    {
        "", "هزار", "میلیون", "میلیارد", "تریلیون"
    };

    public static string ToWords(long number)
    {
        if (number == 0)
            return "صفر ریال";

        if (number < 0)
            return "منفی " + ToWords(-number);

        List<string> parts = new List<string>();

        int group = 0;

        while (number > 0)
        {
            int threeDigits = (int)(number % 1000);

            if (threeDigits != 0)
            {
                string text = ThreeDigitToWords(threeDigits);

                if (!string.IsNullOrEmpty(Groups[group]))
                    text += " " + Groups[group];

                parts.Insert(0, text);
            }

            number /= 1000;
            group++;
        }

        return string.Join(" و ", parts) + " ریال";
    }

    private static string ThreeDigitToWords(int number)
    {
        List<string> parts = new List<string>();

        if (number >= 100)
        {
            parts.Add(Hundreds[number / 100]);
            number %= 100;
        }

        if (number >= 20)
        {
            parts.Add(Tens[number / 10]);
            if (number % 10 > 0)
                parts.Add(Units[number % 10]);
        }
        else if (number > 0)
        {
            parts.Add(Units[number]);
        }

        return string.Join(" و ", parts);
    }
}