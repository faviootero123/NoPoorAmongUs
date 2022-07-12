using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace SaucyCapstone.Services;

public class PdfService
{
    public IRazorViewEngine _razorEngine { get; }

    public PdfService(IRazorViewEngine razorEngine)
    {
        _razorEngine = razorEngine;
    }
    public async Task RenderPageToString(string pageName)
    {

    }

    public async Task<string> RenderToStringAsync(HttpContext httpContext, string pageName)
    {
        //This may not even work yet...
        //Using the razor engine to get html to turn into a pdf.
        ActionContext actionContext = new(httpContext, new RouteData(), new ActionDescriptor());
        RazorPageResult pageResult = _razorEngine.FindPage(actionContext,pageName);
        if (pageResult.Page == null)
        {
            throw new ArgumentException($"{pageName} does not match any available view.");
        }

        using var sw = new StringWriter();

        // add string writer to get the rendered html
        ViewContext viewContext = new(
            pageResult.Page.ViewContext,
            pageResult.Page.ViewContext.View,
            pageResult.Page.ViewContext.ViewData,
            sw);

        pageResult.Page.ViewContext = viewContext;

        await pageResult.Page.ExecuteAsync();
        return sw.ToString();
    }
}
