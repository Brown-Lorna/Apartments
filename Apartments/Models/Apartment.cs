using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apartments.Models
{
    public class Apartment : IValidatableObject
    {
        //Get and set variables from inputs. Validate inputs. User-friendly display headings.
        public int ApartmentId { get; set; }
        [DisplayName("Apt Address")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Apt Address must be between 3 and 50 characters.")]
        public string AptAddress { get; set; }
        [DisplayName("Sq. Footage")]
        [Range(100, 2000, ErrorMessage = "Sq. Footage must be between 100 and 2000.")]
        public int SqFootage { get; set; }
        [DisplayName("Monthly Utility Fee")]
        [Range(0, 300, ErrorMessage = "Utility Fee must be between 0 and 300.")]
        [DataType(DataType.Currency)]
        public double MonthUtilityFee { get; set; }
        [DisplayName("Monthly Parking Fee")]
        [Range(0, 20, ErrorMessage = "Parking Fee must be between 0 and 20.")]
        [DataType(DataType.Currency)]
        public double MonthParkfee { get; set; }
        [DisplayName("Last Cleaning Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastCleanDate { get; set; }
        [DisplayName("Vacant")]
        public bool IsVacant { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Now < LastCleanDate)
            {
                yield return
                    new ValidationResult(errorMessage: "Last Clean Date cannot be greater than today's date",
                        memberNames: new[] { "LastCleanDate" });
            }
        }
    }
}
