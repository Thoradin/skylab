using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EgyetemiSzoftverek.Helpers
{
    public class DashedRoutingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var hasRouteAttributes = controller.Selectors.Any(selector =>
                                               selector.AttributeRouteModel != null);
            if (hasRouteAttributes)
            {
                // This controller manually defined some routes, so treat this 
                // as an override and not apply the convention here.
                return;
            }

            foreach (var controllerAction in controller.Actions)
            {
                foreach (var selector in controllerAction.Selectors.Where(x => x.AttributeRouteModel == null))
                {
                    var template = new StringBuilder();

                    template.Append("/" + PascalToKebabCase(controllerAction.ActionName));

                    selector.AttributeRouteModel = new AttributeRouteModel()
                    {
                        Template = template.ToString()
                    };
                }
            }
        }

        public static string PascalToKebabCase(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }
    }
}
