#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e4628daa6d960cbfaa9d5ef9790c8a5f590b65fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Partials_Scoreboard__Draw), @"mvc.1.0.view", @"/Views/Partials/Scoreboard/_Draw.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Partials/Scoreboard/_Draw.cshtml", typeof(AspNetCore.Views_Partials_Scoreboard__Draw))]
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
#line 1 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
using CurlingTracker.Utility;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4628daa6d960cbfaa9d5ef9790c8a5f590b65fc", @"/Views/Partials/Scoreboard/_Draw.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Partials_Scoreboard__Draw : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(47, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 4 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
 foreach(Game game in Model.Games)
{

#line default
#line hidden
            BeginContext(110, 23, true);
            WriteLiteral("    <div data-game-id=\'");
            EndContext();
            BeginContext(134, 11, false);
#line 6 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
                  Write(game.GameId);

#line default
#line hidden
            EndContext();
            BeginContext(145, 24, true);
            WriteLiteral("\' class=\'game-container\'");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 169, "\"", 227, 6);
            WriteAttributeValue("", 179, "showGame(\'", 179, 10, true);
#line 6 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
WriteAttributeValue("", 189, game.GameId, 189, 12, false);

#line default
#line hidden
            WriteAttributeValue("", 201, "\',", 201, 2, true);
            WriteAttributeValue(" ", 203, "\'", 204, 2, true);
#line 6 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
WriteAttributeValue("", 205, ViewData["EventId"], 205, 20, false);

#line default
#line hidden
            WriteAttributeValue("", 225, "\')", 225, 2, true);
            EndWriteAttribute();
            BeginContext(228, 146, true);
            WriteLiteral(">\n    <div class=\'scores-teams-container\'>\n        <div class=\'scores-team-container\'>\n            <div class=\'scores-team-name\'>\n                ");
            EndContext();
            BeginContext(375, 19, false);
#line 10 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
           Write(game.Team1ShortName);

#line default
#line hidden
            EndContext();
            BeginContext(394, 129, true);
            WriteLiteral(" \n            </div>\n            <div data-score1 class=\'scores-score\'>\n                <span class=\'scores-hammer\' data-hammer1>");
            EndContext();
            BeginContext(524, 67, false);
#line 13 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
                                                    Write(Html.Raw(game.Team1Hammer ? "<img src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(591, 24, true);
            WriteLiteral("</span>\n                ");
            EndContext();
            BeginContext(616, 15, false);
#line 14 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
           Write(game.Team1Score);

#line default
#line hidden
            EndContext();
            BeginContext(631, 138, true);
            WriteLiteral("\n            </div>\n        </div>\n        <div class=\'scores-team-container\'>\n            <div class=\'scores-team-name\'>\n                ");
            EndContext();
            BeginContext(770, 19, false);
#line 19 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
           Write(game.Team2ShortName);

#line default
#line hidden
            EndContext();
            BeginContext(789, 129, true);
            WriteLiteral(" \n            </div>\n            <div data-score2 class=\'scores-score\'>\n                <span class=\'scores-hammer\' data-hammer2>");
            EndContext();
            BeginContext(919, 67, false);
#line 22 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
                                                    Write(Html.Raw(game.Team2Hammer ? "<img src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(986, 24, true);
            WriteLiteral("</span>\n                ");
            EndContext();
            BeginContext(1011, 15, false);
#line 23 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
           Write(game.Team2Score);

#line default
#line hidden
            EndContext();
            BeginContext(1026, 63, true);
            WriteLiteral("\n            </div>\n        </div>\n    </div>\n    <div data-end");
            EndContext();
            BeginWriteAttribute("class", " class=\'", 1089, "\'", 1157, 2);
            WriteAttributeValue("", 1097, "scores-end-container", 1097, 20, true);
#line 27 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
WriteAttributeValue(" ", 1117, game.IsFinal ? " scores-final " : "", 1118, 39, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1158, 10, true);
            WriteLiteral(">\n        ");
            EndContext();
            BeginContext(1170, 152, false);
#line 28 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
    Write(DateTime.Now < Model.Date ? Model.Date.ToString() : (game.IsFinal ? "Final (" + game.CurrentEnd + ")" : StringUtil.AddOrdinal(game.CurrentEnd) + " end"));

#line default
#line hidden
            EndContext();
            BeginContext(1323, 25, true);
            WriteLiteral("\n    </div>\t\t\t\t\t\t\n</div>\n");
            EndContext();
#line 31 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_Draw.cshtml"
}

#line default
#line hidden
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
