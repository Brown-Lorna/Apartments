using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apartments.Models
{
    public class Contract : IValidatableObject
    {
        //Get and set variables from inputs. Validate inputs. User-friendly display headings.
        public int ContractId { get; set; }
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [DisplayName("Monthly Rent")]
        [Range(100, 3000, ErrorMessage = "Monthly Rent must be between 100 and 3000.")]
        [DataType(DataType.Currency)]
        public double MonthlyRent { get; set; }
        public int ApartmentId { get; set; }
        public int TenantId { get; set; }

        public Apartment Apartment { get; set; }
        public Tenant Tenant { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return
                    new ValidationResult(errorMessage: "End Date must be greater than Start Date",
                        memberNames: new[] { "EndDate" });
            }
        }
    }
}
