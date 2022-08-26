using System.ComponentModel.DataAnnotations.Schema;
using NPComplet.YourDeals.Domain.Shared.Base;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways
{
    /// <summary>
    /// 
    /// </summary>
    [Table("GateWayOperationPayouts", Schema = "FINANCE")]
    public class GateWayOperationPayout : BaseEntityDomain
    {
    }
}
