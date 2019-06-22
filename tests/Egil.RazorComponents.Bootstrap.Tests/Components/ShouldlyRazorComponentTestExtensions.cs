using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Org.XmlUnit.Builder;
using Shouldly;

namespace Egil.RazorComponents.Bootstrap.Components
{
    internal static class ComponentTestExtensions
    {
        private const string Tab = "\t";
        private const string TestRootElementName = "ROOT";

        internal static void ShouldBe(this ComponentRenderedText componentRenderedText, string expectedHtml)
        {
            var testHtml = string.Concat(componentRenderedText.Tokens).Trim();
            var test = Input.FromString(WrapInTestRoot(testHtml)).Build();
            var control = Input.FromString(WrapInTestRoot(expectedHtml)).Build();
            var diffResult = DiffBuilder.Compare(control)
                .IgnoreWhitespace()
                .WithTest(test)
                .Build();

            var assertDiffMessage = diffResult.Differences
                .Select(diff => $"- {diff.ToString()}")
                .Aggregate(string.Empty, (acc, diff) => $"{acc}{Environment.NewLine}{diff}");

            diffResult.HasDifferences().ShouldBeFalse($"should be" +
                $"{Environment.NewLine}{Environment.NewLine}{PrettyXml(expectedHtml)}" +
                $"{Environment.NewLine}{Environment.NewLine}{Tab}but was" +
                $"{Environment.NewLine}{Environment.NewLine}{PrettyXml(testHtml)}" +
                $"{Environment.NewLine}{Environment.NewLine}{Tab}with the following differences:" +
                $"{Environment.NewLine}{assertDiffMessage}" +
                $"{Environment.NewLine}");
        }

        private static string WrapInTestRoot(string html)
        {
            return $"<{TestRootElementName}>{Environment.NewLine}{html ?? string.Empty}{Environment.NewLine}</{TestRootElementName}>";
        }

        private static string PrettyXml(string xml)
        {            
            try
            {
                var stringBuilder = new StringBuilder();
                var element = XElement.Parse(xml);
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true,
                    NewLineOnAttributes = false
                };

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    element.Save(xmlWriter);
                }

                return stringBuilder.ToString();
            }
            catch
            {
                return xml;
            }
        }
    }
}
