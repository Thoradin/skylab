﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EgyetemiSzoftverek.Helpers
{
    /// <summary>
    /// Extension methods for the <see cref="HtmlHelper"/> class.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Renders a hyperlink that links to an e-mail address. The e-mail address is encoded to mislead spam bots.
        /// </summary>
        /// <param name="html">The <see cref="HtmlHelper"/> this method extends.</param>
        /// <param name="text">The visible text of the generated hyperlink. It is also rendered in encoded form.</param>
        /// <param name="emailAddress">The e-mail address to link to.</param>
        /// <param name="subject">The proposed subject of the e-mail.</param>
        /// <param name="body">The proposed body of the e-mail.</param>
        /// <returns>A raw <c>a</c> tag that renders the link.</returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Used only from C#.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "html", Justification = "Extension method to conform with other MVC helpers.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension method.")]
        public static HtmlString EmailLink(this IHtmlHelper html, string text, string emailAddress, string subject = null, string body = null)
        {
            // Input validation.
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(emailAddress))
            {
                return null;
            }

            // Encode the sensitive data.
            string encodedEmailAddress = BitConverter.ToString(Encoding.ASCII.GetBytes(emailAddress)).Replace("-", String.Empty);
            string encodedText = BitConverter.ToString(Encoding.ASCII.GetBytes(text)).Replace("-", String.Empty);

            TagBuilder tag = new TagBuilder("a");
            tag.AddCssClass("email-link");
            tag.MergeAttribute("href", "#");
            tag.MergeAttributes(new Dictionary<string, string>
                {
                    { "href", "#" },
                    { "data-code", encodedEmailAddress },
                    { "data-text", encodedText },
                    { "data-subject", subject },
                    { "data-body", body }
                });

            return new HtmlString(tag.ToString());
        }
    }
}
