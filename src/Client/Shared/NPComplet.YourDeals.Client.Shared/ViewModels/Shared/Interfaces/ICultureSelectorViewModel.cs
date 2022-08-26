using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Shared.Interfaces
{
   public interface ICultureSelectorViewModel
    {
        CultureInfo[] cultures { get; set; }
        CultureInfo Culture { get; set; }

      
    }
}
