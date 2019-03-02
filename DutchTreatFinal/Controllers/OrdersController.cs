using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreatFinal.Data;
using DutchTreatFinal.Data.Entities;
using DutchTreatFinal.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreatFinal.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
     //[ApiController]
    
    
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var results = _repository.GetAllOrders(includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpGet("id:int")]
        public IActionResult Get(int orderId)
        { 
            try
            {
                var order = _repository.GetOrderById(orderId);

                if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
                else return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }
         [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            //add to the Db
            try
            {
                if(ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                       
                        return Created($"/api/orders/{ newOrder.Id}", 
                            _mapper.Map<Order,OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
            }

            return BadRequest("Failed to get new order");
        }
    }
}