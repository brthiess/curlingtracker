#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48687a97b3f1a2237de52aa11c8c82a1fc9a4c37"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48687a97b3f1a2237de52aa11c8c82a1fc9a4c37", @"/Views/Api/Game.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Api_Game : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 37, true);
            WriteLiteral("<div class=\"game-details-back-button\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 37, "\"", 79, 3);
            WriteAttributeValue("", 47, "showCompetition(", 47, 16, true);
#line 1 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue("", 63, Model.EventId, 63, 14, false);

#line default
#line hidden
            WriteAttributeValue("", 77, ");", 77, 2, true);
            EndWriteAttribute();
            BeginContext(80, 231, true);
            WriteLiteral("></div>\n<section class=\'game-details-container\'>\n\t<section class=\'linescore\'>\n\t\t<table class=\'linescore-table\'>\n\t\t\t<thead>\n\t\t\t\t<th class=\'linescore-table-header t-left\'>Team</th>\n\t\t\t\t<th class=\'linescore-table-header t-left\'></th>\n");
            EndContext();
#line 8 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                 for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(391, 64, true);
            WriteLiteral("                    <th class=\'linescore-table-header t-center\'>");
            EndContext();
            BeginContext(456, 9, false);
#line 9 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                           Write(endNumber);

#line default
#line hidden
            EndContext();
            BeginContext(465, 6, true);
            WriteLiteral("</th>\n");
            EndContext();
#line 10 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                }				

#line default
#line hidden
            BeginContext(493, 145, true);
            WriteLiteral("\t\t\t\t<th class=\'linescore-table-header t-center\'>Tot</th>\n\t\t\t</thead>\n\t\t\t<tbody>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\'linescore-table-data t-left no-padding\'>");
            EndContext();
            BeginContext(639, 16, false);
#line 15 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                  Write(Model.Team1.Name);

#line default
#line hidden
            EndContext();
            BeginContext(655, 51, true);
            WriteLiteral("</td>\n\t\t\t\t\t<td class=\'linescore-table-data t-left\'>");
            EndContext();
            BeginContext(707, 93, false);
