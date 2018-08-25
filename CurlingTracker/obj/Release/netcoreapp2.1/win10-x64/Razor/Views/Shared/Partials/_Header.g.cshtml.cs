#pragma checksum "/home/brad/Documents/websites/CurlingTracker/CurlingTracker/Views/Shared/Partials/_Header.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "72872fad95c2cbb026be2154af217d9884f256dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Partials__Header), @"mvc.1.0.view", @"/Views/Shared/Partials/_Header.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Partials/_Header.cshtml", typeof(AspNetCore.Views_Shared_Partials__Header))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72872fad95c2cbb026be2154af217d9884f256dd", @"/Views/Shared/Partials/_Header.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baa2462f238fa76c3bdf3f6aa62851e23506edf", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Partials__Header : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1, true);
            WriteLiteral("\t");
            EndContext();
            BeginContext(1, 1741, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e02ca53ac51c46959bd858c5d1a00a68", async() => {
                BeginContext(7, 1728, true);
                WriteLiteral(@"
		<title>Curling Scores</title>
		<meta name=""viewport"" content=""width=device-width"">
		<link rel=""shortcut icon"" href=""/images/favicon.png""/>
		<link rel=""stylesheet"" href=""/css/style.css""/>
		<script src=""https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js""></script>
		<link rel=""apple-touch-icon"" sizes=""57x57"" href=""/images/favicons/apple-icon-57x57.png"">
		<link rel=""apple-touch-icon"" sizes=""60x60"" href=""/images/favicons/apple-icon-60x60.png"">
		<link rel=""apple-touch-icon"" sizes=""72x72"" href=""/images/favicons/apple-icon-72x72.png"">
		<link rel=""apple-touch-icon"" sizes=""76x76"" href=""/images/favicons/apple-icon-76x76.png"">
		<link rel=""apple-touch-icon"" sizes=""114x114"" href=""/images/favicons/apple-icon-114x114.png"">
		<link rel=""apple-touch-icon"" sizes=""120x120"" href=""/images/favicons/apple-icon-120x120.png"">
		<link rel=""apple-touch-icon"" sizes=""144x144"" href=""/images/favicons/apple-icon-144x144.png"">
		<link rel=""apple-touch-icon"" sizes=""152x152"" href=""/images/favicons/apple-icon-152x152.p");
                WriteLiteral(@"ng"">
		<link rel=""apple-touch-icon"" sizes=""180x180"" href=""/images/favicons/apple-icon-180x180.png"">
		<link rel=""icon"" type=""image/png"" sizes=""192x192""  href=""/images/favicons/android-icon-192x192.png"">
		<link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""/images/favicons/favicon-32x32.png"">
		<link rel=""icon"" type=""image/png"" sizes=""96x96"" href=""/images/favicons/favicon-96x96.png"">
		<link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/images/favicons/favicon-16x16.png"">
		<link rel=""manifest"" href=""/manifest.json"">
		<meta name=""msapplication-TileColor"" content=""#ffffff"">
		<meta name=""msapplication-TileImage"" content=""/ms-icon-144x144.png"">
		<meta name=""theme-color"" content=""#ffffff"">
	");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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