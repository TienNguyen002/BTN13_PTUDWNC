using CookingWeb.Core.DTO.Others;
using CookingWeb.Core.Entities;
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

        Task<Demand> GetDemandByIdAsync(int id, CancellationToken cancellationToken = default);  
        #endregion

        #region Price
        Task<IList<PriceItem>> GetPricesAsync(CancellationToken cancellationToken = default);

        Task<Price> GetPriceByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion

        #region NumberOfSessions
        Task<IList<NumberOfSessionsItem>> GetNumberOfSessionsAsync(CancellationToken cancellationToken = default);

        Task<NumberOfSessions> GetNumberOfSessionsByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion

        #region Topic
        Task<IList<TopicItem>> GetTopicsAsync(CancellationToken cancellationToken = default);

        Task<Topic> GetTopicByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}
