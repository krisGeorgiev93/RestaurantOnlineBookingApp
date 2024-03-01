using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace RestaurantOnlineBookingApp.Infrastructure.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult result = 
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !string.IsNullOrWhiteSpace(result.FirstValue))
            {
                decimal parsedValue = 0;
                bool binderSucceeded = false;

                try
                {
                    string formDecimalValue = result.FirstValue;
                    formDecimalValue = formDecimalValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    formDecimalValue = formDecimalValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    parsedValue = Convert.ToDecimal(formDecimalValue);
                    binderSucceeded = true; 
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (binderSucceeded)
                {
                    bindingContext.Result = ModelBindingResult.Success(parsedValue);
                }


            }

            return Task.CompletedTask;
        }
    }
}
