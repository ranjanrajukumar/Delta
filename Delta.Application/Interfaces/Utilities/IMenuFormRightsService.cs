using Delta.Application.DTOs.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delta.Application.Interfaces.Utilities
{
    public interface IMenuFormRightsService
    {
        Task<List<MenuFormRightDto>> GetMenuRightsAsync(int menuId);
    }
}
