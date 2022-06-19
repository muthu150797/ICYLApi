using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EnterpriseLayer.Utilities;


namespace ICYL.BL.CustomValidator
{
    public class ValidateCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (PaymentConfig)validationContext.ObjectInstance;

            if (model.PaymentType !=null && model.PaymentType.ToLower().Equals("6"))
            {
                if (string.IsNullOrEmpty(Conversion.ConversionToString(value)))
                {
                    return new ValidationResult(this.ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;

                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }


    public class ValidateCC : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (PaymentConfig)validationContext.ObjectInstance;

            if (model.PaymentType != null && model.PaymentType.ToLower().Equals("5"))
            {
                if (string.IsNullOrEmpty(Conversion.ConversionToString(value)))
                {
                    return new ValidationResult(this.ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}
