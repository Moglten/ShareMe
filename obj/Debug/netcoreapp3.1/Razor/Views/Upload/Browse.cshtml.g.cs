#pragma checksum "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f82089963407709b09c3b6f33dbcc6f37e7b5d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Upload_Browse), @"mvc.1.0.view", @"/Views/Upload/Browse.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using File_Sharing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using File_Sharing.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Rendering;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\muham\source\repos\File Sharing\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f82089963407709b09c3b6f33dbcc6f37e7b5d3", @"/Views/Upload/Browse.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6c132535462ba65dbdea26a417f59c9ce8e2005", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Upload_Browse : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UploadViewModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
  
    ViewData["Title"] = "Browse";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n<h1>Browse</h1>\r\n<hr />\r\n<br />\r\n\r\n\r\n\r\n<div class=\"container\">\r\n   <div class=\"row\"></div>\r\n         ");
#nullable restore
#line 16 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
    Write(await Component.InvokeAsync("UploadsList", new { Uploads = Model }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </br>\r\n        </br>\r\n        <div class=\"col-12 text-center\">\r\n            \r\n");
#nullable restore
#line 21 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
             if(@ViewBag.CurrentPage > 1){

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a");
            BeginWriteAttribute("href", " href=\"", 410, "\"", 472, 2);
            WriteAttributeValue("", 417, "/Upload/Browse/?RequiredPage=", 417, 29, true);
#nullable restore
#line 22 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
WriteAttributeValue("", 446, ViewBag.CurrentPage - 1, 446, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-outline-primary btn-lg algin-left\">Prev</a>\r\n");
#nullable restore
#line 23 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
            } 

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
             if(@ViewBag.CurrentPage < @ViewBag.TotalPages){

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a");
            BeginWriteAttribute("href", " href=\"", 630, "\"", 692, 2);
            WriteAttributeValue("", 637, "/Upload/Browse/?RequiredPage=", 637, 29, true);
#nullable restore
#line 25 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
WriteAttributeValue("", 666, ViewBag.CurrentPage + 1, 666, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-outline-primary btn-lg algin-right\">Next</a>\r\n");
#nullable restore
#line 26 "C:\Users\muham\source\repos\File Sharing\Views\Upload\Browse.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n<br />\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UploadViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
