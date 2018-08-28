#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d9a4c26e022192ed7d681ca561e38dc280b56582"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Api_Game), @"mvc.1.0.view", @"/Views/Api/Game.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Api/Game.cshtml", typeof(AspNetCore.Views_Api_Game))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d9a4c26e022192ed7d681ca561e38dc280b56582", @"/Views/Api/Game.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Api_Game : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 223, true);
            WriteLiteral("<section class=\'game-details-container\'>\n\t<section class=\'linescore\'>\n\t\t<table class=\'linescore-table\'>\n\t\t\t<thead>\n\t\t\t\t<th class=\'linescore-table-header t-left\'>Team</th>\n\t\t\t\t<th class=\'linescore-table-header t-left\'></th>\n");
            EndContext();
#line 7 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                 for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(303, 64, true);
            WriteLiteral("                    <th class=\'linescore-table-header t-center\'>");
            EndContext();
            BeginContext(368, 9, false);
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                           Write(endNumber);

#line default
#line hidden
            EndContext();
            BeginContext(377, 6, true);
            WriteLiteral("</th>\n");
            EndContext();
#line 9 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                }				

#line default
#line hidden
            BeginContext(405, 145, true);
            WriteLiteral("\t\t\t\t<th class=\'linescore-table-header t-center\'>Tot</th>\n\t\t\t</thead>\n\t\t\t<tbody>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\'linescore-table-data t-left no-padding\'>");
            EndContext();
            BeginContext(551, 16, false);
#line 14 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                  Write(Model.Team1.Name);

#line default
#line hidden
            EndContext();
            BeginContext(567, 51, true);
            WriteLiteral("</td>\n\t\t\t\t\t<td class=\'linescore-table-data t-left\'>");
            EndContext();
            BeginContext(619, 93, false);
