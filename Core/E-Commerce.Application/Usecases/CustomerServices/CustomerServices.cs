using E_Commerce.Application.Dtos.CustomerDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {

        private readonly IRepository<Customer> _repository;

        public CustomerServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            await _repository.CreateAsync(new Customer
            {
                FirstName = createCustomerDto.FirstName,
                LastName =  createCustomerDto.LastName,
                Email = createCustomerDto.Email
            });
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(customer);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(x => new ResultCustomerDto
            {
                CustomerID = x.CustomerID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Orders = x.Orders,  
            }).ToList();
        }

        public async Task<GetByIdCustomerDto> GetByIdAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            return new GetByIdCustomerDto
            {
                CustomerID = values.CustomerID,
                FirstName = values.FirstName,
                LastName = values.LastName,
                Email = values.Email,
                Orders = values.Orders,
            };
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var values = await _repository.GetByIdAsync(updateCustomerDto.CustomerID);
            values.FirstName = updateCustomerDto.FirstName;
            values.LastName = updateCustomerDto.LastName;
            values.Email = updateCustomerDto.Email;
            //values.Orders = updateCustomerDto.Orders;   
            await _repository.UpdateAsync(values);

        }
    }
}
