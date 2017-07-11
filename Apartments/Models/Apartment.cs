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
        public int ApartmentId { get; set; }
        [DisplayName("Apt Address")]
        public string AptAddress { get; set; }
        [DisplayName("Sq. Footage")]
        public int SqFootage { get; set; }
        [DisplayName("Month Utility Fee")]
        [DataType(DataType.Currency)]
        public double MonthUtilityFee { get; set; }
        [DisplayName("Month Park Fee")]
        [DataType(DataType.Currency)]
        public double MonthParkfee { get; set; }
        [DisplayName("Last Clean Date")]
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
