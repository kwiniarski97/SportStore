namespace SportsStore.Infrastructure
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    using SportsStore.Models.ViewModels;

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = this.urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder results = new TagBuilder("div");

            for (int i = 1; i <= this.PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                this.PageUrlValues["productPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(this.PageAction, this.PageUrlValues);
                if (this.PageClassesEnabled)
                {
                    tag.AddCssClass(this.PageClass);
                    tag.AddCssClass(i == this.PageModel.CurrentPage ? this.PageClassSelected : this.PageClassNormal);
                    tag.InnerHtml.Append(i.ToString());
                    results.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(results.InnerHtml);
            }
        }
    }
}