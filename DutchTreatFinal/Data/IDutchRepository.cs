using System.Collections.Generic;
using DutchTreatFinal.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreatFinal.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string catergory);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);
        bool SaveAll();
        void AddEntity(object model);
    }
}