using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Contract;
using AutoMapper;
using CustomersRoleUpdater.Application.Mappings;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomerDataService customerDataRequest
    //IMapper mapper
) : ICustomersStatusUpdater
{
    public List<Guid> UpdateCustomerRoles(List<Customer> customers)
    {
        var result = customers.Select(p => p.Id).DistinctBy(p => p).ToList();

        return result; // _mapper.Map<List<CustomerId>>(result);
    }

    public List<Customer> GetCustomerIdsWithoutNull(List<Customer>[] customers)
    {
        return customers.SelectMany(c => c).Where(c => c != null).ToList();
    }

    public async Task<ListCustomerId>? GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();
        var result = await Task.WhenAll(task1, task2, task3);
        if (result.Length > 0)
        {
            var customers = GetCustomerIdsWithoutNull(result);
            if(customers.Count() > 0)
            {
                var listId = UpdateCustomerRoles(customers);
                ListCustomerId customerIds = new();
                customerIds.CustomerIds = listId;
                return customerIds;
            }  
        }
        return null;
    }
}






