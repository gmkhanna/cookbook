using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace NAME
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
        }
    }
}
