#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "180d7fcec399c155a7448b1edfb759a91f573944"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Partials_Scoreboard__Scores), @"mvc.1.0.view", @"/Views/Partials/Scoreboard/_Scores.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Partials/Scoreboard/_Scores.cshtml", typeof(AspNetCore.Views_Partials_Scoreboard__Scores))]
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
#line 1 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/_ViewImports.cshtml"
using CurlingTracker;

#line default
#line hidden
#line 2 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/_ViewImports.cshtml"
using CurlingTracker.Models;

#line default
#line hidden
#line 4 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/_ViewImports.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"180d7fcec399c155a7448b1edfb759a91f573944", @"/Views/Partials/Scoreboard/_Scores.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Partials_Scoreboard__Scores : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("game-time-name-option"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(18, 335, true);
            WriteLiteral(@"
<div class='scores-info-container active' data-scoreboard-section='scores'>
	<div class='scores'>
		<div data-draw-id='<%- competition.currentDrawId %>' class='games-container'>
			<div class='select-wrapper scores-draw-wrapper'>
				<select class='game-time-name' onchange=""updateDrawId(<%- competition.eventId %>, $(this).val());"">
");
            EndContext();
#line 7 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
                     for(var i = 0; i < Model.Draws.Count; i++) {

#line default
#line hidden
            BeginContext(404, 6, true);
            WriteLiteral("\t\t\t\t\t\t");
            EndContext();
            BeginContext(410, 207, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7176df921f2e4784822b323824931ec0", async() => {
                BeginContext(479, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(481, 97, false);
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
                                                                                         Write(Model.Draws[i].Date.ToString("MMMM dd h:mmtt", System.Globalization.CultureInfo.InvariantCulture));

#line default
#line hidden
                EndContext();
                BeginContext(578, 3, true);
                WriteLiteral(" - ");
                EndContext();
                BeginContext(582, 26, false);
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
                                                                                                                                                                                              Write(Model.Draws[i].DisplayName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
                           WriteLiteral(Model.Draws[i].DrawId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(617, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 9 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
					}  

#line default
#line hidden
            BeginContext(627, 25, true);
            WriteLiteral("\t\t\t\t\t</select>\n\t\t\t</div>\n");
            EndContext();
#line 12 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
              await Html.RenderPartialAsync("../Scoreboard/_ScoresGames.cshtml");

#line default
#line hidden
            BeginContext(726, 51, true);
            WriteLiteral("\t\t</div>\n\t</div>\n</div>\n<script>\n\tcurrentDrawId = \"");
            EndContext();
            BeginContext(779, 57, false);
#line 17 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Scores.cshtml"
                 Write(Model.CurrentDraw != null ? Model.CurrentDraw.DrawId : "");

#line default
#line hidden
            EndContext();
            BeginContext(837, 12, true);
            WriteLiteral("\";\n</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Configuration { get; private set; }
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
