#pragma checksum "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c06f13a6ffc9d223f416864c7b60c6e05d224942"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TinTuc_Index), @"mvc.1.0.view", @"/Views/TinTuc/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c06f13a6ffc9d223f416864c7b60c6e05d224942", @"/Views/TinTuc/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6aa0755f63519ac82cee51c5afd304c0dc2d7da", @"/Views/_ViewImports.cshtml")]
    public class Views_TinTuc_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <!-- Start All Title Box -->
<div class=""all-title-box"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <h2>Tin tức</h2>
                <ul class=""breadcrumb"">
                    <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                    <li class=""breadcrumb-item active"">NEWS</li>
                </ul>
            </div>
        </div>
    </div>
</div>
<!-- End All Title Box -->
<!-- Start About Page  -->
<div class=""about-box-main"">
    <div class=""container"">
");
#nullable restore
#line 19 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml"
         foreach (var item in ViewBag.TinTuc)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\n                <div class=\"col-lg-6\">\n                    <h2 class=\"noo-sh-title\">");
#nullable restore
#line 23 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml"
                                        Write(item.TenTinTuc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n                    <p>\n                        ");
#nullable restore
#line 25 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml"
                   Write(Html.Raw(item.ChiTiet));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </p>\n                </div>\n                <div class=\"col-lg-6\">\n                    <div class=\"banner-frame\">\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c06f13a6ffc9d223f416864c7b60c6e05d2249425881", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1011, "~/tintuc/", 1011, 9, true);
#nullable restore
#line 30 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml"
AddHtmlAttributeValue("", 1020, item.Hinh, 1020, 10, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                    </div>\n                </div>\n            </div>\n");
#nullable restore
#line 34 "C:\Users\N.H.M.Phat\OneDrive\Desktop\Final_véion\New_Version\MotoBike_Shop\21-11-2021\Views\TinTuc\Index.cshtml"
        }       

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n</div>\n<!-- End About Page -->");
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
