using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.ViewComponents
{
    public class _HeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
