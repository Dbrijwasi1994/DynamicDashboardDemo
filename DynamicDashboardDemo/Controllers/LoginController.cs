using DNTCaptcha.Core;
using DynamicDashboardDemo.Models;
using DynamicDashboardDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginInfoRepo _loginInfo;
        private readonly DNTCaptchaOptions _captchaOptions;
        private readonly IDNTCaptchaValidatorService _validatorService;
        public LoginController(IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> captchaOptions, ILoginInfoRepo loginInfo, ILogger<LoginController> logger)
        {
            _validatorService = validatorService;
            _captchaOptions = captchaOptions == null ? throw new ArgumentNullException(nameof(captchaOptions)) : captchaOptions.Value; ;
            _loginInfo = loginInfo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Login";
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(ErrorMessage = "Please Enter Valid Captcha.", CaptchaGeneratorLanguage = Language.English, CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public IActionResult Login([FromForm] LoginViewModel loginViewModel)
        {
            ViewData["Title"] = "Login";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {
                        this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Please Enter Valid Captcha.");
                        return View("Login");
                    }
                    if (!_loginInfo.IsValidUser(loginViewModel.Username, loginViewModel.Password))
                    {
                        this.ModelState.AddModelError("ModelOnly", "Username or Password is Invalid.");
                        return View("Login");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Login");
            }
        }
    }
}
