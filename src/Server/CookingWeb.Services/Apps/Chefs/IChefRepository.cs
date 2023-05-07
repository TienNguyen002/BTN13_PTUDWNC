using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Chef;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Chefs
{
    public interface IChefRepository
    {
        Task<IList<ChefItem>> GetChefsAsync(
             bool id = true,
         CancellationToken cancellationToken = default);
        Task<IList<ChefItem>> GetCachedChefsByIdAsync(
            CancellationToken cancellationToken = default);
        Task<IList<ChefItem>> GetChefsSlugAsync(
         CancellationToken cancellationToken = default);
      
        Task<bool> IsChefSlugExistedAsync(
         int id,
         string slug,
         CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateChefAsync(
           Chef chef,
           CancellationToken cancellationToken = default);
        Task<bool> DeleteChefsAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task<Chef> GetChefsBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);
    }
}
