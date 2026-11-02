using RMS.Controllers.BarAvordUser.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.BaseInfo.Dto;

public class ResultGetFosoulForBarAvordDto
{
    public List<GetFosoulOutputDto> Fosoul { get; set; }
    public List<clsVahed> lstVaheds { get; set; }
}

public class ResultGetFosoulForSooratVaziatDto
{
    public List<GetFosoulOutputDto> Fosoul { get; set; }
    public GetFosoulOutputDto CurrentFasl { get; set; }
    public List<clsVahed> lstVaheds { get; set; }
    public GetSumFosoulForSooratVaziatDto SumFosoulForSooratVaziat { get; set; }
}
