#pragma checksum "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fffc6fce83ecc38e534ce69dd35aa9404177cd02"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation_Index_Backup2), @"mvc.1.0.view", @"/Views/Reservation/Index_Backup2.cshtml")]
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
#line 1 "C:\Users\mathe\ReservationSytem\T2RMS\Views\_ViewImports.cshtml"
using T2RMSWS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mathe\ReservationSytem\T2RMS\Views\_ViewImports.cshtml"
using T2RMSWS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fffc6fce83ecc38e534ce69dd35aa9404177cd02", @"/Views/Reservation/Index_Backup2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cec448861e4b4da730b5bc93f8f029eb568b7f1d", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation_Index_Backup2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/crud/bstable.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"
  
    ViewData["Title"] = "Reservation";

    var date = DateTime.Now;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Reservation</h1>\r\n\r\n<!--\r\n<ul>\r\n    &at foreach (var item in ViewBag.Reservations)\r\n    {\r\n        <li>&atitem</li>\r\n    }\r\n</ul>\r\n    -->\r\n");
#nullable restore
#line 18 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"
 if (User.IsInRole("Manager") || User.IsInRole("Staff"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div>
        <table class=""table table-striped table-bordered"" id=""reservationTable"">
            <thead class=""thead-dark"">
                <tr>
                    <th scope=""col"">#</th>
                    <th scope=""col"">First Name</th>
                    <th scope=""col"">Last Name</th>
                    <th scope=""col"">Number of Guests</th>
                    <th scope=""col"">Time of Reservation</th>


                </tr>
            </thead>
            <tbody>
                <tr class=""reservationList"">
                    <td scope=""row"">1</td>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>2</td>
                    <td>");
#nullable restore
#line 39 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"
                   Write(date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                </tr>\r\n                <tr class=\"reservationList\">\r\n                    <td scope=\"row\">2</td>\r\n                    <td>Jacob</td>\r\n                    <td>Thornton</td>\r\n                    <td>2</td>\r\n                    <td>");
#nullable restore
#line 47 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"
                   Write(date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                </tr>\r\n                <tr class=\"reservationList\">\r\n                    <td scope=\"row\">3</td>\r\n                    <td>Larry</td>\r\n                    <td>Smith</td>\r\n                    <td>2</td>\r\n                    <td>");
#nullable restore
#line 55 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"
                   Write(date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n\r\n\r\n        <button id=\"new-row-button\" class=\"btn btn-dark\" onclick=\"addNewRow()\">Add New Reservation</button>\r\n\r\n\r\n        <button id=\"save-button\" class=\"btn btn-dark\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1759, "\"", 1769, 0);
            EndWriteAttribute();
            WriteLiteral(">Save Changes</button>\r\n\r\n    </div>\r\n");
#nullable restore
#line 68 "C:\Users\mathe\ReservationSytem\T2RMS\Views\Reservation\Index_Backup2.cshtml"


}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fffc6fce83ecc38e534ce69dd35aa9404177cd026857", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <script>


        function addNewRow() {
            console.log(""ASD"");
            if (document.getElementsByClassName(""reservationList"").length == 0) {
                var table = document.getElementById(""reservationTable"").getElementsByTagName('tbody')[0];

                editableTable.destroy();
                table.innerHTML = '<tr class=""reservationList""><td scope=""row""></td><td></td><td></td></tr>';
                editableTable.init();


            }

        }

    </script>


    <script>

        var editableTable = new BSTable(""reservationTable"", {
            $addButton: $('#new-row-button'), advanced: {
                columnLabel: 'Actions',
                buttonHTML: `<div class=""btn-group pull-right"">
                            <button id=""bEdit"" type=""button"" class="""">
                              <span class=""fa fa-edit"" > </span>
                            </button>
                            <button id=""bDel"" type=""button"" class="""">
        ");
                WriteLiteral(@"                      <span class=""fa fa-trash"" > </span>
                            </button>
                            <button id=""bAcep"" type=""button"" class=""btn btn-sm btn-default"" style=""display:none;"">
                              <span class=""fa fa-check-circle"" > </span>
                            </button>
                            <button id=""bCanc"" type=""button"" class=""btn btn-sm btn-default"" style=""display:none;"">
                              <span class=""fa fa-times-circle"" > </span>
                            </button>
                          </div>`
            }
        });

        editableTable.init();


    </script>

");
            }
            );
            WriteLiteral("\r\n");
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
