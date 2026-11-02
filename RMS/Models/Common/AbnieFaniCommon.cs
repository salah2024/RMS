using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommon(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;



}
