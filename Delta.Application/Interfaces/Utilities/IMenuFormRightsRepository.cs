using Delta.Domain.Entities.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delta.Application.Interfaces.Utilities
{
    public interface IMenuFormRightsRepository
    {
        /// <summary>
        /// Returns all form, tab and report rights for a menu
        /// </summary>
        Task<List<MenuFormRight>> GetByMenuIdAsync(int menuId);
    }
}
