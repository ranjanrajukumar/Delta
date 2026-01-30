using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Delta.Infrastructure.Persistence.Dapper
{
    public interface IDapperContext
    {
     

        Task<int> EditDataAsync(string command, object parameters);
        Task<List<T>> GetAllAsync<T>(string command, object parameters);
        Task<T?> GetAsync<T>(string command, object parameters);

        Task<T> ExecuteScalarAsync<T>(string command, object parameters);
    }
}
