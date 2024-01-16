using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Backend_Project_Amado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly AppDbContext _appDbContext;
        public HomeController(IMailService mailService, AppDbContext appDbContext)
        {
            _mailService = mailService;
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(MailSenderVM model)
        {
            if(!ModelState.IsValid) return View(model);

            var receivers = _appDbContext.Subscribers.ToList();
            
            var subject = model.Subject;
            var body = model.Body;

            foreach(var receiver in receivers)
            {
                await _mailService.SendEmailAsync(receiver.EmailAddress, subject, body);
            }

            return RedirectToAction("Index");
        }
        
    }
}
