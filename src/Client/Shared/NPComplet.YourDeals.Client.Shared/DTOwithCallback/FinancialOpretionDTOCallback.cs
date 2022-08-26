using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.FinanceDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Client.Shared.DTOwithCallback
{
   public class FinancialOpretionDtoCallback : FinancialOpretionDTO
    {

        public EventCallback UpdateFinancSupports { get; set; }
    }
}
