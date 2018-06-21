#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27c3f1d1a24745445e5237ab4f4509819da70cd2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Partials_Scoreboard__ScoresGames), @"mvc.1.0.view", @"/Views/Partials/Scoreboard/_ScoresGames.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Partials/Scoreboard/_ScoresGames.cshtml", typeof(AspNetCore.Views_Partials_Scoreboard__ScoresGames))]
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
#line 1 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
using CurlingTracker.Utility;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27c3f1d1a24745445e5237ab4f4509819da70cd2", @"/Views/Partials/Scoreboard/_ScoresGames.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Partials_Scoreboard__ScoresGames : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(31, 5, false);
#line 2 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
Write(Model);

#line default
#line hidden
            EndContext();
            BeginContext(36, 42, true);
            WriteLiteral(" Event\n<div class=\'scores-games-wrapper\'>\n");
            EndContext();
#line 4 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
     if (Model.CurrentDraw != null) 
	{
		foreach(Game game in Model.CurrentDraw.Games)
		{

#line default
#line hidden
            BeginContext(167, 22, true);
            WriteLiteral("\t\t\t<div data-game-id=\'");
            EndContext();
            BeginContext(190, 11, false);
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                          Write(game.GameId);

#line default
#line hidden
            EndContext();
            BeginContext(201, 148, true);
            WriteLiteral("\' class=\'game-container\'>\n\t\t\t<div class=\'scores-teams-container\'>\n\t\t\t\t<div class=\'scores-team-container\'>\n\t\t\t\t\t<div class=\'scores-team-name\'>\n\t\t\t\t\t\t");
            EndContext();
            BeginContext(350, 19, false);
#line 12 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                   Write(game.Team1ShortName);

#line default
#line hidden
            EndContext();
            BeginContext(369, 27, true);
            WriteLiteral(" \n\t\t\t\t\t\t<span data-hammer1>");
            EndContext();
            BeginContext(397, 67, false);
#line 13 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                                      Write(Html.Raw(game.Team1Hammer ? "<img src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(464, 63, true);
            WriteLiteral("</span>\n\t\t\t\t\t</div>\n\t\t\t\t\t<div data-score1 class=\'scores-score\'>");
            EndContext();
            BeginContext(528, 15, false);
#line 15 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                                                     Write(game.Team1Score);

#line default
#line hidden
            EndContext();
            BeginContext(543, 100, true);
            WriteLiteral("</div>\n\t\t\t\t</div>\n\t\t\t\t<div class=\'scores-team-container\'>\n\t\t\t\t\t<div class=\'scores-team-name\'>\n\t\t\t\t\t\t");
            EndContext();
            BeginContext(644, 19, false);
#line 19 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                   Write(game.Team2ShortName);

#line default
#line hidden
            EndContext();
            BeginContext(663, 27, true);
            WriteLiteral(" \n\t\t\t\t\t\t<span data-hammer2>");
            EndContext();
            BeginContext(691, 67, false);
#line 20 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                                      Write(Html.Raw(game.Team2Hammer ? "<img src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(758, 63, true);
            WriteLiteral("</span>\n\t\t\t\t\t</div>\n\t\t\t\t\t<div data-score2 class=\'scores-score\'>");
            EndContext();
            BeginContext(822, 15, false);
#line 22 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
                                                     Write(game.Team2Score);

#line default
#line hidden
            EndContext();
            BeginContext(837, 44, true);
            WriteLiteral("</div>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t\t<div data-end");
            EndContext();
            BeginWriteAttribute("class", " class=\'", 881, "\'", 949, 2);
            WriteAttributeValue("", 889, "scores-end-container", 889, 20, true);
#line 25 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
WriteAttributeValue(" ", 909, game.IsFinal ? " scores-final " : "", 910, 39, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(950, 6, true);
            WriteLiteral(">\n\t\t\t\t");
            EndContext();
            BeginContext(958, 176, false);
#line 26 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
            Write(DateTime.Now < Model.CurrentDraw.Date ? Model.CurrentDraw.Date.ToString() : (game.IsFinal ? "Final (" + game.CurrentEnd + ")" : StringUtil.AddOrdinal(game.CurrentEnd) + " end"));

#line default
#line hidden
            EndContext();
            BeginContext(1135, 26, true);
            WriteLiteral("\n\t\t\t</div>\t\t\t\t\t\t\n\t\t</div>\n");
            EndContext();
#line 29 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Partials/Scoreboard/_ScoresGames.cshtml"
		}
	}

#line default
#line hidden
            BeginContext(1168, 11, true);
            WriteLiteral("\t\t\t\n\n</div>");
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
