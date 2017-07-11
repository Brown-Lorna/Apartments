using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apartments.Models;

namespace Apartments.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApartmentContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Tenants.Any())
            {
                return;   // DB has been seeded
            }

            var tenants = new Tenant[]
            {
                new Tenant{FirstName="Carson",LastName="Alexander",Phone="2085554678",Email="josephG@mymail.com"},
                new Tenant{FirstName="Meredith",LastName="Alonso",Phone="2085554683",Email="webdragon@mymail.com"},
                new Tenant{FirstName="Arturo",LastName="Anand",Phone="2085554725",Email="Erikamartinez@mymail.com"},
                new Tenant{FirstName="Gytis",LastName="Barzdukas",Phone="2085554689",Email="rosstheboss@mymail.com"},
                new Tenant{FirstName="Yan",LastName="Li",Phone="2085554687",Email="freddiestrada@mymail.com"},
                new Tenant{FirstName="Peggy",LastName="Justice",Phone="2085554688",Email="paekjunseo@mymail.com"},
                new Tenant{FirstName="Laura",LastName="Norman",Phone="2085554694",Email="scarecrow@mymail.com"},
                new Tenant{FirstName="Nino",LastName="Olivetto",Phone="2085554714",Email="lupechavez@mymail.com"}
            };
            foreach (Tenant s in tenants)
            {
                context.Tenants.Add(s);
            }
            context.SaveChanges();

            var apartments = new Apartment[]
            {
                new Apartment{AptAddress="2401 Alpine St",SqFootage=1100,MonthUtilityFee=95,MonthParkfee=10,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2402 Alpine St",SqFootage=1100,MonthUtilityFee=75,MonthParkfee=5,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2403 Alpine St",SqFootage=1100,MonthUtilityFee=60,MonthParkfee=5,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2404 Alpine St",SqFootage=1100,MonthUtilityFee=95,MonthParkfee=5,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2405 Alpine St",SqFootage=1100,MonthUtilityFee=75,MonthParkfee=10,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2406 Alpine St",SqFootage=1100,MonthUtilityFee=60,MonthParkfee=0,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2407 Alpine St",SqFootage=1100,MonthUtilityFee=95,MonthParkfee=10,LastCleanDate=DateTime.Parse("01/01/1900")},
                new Apartment{AptAddress="2408 Alpine St",SqFootage=1100,MonthUtilityFee=75,MonthParkfee=5,LastCleanDate=DateTime.Parse("01/01/1900")}
            };
            foreach (Apartment c in apartments)
            {
                context.Apartments.Add(c);
            }
            context.SaveChanges();

            var contracts = new Contract[]
            {
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=960,TenantId=1,ApartmentId=1},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=800,TenantId=2,ApartmentId=2},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=750,TenantId=3,ApartmentId=3},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=960,TenantId=4,ApartmentId=4},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=800,TenantId=5,ApartmentId=5},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=750,TenantId=6,ApartmentId=6},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=960,TenantId=7,ApartmentId=7},
                new Contract{StartDate=DateTime.Parse("01/01/1900"),EndDate=DateTime.Parse("01/01/1900"),MonthlyRent=800,TenantId=8,ApartmentId=8}
            };
            foreach (Contract e in contracts)
            {
                context.Contracts.Add(e);
            }
            context.SaveChanges();
        }
    }
}