#line 16 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                       Write(Html.Raw(Model.Team1Hammer ? "<img class='linescore-hammer' src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(800, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 17 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                     for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(887, 48, true);
            WriteLiteral("\t\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(936, 109, false);
#line 18 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Html.Raw(endNumber < Model.CurrentEnd ? @Model.GetScoreForEnd(1, endNumber) : "<span class='faded'>X</span>"));

#line default
#line hidden
            EndContext();
            BeginContext(1045, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 19 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                    }

#line default
#line hidden
            BeginContext(1073, 47, true);
            WriteLiteral("\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(1121, 16, false);
#line 20 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                         Write(Model.Team1Score);

#line default
#line hidden
            EndContext();
            BeginContext(1137, 90, true);
            WriteLiteral("</strong></td>\n\t\t\t\t</tr>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\'linescore-table-data t-left no-padding\'>");
            EndContext();
            BeginContext(1228, 16, false);
#line 23 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                  Write(Model.Team2.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1244, 51, true);
            WriteLiteral("</td>\n\t\t\t\t\t<td class=\'linescore-table-data t-left\'>");
            EndContext();
            BeginContext(1296, 93, false);
#line 24 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                       Write(Html.Raw(Model.Team2Hammer ? "<img class='linescore-hammer' src='/images/hammer.png'/>" : ""));

#line default
#line hidden
            EndContext();
            BeginContext(1389, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 25 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                     for(var endNumber = 1; endNumber <= Model.GetNumberOfEnds(); endNumber++){

#line default
#line hidden
            BeginContext(1476, 48, true);
            WriteLiteral("\t\t\t\t\t\t<td class=\'linescore-table-data t-center\'>");
            EndContext();
            BeginContext(1525, 109, false);
#line 26 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Html.Raw(endNumber < Model.CurrentEnd ? @Model.GetScoreForEnd(2, endNumber) : "<span class='faded'>X</span>"));

#line default
#line hidden
            EndContext();
            BeginContext(1634, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 27 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                    }

#line default
#line hidden
            BeginContext(1662, 55, true);
            WriteLiteral("\t\t\t\t\t<td class=\'linescore-table-data t-center\'><strong>");
            EndContext();
            BeginContext(1718, 16, false);
#line 28 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                 Write(Model.Team2Score);

#line default
#line hidden
            EndContext();
            BeginContext(1734, 141, true);
            WriteLiteral("</strong></td>\n\t\t\t\t</tr>\n\t\t\t</tbody>\n\t\t</table>\n\t</section>\n\t<section class=\'game-details-team-container\'>\n\t\t<div class=\'game-details-team\'>\n");
            EndContext();
#line 35 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
             if(@Model.PercentagesAvailable == true) { 
				
			} 
			else {

#line default
#line hidden
            BeginContext(1943, 8, true);
            WriteLiteral("\t\t\t\t<div");
            EndContext();
            BeginWriteAttribute("class", " class=\'", 1951, "\'", 2074, 2);
            WriteAttributeValue("", 1959, "game-team-details-team-container", 1959, 32, true);
#line 39 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue(" ", 1991, Model.Team1.TeamType == EventType.TeamType.MIXED_DOUBLES ? "mixed-doubles" : "", 1992, 82, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2075, 93, true);
            WriteLiteral(">\n\t\t\t\t\t<div class=\'game-team-details-team \'>\n\t\t\t\t\t\t<h3 class=\'game-team-details-header\'>Team ");
            EndContext();
            BeginContext(2169, 16, false);
#line 41 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Model.Team1.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2185, 61, true);
            WriteLiteral("</h3>\n\t\t\t\t\t\t<div class=\'game-team-details-player-container\'>\n");
            EndContext();
#line 43 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                             foreach (var player in @Model.Team1.Players) {

#line default
#line hidden
            BeginContext(2301, 96, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<div class=\'game-team-details-player\'>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-position\'>");
            EndContext();
            BeginContext(2398, 24, false);
#line 45 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                       Write(player.GetPositionName());

#line default
#line hidden
            EndContext();
            BeginContext(2422, 64, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-image\'><img");
            EndContext();
            BeginWriteAttribute("src", " src=\'", 2486, "\'", 2521, 2);
            WriteAttributeValue("", 2492, "/images/players/", 2492, 16, true);
#line 46 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue("", 2508, player.Image, 2508, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2522, 61, true);
            WriteLiteral("/></div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-name\'>");
            EndContext();
            BeginContext(2584, 20, false);
#line 47 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                          Write(player.GetFullName());

#line default
#line hidden
            EndContext();
            BeginContext(2604, 22, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t</div>\n");
            EndContext();
#line 49 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
							}

#line default
#line hidden
            BeginContext(2635, 122, true);
            WriteLiteral("\t\t\t\t\t\t</div>\n\t\t\t\t\t</div>\n\t\t\t\t\t\n\t\t\t\t\t<div class=\'game-team-details-team \'>\n\t\t\t\t\t\t<h3 class=\'game-team-details-header\'>Team ");
            EndContext();
            BeginContext(2758, 16, false);
#line 54 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                             Write(Model.Team2.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2774, 61, true);
            WriteLiteral("</h3>\n\t\t\t\t\t\t<div class=\'game-team-details-player-container\'>\n");
            EndContext();
#line 56 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                             foreach (var player in @Model.Team2.Players) {

#line default
#line hidden
            BeginContext(2890, 96, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<div class=\'game-team-details-player\'>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-position\'>");
            EndContext();
            BeginContext(2987, 24, false);
#line 58 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                       Write(player.GetPositionName());

#line default
#line hidden
            EndContext();
            BeginContext(3011, 64, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-image\'><img");
            EndContext();
            BeginWriteAttribute("src", " src=\'", 3075, "\'", 3110, 2);
            WriteAttributeValue("", 3081, "/images/players/", 3081, 16, true);
#line 59 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
WriteAttributeValue("", 3097, player.Image, 3097, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3111, 61, true);
            WriteLiteral("/></div>\n\t\t\t\t\t\t\t\t\t<div class=\'game-team-details-player-name\'>");
            EndContext();
            BeginContext(3173, 20, false);
#line 60 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
                                                                          Write(player.GetFullName());

#line default
#line hidden
            EndContext();
            BeginContext(3193, 22, true);
            WriteLiteral("</div>\n\t\t\t\t\t\t\t\t</div>\n");
            EndContext();
#line 62 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
							}

#line default
#line hidden
            BeginContext(3224, 43, true);
            WriteLiteral("\t\t\t\t\t\t</div>\t\t\t\t\t\t\t\n\t\t\t\t\t</div>\n\t\t\t\t</div>\n");
            EndContext();
#line 66 "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Api/Game.cshtml"
			}

#line default
#line hidden
            BeginContext(3272, 34, true);
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
