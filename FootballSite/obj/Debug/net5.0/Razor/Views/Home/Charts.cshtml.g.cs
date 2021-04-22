#pragma checksum "C:\Users\Home-PC\source\repos\FootballSite\FootballSite\Views\Home\Charts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8f7de8afb85c6a3f93e78af9fa10c96d1f41e63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Charts), @"mvc.1.0.view", @"/Views/Home/Charts.cshtml")]
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
#line 1 "C:\Users\Home-PC\source\repos\FootballSite\FootballSite\Views\_ViewImports.cshtml"
using FootballSite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Home-PC\source\repos\FootballSite\FootballSite\Views\_ViewImports.cshtml"
using FootballSite.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8f7de8afb85c6a3f93e78af9fa10c96d1f41e63", @"/Views/Home/Charts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e68e51e968d2800dfce256dcbb30edf6f5df3ba4", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Charts : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Home-PC\source\repos\FootballSite\FootballSite\Views\Home\Charts.cshtml"
  
    ViewBag.Title = "title";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div id=\"chart1\"></div>\r\n        <div id=\"chart2\"></div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
    <script>
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            $.get('/api/Charts/StadiumData', function(JsonData) {
                data = google.visualization.arrayToDataTable(JsonData, false);
                var option = {
                    title: ""Місткість стадіонів"",
                    width: 500,
                    height: 400
                };
                chart = new google.visualization.PieChart(document.getElementById('chart1'));
                chart.draw(data, option);
            });

            $.get('/api/Charts/PlayerCountryData', function(JsonData) {
                 data = google.visualization.arrayToDataTable(JsonData, false);
                 var option = {
                     title: ""Країни гравців"",
                     width: 500,
                     heigh");
                WriteLiteral("t: 400\r\n                 };\r\n                 chart = new google.visualization.PieChart(document.getElementById(\'chart2\'));\r\n                 chart.draw(data, option);\r\n            });\r\n\r\n        }\r\n    </script>\r\n");
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
