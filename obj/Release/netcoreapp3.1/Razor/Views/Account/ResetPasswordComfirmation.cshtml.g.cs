#pragma checksum "C:\Users\muham\source\repos\File Sharing\Views\Account\ResetPasswordComfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f049deb94a032bc66104ff55ec6d6e200ed55efc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ResetPasswordComfirmation), @"mvc.1.0.view", @"/Views/Account/ResetPasswordComfirmation.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f049deb94a032bc66104ff55ec6d6e200ed55efc", @"/Views/Account/ResetPasswordComfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6c132535462ba65dbdea26a417f59c9ce8e2005", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Account_ResetPasswordComfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<br />\r\n<br />\r\n<h2 class=\"text-center\">Forget Password for your Email</h2>\r\n<br />\r\n<br />\r\n<p class=\"text-center\">\r\n    <strong>Please click that Link to reset your password :</strong>\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 194, "\"", 231, 1);
#nullable restore
#line 8 "C:\Users\muham\source\repos\File Sharing\Views\Account\ResetPasswordComfirmation.cshtml"
WriteAttributeValue("", 201, ViewBag.confirmationResetLink, 201, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> CLICK HERE</a>\r\n</p>\r\n<br />\r\n<h4 class=\"text-center\">Thank you for using FileSharing</h2>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