#line 15 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                       Write(Html.Raw(Model.Team1Hammer ? "<img class='linescore-hammer' src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(712, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 16 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                     for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(799, 48, true);
            WriteLiteral("\t\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(848, 161, false);
#line 17 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Html.Raw(endNumber < Model.CurrentEnd ? @Model.GetScoreForEnd(1, endNumber) : (Model.IsFinal ?  "<span class='faded'>X</span>" : "<span class='faded'>-</span>")));

#line default
#line hidden
            EndContext();
            BeginContext(1009, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 18 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                    }

#line default
#line hidden
            BeginContext(1037, 47, true);
            WriteLiteral("\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(1085, 16, false);
#line 19 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                         Write(Model.Team1Score);

#line default
#line hidden
            EndContext();
            BeginContext(1101, 90, true);
            WriteLiteral("</strong></td>\n\t\t\t\t</tr>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\'linescore-table-data t-left no-padding\'>");
            EndContext();
            BeginContext(1192, 16, false);
#line 22 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                  Write(Model.Team2.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1208, 51, true);
            WriteLiteral("</td>\n\t\t\t\t\t<td class=\'linescore-table-data t-left\'>");
            EndContext();
            BeginContext(1260, 93, false);
#line 23 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                       Write(Html.Raw(Model.Team2Hammer ? "<img class='linescore-hammer' src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(1353, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 24 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                     for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(1440, 48, true);
            WriteLiteral("\t\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(1489, 161, false);
#line 25 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Html.Raw(endNumber < Model.CurrentEnd ? @Model.GetScoreForEnd(2, endNumber) : (Model.IsFinal ?  "<span class='faded'>X</span>" : "<span class='faded'>-</span>")));

#line default
#line hidden
            EndContext();
            BeginContext(1650, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 26 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                    }

#line default
#line hidden
            BeginContext(1678, 55, true);
            WriteLiteral("\t\t\t\t\t<td class=\'linescore-table-data t-center\'><strong>");
            EndContext();
            BeginContext(1734, 16, false);
#line 27 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                 Write(Model.Team2Score);

#line default
#line hidden
            EndContext();
            BeginContext(1750, 141, true);
            WriteLiteral("</strong></td>\n\t\t\t\t</tr>\n\t\t\t</tbody>\n\t\t</table>\n\t</section>\n\t<section class=\'game-details-team-container\'>\n\t\t<div class=\'game-details-team\'>\n");
            EndContext();
#line 34 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
             if(@Model.PercentagesAvailable == true) { 
				
			} 
			else {

#line default
#line hidden
            BeginContext(1959, 8, true);
            WriteLiteral("\t\t\t\t<div");
            EndContext();
            BeginWriteAttribute("class", " class=\'", 1967, "\'", 2090, 2);
            WriteAttributeValue("", 1975, "game-team-details-team-container", 1975, 32, true);
#line 38 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue(" ", 2007, Model.Team1.TeamType == EventType.TeamType.MIXED_DOUBLES ? "mixed-doubles" : "", 2008, 82, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2091, 93, true);
            WriteLiteral(">\n\t\t\t\t\t<div class=\'game-team-details-team \'>\n\t\t\t\t\t\t<h3 class=\'game-team-details-header\'>Team ");
            EndContext();
            BeginContext(2185, 16, false);
#line 40 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Model.Team1.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2201, 61, true);
            WriteLiteral("</h3>\n\t\t\t\t\t\t<div class=\'game-team-details-player-container\'>\n");
            EndContext();
#line 42 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                             foreach (var player in @Model.Team1.GetPlayersSorted()) {

#line default
#line hidden
            BeginContext(2328, 96, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<div class=\'game-team-details-player\'>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-position\'>");
            EndContext();
            BeginContext(2425, 24, false);
#line 44 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                       Write(player.GetPositionName());

#line default
#line hidden
            EndContext();
            BeginContext(2449, 64, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-image\'><img");
            EndContext();
            BeginWriteAttribute("src", " src=\'", 2513, "\'", 2548, 2);
            WriteAttributeValue("", 2519, "/images/players/", 2519, 16, true);
#line 45 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue("", 2535, player.Image, 2535, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2549, 61, true);
            WriteLiteral("/></div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-name\'>");
            EndContext();
            BeginContext(2611, 20, false);
#line 46 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                          Write(player.GetFullName());

#line default
#line hidden
            EndContext();
            BeginContext(2631, 22, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t</div>\n");
            EndContext();
#line 48 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
							}

#line default
#line hidden
            BeginContext(2662, 122, true);
            WriteLiteral("\t\t\t\t\t\t</div>\n\t\t\t\t\t</div>\n\t\t\t\t\t\n\t\t\t\t\t<div class=\'game-team-details-team \'>\n\t\t\t\t\t\t<h3 class=\'game-team-details-header\'>Team ");
            EndContext();
            BeginContext(2785, 16, false);
#line 53 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Model.Team2.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2801, 61, true);
            WriteLiteral("</h3>\n\t\t\t\t\t\t<div class=\'game-team-details-player-container\'>\n");
            EndContext();
#line 55 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                             foreach (var player in @Model.Team2.GetPlayersSorted()) {

#line default
#line hidden
            BeginContext(2928, 96, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<div class=\'game-team-details-player\'>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-position\'>");
            EndContext();
            BeginContext(3025, 24, false);
#line 57 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                       Write(player.GetPositionName());

#line default
#line hidden
            EndContext();
            BeginContext(3049, 64, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-image\'><img");
            EndContext();
            BeginWriteAttribute("src", " src=\'", 3113, "\'", 3148, 2);
            WriteAttributeValue("", 3119, "/images/players/", 3119, 16, true);
#line 58 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue("", 3135, player.Image, 3135, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3149, 61, true);
            WriteLiteral("/></div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-name\'>");
            EndContext();
            BeginContext(3211, 20, false);
#line 59 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                          Write(player.GetFullName());

#line default
#line hidden
            EndContext();
            BeginContext(3231, 22, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t</div>\n");
            EndContext();
#line 61 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
							}

#line default
#line hidden
            BeginContext(3262, 43, true);
            WriteLiteral("\t\t\t\t\t\t</div>\t\t\t\t\t\t\t\n\t\t\t\t\t</div>\n\t\t\t\t</div>\n");
            EndContext();
#line 65 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
			}

#line default
#line hidden
            BeginContext(3310, 34, true);
            WriteLiteral("\t\t</div>\n\t\t\n\t</section>\n</section>");
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
