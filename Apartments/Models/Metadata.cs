using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apartments.Models
{
    public class TenantMetadata
    {
        [DisplayName("First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First Name must begin with a capital letter and only contain letters and spaces.")]
        public string FirstName;

        [DisplayName("Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Last Name must begin with a capital letter and only contain letters and spaces.")]
        public string LastName;

        [RegularExpression("^[01]?[- .]?(\\([2-9]\\d{2}\\)|[2-9]\\d{2})[- .]?\\d{3}[- .]?\\d{4}$", ErrorMessage = "Invalid Phone Number")] [DisplayName("Phone #")]
        public string Phone;

        [RegularExpression("^(?(\"\")(\"\".+?\"\"@)|(([0-9a-zA-Z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-zA-Z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,6}))$", ErrorMessage = "Invalid Email Address")] [DisplayName("Email")]
        public string Email;
    }

    public class ApartmentMetadata
    {
        [DisplayName("Apt Address")]
        public string AptAddress;

        [DisplayName("Sq. Footage")]
        public int SqFootage;

        [DisplayName("Month Utility Fee")]
        [DataType(DataType.Currency)]
        public double MonthUtilityFee;

        [DisplayName("Month Park Fee")]
        [DataType(DataType.Currency)]
        public double MonthParkfee;

        [DisplayName("Last Clean Date")] [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastCleanDate;

        [DisplayName("Vacant")]
        public bool IsVacant;
    }

    public class ContractMetadata
    {
        [DisplayName("Start Date")] [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate;

        [DisplayName("End Date")] [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate;

        [DisplayName("Monthly Rent")] [DataType(DataType.Currency)]
        public double MonthlyRent;
    }
}
