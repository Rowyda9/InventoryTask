using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BackCore.Base
{
    public class Validator       
    {
        #region public Methods

        static void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);

                var validationResults = ((IValidatableObject)item).Validate(validationContext);

                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
            }
        }

        #endregion

        #region IEntityValidator Members

        public static bool IsValid<TEntity>(TEntity item) where TEntity : class
        {

            if (item == null)
                return false;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);

            return !validationErrors.Any();
        }

        public static IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);

            return validationErrors;
        }

        #endregion
    }
}
