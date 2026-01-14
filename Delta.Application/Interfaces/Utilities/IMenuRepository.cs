using System;
using System.Collections.Generic;
using Delta.Domain.Entities.Utilities;


namespace Delta.Application.Interfaces.Utilities
{
   
        public interface IMenuRepository
        {
            Task<List<Menu>> GetAllAsync();
        }
   
}
