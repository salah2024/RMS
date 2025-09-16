using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers.KhakRizi;

public class KhakRiziController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;


}
