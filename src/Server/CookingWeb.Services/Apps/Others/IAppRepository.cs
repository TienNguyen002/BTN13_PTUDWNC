using CookingWeb.Core.DTO.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Other
{
    public interface IAppRepository
    {
        #region Demand
        Task<IList<DemandItem>> GetDemandsAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Price
        Task<IList<PriceItem>> GetPricesAsync(CancellationToken cancellationToken = default);
        #endregion

        #region NumberOfSessions
        Task<IList<NumberOfSessionsItem>> GetNumberOfSessionsAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
