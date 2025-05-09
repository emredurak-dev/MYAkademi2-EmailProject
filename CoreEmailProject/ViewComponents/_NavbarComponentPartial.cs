using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.ViewComponents
{
    public class _NavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
