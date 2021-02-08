using DynamicDashboardDemo.Models;
using DynamicDashboardDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardInfoRepo _dashboardInfoRepository;
        public DashboardController(IDashboardInfoRepo dashboardInfoRepository, ILogger<DashboardController> logger)
        {
            _dashboardInfoRepository = dashboardInfoRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TemplateList()
        {
            return View("Templates/TemplateList");
        }

        public IActionResult ViewDashboard(int id)
        {
            try
            {
                DashboardsInfo dashboardInfo = _dashboardInfoRepository.GetDashboardInfo(id);
                int widgetsCount = _dashboardInfoRepository.GetWidgetsCount(dashboardInfo.TemplateId).Value;
                var linked_widgets = _dashboardInfoRepository.GetLinkedWidgets(id);
                for (int i = 1; i <= widgetsCount; i++)
                {
                    var widget = linked_widgets.Where(x => x.Placement == i.ToString());
                    if (widget.Any())
                    {
                        ViewData["Widget" + i] = "../Widgets/Widget" + widget.First().WidgetId + ".cshtml";
                    }
                    else
                    {
                        ViewData["Widget" + i] = "../Widgets/Default.cshtml";
                    }
                }
                ViewData["dashboardId"] = id;
                return View("Templates/Template" + dashboardInfo.TemplateId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new EmptyResult();
            }
        }

        public IActionResult WidgetList(int id)
        {
            ViewData["dashboardId"] = id;
            return View("Widgets/WidgetList");
        }

        public ActionResult GetDashboardsList()
        {
            return Ok(_dashboardInfoRepository.GetDashboardInfo());
        }

        public string CreateDashboard(DashboardsInfo dashboard)
        {
            try
            {
                return _dashboardInfoRepository.CreateDashboard(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "False";
            }
        }

        public string AddWidget(DashboardLinkedWidgets widget)
        {
            try
            {
                _dashboardInfoRepository.RemoveWidgets(widget);
                return _dashboardInfoRepository.AddWidget(widget).Equals(true) ? "True" : "False";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
        }
    }
}
