using NPComplet.YourDeals.Domain.Shared.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings
{
    [Table("FEESTIERS", Schema = "FINNACE")]
    public class FeesTire: BaseEntityDomain
    {
        public string  Name { get;set;}
    }
}
