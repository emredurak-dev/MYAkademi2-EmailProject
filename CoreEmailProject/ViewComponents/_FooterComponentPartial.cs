using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.ViewComponents
{
    public class _FooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
