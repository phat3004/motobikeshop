#pragma checksum "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\Account\ResetPasswordConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4e0b693106264f2e0a143985f3002183a77f9857"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ResetPasswordConfirmation), @"mvc.1.0.view", @"/Views/Account/ResetPasswordConfirmation.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\_ViewImports.cshtml"
using _21_11_2021;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\_ViewImports.cshtml"
using _21_11_2021.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\_ViewImports.cshtml"
using _21_11_2021.Areas.admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e0b693106264f2e0a143985f3002183a77f9857", @"/Views/Account/ResetPasswordConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6aa0755f63519ac82cee51c5afd304c0dc2d7da", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ResetPasswordConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n\n\n");
            WriteLiteral(@"
<main class=""login-form"">
    <div class=""cotainer"">
        <div class=""row justify-content-center"">
            <div class=""col-md-8"">
                <div class=""card"">
                    <div class=""card-header"">Quên mật khẩu</div>
                    <div class=""card-body"">
                        <h1>Xác nhận đặt lại mật khẩu</h1>
                        <h2>
                            Mật khẩu của bạn đã được đặt lại.<a class=""btn btn-link"" onclick=""showInPopUp('/Account/Login','Đăng nhập')"" style=""color:blue;"">Đăng nhập</a>
                        </h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
</main>
<div class=""modal"" tabindex=""-1"" role=""dialog"" id=""form-modal"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span");
            WriteLiteral(" aria-hidden=\"true\">&times;</span>\n                </button>\n            </div>\n            <div class=\"modal-body\">\n            </div>\n        </div>\n    </div>\n</div>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n");
#nullable restore
#line 49 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\Account\ResetPasswordConfirmation.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
