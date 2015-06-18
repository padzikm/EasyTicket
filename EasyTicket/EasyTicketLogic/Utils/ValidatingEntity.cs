using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public abstract class ValidatingEntity<T> : IDataErrorInfo
    {
        private readonly Dictionary<string, Func<ValidatingEntity<T>, object>> propertyGetters;
        private readonly Dictionary<string, ValidationAttribute[]> validators;

        public ValidatingEntity()
        {
            this.validators = this.GetType()
             .GetProperties()
             .Where(p => this.GetValidations(p).Length != 0)
             .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
             .GetProperties()
             .Where(p => this.GetValidations(p).Length != 0)
             .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }

        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        private Func<ValidatingEntity<T>, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<ValidatingEntity<T>, object>(viewmodel => property.GetValue(viewmodel, null));
        }

        public string this[string propertyName]
        {
            get
            {
                if (this.propertyGetters.ContainsKey(propertyName))
                {
                    var propertyValue = this.propertyGetters[propertyName](this);
                    var errorMessages = this.validators[propertyName]
                     .Where(v => !v.IsValid(propertyValue))
                     .Select(v => v.ErrorMessage).ToArray();

                    return string.Join(Environment.NewLine, errorMessages);
                }

                return string.Empty;
            }
        }

        public string Error
        {
            get
            {
                var errors = from validator in this.validators
                             from attribute in validator.Value
                             where !attribute.IsValid(this.propertyGetters[validator.Key](this))
                             select attribute.ErrorMessage;

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }
    }
}
