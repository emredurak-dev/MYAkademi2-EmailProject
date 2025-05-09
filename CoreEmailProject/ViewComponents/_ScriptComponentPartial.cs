using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.ViewComponents
{
    public class _ScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
