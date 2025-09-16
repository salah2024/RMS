using RMS.Models.Entity;

namespace RMS.Models.Common;

public class AbnieFaniEzafeBahaCommon(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;
    public string BotonDivarKooleEzafeBahaLow5(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن دیوار کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = (decimal.Parse(b1.Trim()) + decimal.Parse(b2.Trim())) / 2;
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "42";///اضافه بها بتن دیوار کوله
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();
        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonDivarKooleEzafeBahaHigh5(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h, Guid FBId120302, Guid FBId120303)
    {
        decimal X = (decimal.Parse(b1.Trim()) - decimal.Parse(b2.Trim()))
                        * ((decimal.Parse(h.Trim()) - decimal.Parse("5.3")) / (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
        ////////////////اضافه بها بتن دیوار کوله
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن دیوار کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = (2 * decimal.Parse(b1.Trim()) + X) / 2;
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("5.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "42";///اضافه بها بتن دیوار کوله
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        /////////////////قسمت دوم 5 متر اول
        ////////////////اضافه بها بتن دیوار کوله
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن دیوار کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = (decimal.Parse(b1.Trim()) + X + decimal.Parse(b2.Trim())) / 2;
        RizMetre.Ertefa = decimal.Parse("5.0");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "42";///اضافه بها بتن دیوار کوله
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();
        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    public string BotonDivarMakhrootiEzafeBahaLow5(Guid PolVaAbroId, int PolNum, string D, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId
       , string p1, string p2, decimal x, string TedadDahaneh, int tedad)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        ////////////////اضافه بها بتن مخروط پایه میانی (کامل)
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن مخروط پایه میانی (کامل)";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p1.Trim()) * decimal.Parse(p1.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = decimal.Parse(h.Trim()) + x;
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "48";///اضافه بها بتن مخروط پایه میانی (کامل)
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////
        if (x != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.Sharh = "اضافه بها کسر بتن مخروط پایه میانی(بالایی)";
            RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
            RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
            RizMetre.Arz = decimal.Parse(Math.PI.ToString());
            RizMetre.Ertefa = (-1) * x;
            RizMetre.Vazn = 0;
            RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = FBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "49";///اضافه بها کسر بتن مخروط پایه میانی(بالایی)
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            //RizMetre.Save();
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
        }
        return (++Shomareh).ToString();
    }

    public string BotonDivarMakhrootiEzafeBahaHigh5(Guid PolVaAbroId, int PolNum, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120302, Guid FBId120303, string p1, string p2, decimal x, string TedadDahaneh, int tedad)
    {
        decimal dPSefr = 1 + (((decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())) * (decimal.Parse(h.Trim()) - decimal.Parse("5.3"))))
            / (decimal.Parse(p1.Trim()) * (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
        ////////////////اضافه بها بتن مخروط پایه میانی (کامل)
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن مخروط پایه میانی (کامل)";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
        RizMetre.Tool = (dPSefr * dPSefr) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = (decimal.Parse(p2.Trim()) / (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())))
                                * (decimal.Parse(h.Trim()) - decimal.Parse("0.3")) + (decimal.Parse(h.Trim()) - decimal.Parse("5.3"));
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "48";///اضافه بها بتن مخروط پایه میانی (کامل)
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh120303;
        RizMetre.Sharh = "اضافه بها کسر بتن مخروط پایه میانی(بالایی)";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = (-1) * (decimal.Parse(h.Trim()) - decimal.Parse("0.3")) * (decimal.Parse(p2.Trim()) / (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())));
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "49";///اضافه بها کسر بتن مخروط پایه میانی(بالایی)
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        /////////////////قسمت دوم 5 متر اول
        ////////////////اضافه بهای مخروطی پایه میانی
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن مخروط پایه میانی (کامل)";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p1.Trim()) * decimal.Parse(p1.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = (decimal.Parse(h.Trim()) - decimal.Parse("0.3")) *
            (decimal.Parse(p1.Trim()) / (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())));
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "48";///اضافه بها بتن مخروط پایه میانی (کامل)
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh120302;
        RizMetre.Sharh = "اضافه بها کسر بتن مخروط پایه میانی(بالایی)";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
        RizMetre.Tool = (dPSefr * dPSefr) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = (-1) * (decimal.Parse(p2.Trim()) / (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())))
                                * (decimal.Parse(h.Trim()) - decimal.Parse("0.3")) + (decimal.Parse(h.Trim()) - decimal.Parse("5.3"));
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "49";///اضافه بها کسر بتن مخروط پایه میانی(بالایی)
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    /// <summary>
    /// //اضافه بها بتن دیوار پایه میانی
    /// </summary>
    public string BotonDivarPayeMianiEzafeBahaLow5(Guid PolVaAbroId, int PolNum, string D, long Shomareh, string LAbro, string b1, string b2, string h
        , Guid FBId, string p1, string p2, string TedadDahaneh)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن دیوار پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1);
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = (decimal.Parse(p1.Trim()) + decimal.Parse(p2.Trim())) / 2;
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "43";///اضافه بها بتن دیوار پایه میانی
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();
        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonDivarPayeMianiEzafeBahaHigh5(Guid PolVaAbroId, int PolNum, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120302, Guid FBId120303, string p1, string p2, string TedadDahaneh)
    {
        decimal X = ((decimal.Parse(h.Trim()) - decimal.Parse("5.3")) / (decimal.Parse(h.Trim()) - decimal.Parse("0.3")))
                    * (2 * (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim())));
        ////////////////اضافه بها بتن دیوار پایه میانی
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن دیوار پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1);
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(p2.Trim()) + X;
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("5.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "43";///اضافه بها بتن دیوار پایه میانی
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        /////////////////قسمت دوم 5 متر اول
        ////////////////اضافه بها بتن دیوار پایه میانی
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن دیوار پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1);
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = (decimal.Parse(p2.Trim()) + decimal.Parse(p1.Trim()) + (2 * X)) / 2;
        RizMetre.Ertefa = decimal.Parse("5");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "43";///اضافه بها بتن دیوار پایه میانی
        RizMetre.ForItem = "120104";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    /// <summary>
    /// //اضافه بها بتن شناژ کوله
    /// </summary>
    public string BotonShenajKooleEzafeBahaState1(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId, string c1, string t)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse(t);
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonShenajKooleEzafeBahaState2(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh, string LAbro, string b1, string b2, string h,
        Guid FBId, string c1, string t)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse(t);
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonShenajKooleEzafeBahaState3(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h,
                         Guid FBId120302, Guid FBId120303, string c1, string t)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse("5.3") - decimal.Parse(h.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        //////////////////////////
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("5");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse(t);
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    public string BotonShenajKooleEzafeBahaState4(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h,
                         Guid FBId120302, Guid FBId120303, string c1, string t)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        //////////////////////////
        RizMetre.Shomareh = ++Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse("5") - decimal.Parse(h.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        //////////////////////////
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) - decimal.Parse("5");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "44";///اضافه بها بتن شناژ کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        // RizMetre.Save();
        ////////////
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }
    /// <summary>
    /// //اضافه بها بتن شناژ پایه میانی
    /// </summary>
    public string BotonShenajPayeMianiEzafeBahaState1(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId
        , string p2, int tedad)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بها بتن شناژ پایه میانی";
        RizMetre.Tedad = tedad;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(p2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "45";///اضافه بها بتن شناژ پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonShenajPayeMianiEzafeBahaState2(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120302, Guid FBId120303, string p2, int tedad)
    {
        ///////////////// 5 متر اول
        ////////////////اضافه بها بتن شناژ پایه میانی
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بها بتن دیوار پایه میانی";
        RizMetre.Tedad = tedad;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(p2.Trim());
        RizMetre.Ertefa = decimal.Parse("5.3") - decimal.Parse(h.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "45";///اضافه بها بتن شناژ پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////////اضافه بها بتن شناژ پایه میانی
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن شناژ پایه میانی";
        RizMetre.Tedad = tedad;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(p2.Trim());
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("5");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "45";///اضافه بها بتن شناژ پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    public string BotonShenajPayeMianiEzafeBahaState3(Guid PolVaAbroId, int PolNum, string TedadDahaneh, string D, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120303, string p2, int tedad)
    {
        ///////////////// 5 متر اول
        ////////////////اضافه بها بتن شناژ پایه میانی
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بها بتن دیوار پایه میانی";
        RizMetre.Tedad = tedad;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(p2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "45";///اضافه بها بتن شناژ پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120303).ToString();
    }

    public string BotonShenajMakhrootiEzafeBahaState1(Guid PolVaAbroId, int PolNum, string D, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId
    , string p2, string TedadDahaneh, int tedad)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        ////////////////اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بهای بتن شناژ مخروطی پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "46";///اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonShenajMakhrootiEzafeBahaState2(Guid PolVaAbroId, int PolNum, string D, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120302, Guid FBId120303, string p2, string TedadDahaneh, int tedad)
    {
        //////////////// 5 متر اول
        ////////////////اضافه بهای بتن شناژ مخروطی پایه میانی
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بهای بتن شناژ مخروطی پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = decimal.Parse("5.3") - decimal.Parse(h.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "46";///اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        ////////////////اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بهای بتن شناژ مخروطی پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("5");
        RizMetre.Vazn = 0;
        RizMetre.Des = "";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "46";///اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }
    public string BotonShenajMakhrootiEzafeBahaState3(Guid PolVaAbroId, int PolNum, string D, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120303, string p2, string TedadDahaneh, int tedad)
    {
        ////////////////اضافه بهای بتن شناژ مخروطی پایه میانی
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بهای بتن شناژ مخروطی پایه میانی";
        RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "46";///اضافه بهای بتن شناژ مخروطی پایه میانی
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120303).ToString();
    }

    /// <summary>
    /// اضافه بهای بتن دال
    /// </summary>
    public string BotonDalEzafeBahaState1(Guid PolVaAbroId, int PolNum, long Shomareh, string LAbro, string b1, string b2, string h, Guid FBId
        , string c1, string j, string D, string t, string TedadDahaneh)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        ////////////////اضافه بهای بتن دال
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = "اضافه بهای بتن دال";
        RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
        RizMetre.Ertefa = decimal.Parse(t.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "47";///اضافه بهای بتن دال
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh).ToString();
    }

    public string BotonDalEzafeBahaState2(Guid PolVaAbroId, int PolNum, long Shomareh120302, long Shomareh120303, string LAbro, string b1, string b2, string h
        , Guid FBId120302, Guid FBId120303, string c1, string j, string D, string t, string TedadDahaneh)
    {
        ///////////////// 5 متر اول
        ////////////////اضافه بهای بتن دال
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120302;
        RizMetre.Sharh = "اضافه بهای بتن دال";
        RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
        RizMetre.Ertefa = decimal.Parse(t.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120302;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "47";///اضافه بهای بتن دال
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        //RizMetre.Save();
        ////////////////اضافه بهای بتن دال
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بهای بتن دال";
        RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
        RizMetre.Ertefa = decimal.Parse(t.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "47";///اضافه بهای بتن دال
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120302).ToString() + "" + (++Shomareh120303).ToString();
    }

    public string BotonDalEzafeBahaState3(Guid PolVaAbroId, int PolNum, long Shomareh120303, string LAbro, string b1, string b2, string h, Guid FBId120303
                , string c1, string j, string D, string t, string TedadDahaneh)
    {
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        ////////////////اضافه بهای بتن دال
        RizMetre.Shomareh = Shomareh120303;
        RizMetre.Sharh = "اضافه بهای بتن دال";
        RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
        RizMetre.Ertefa = decimal.Parse(t.Trim());
        RizMetre.Vazn = 0;
        RizMetre.Des = RizMetre.Des = RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = FBId120303;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "47";///اضافه بهای بتن دال
        RizMetre.ForItem = "120106";
        RizMetre.UseItem = "";
        _context.RizMetreUserses.Add(RizMetre);
        _context.SaveChanges();

        //RizMetre.Save();
        return (++Shomareh120303).ToString();
    }

}
