using E_Commerce.Application.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.CustomerServices
{
    public interface ICustomerServices
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task<GetByIdCustomerDto> GetByIdAsync(int id);
        Task CreateCustomerAsync(CreateCustomerDto model);
        Task UpdateCustomerAsync(UpdateCustomerDto model);
        Task DeleteCustomerAsync(int id);
    }
}
