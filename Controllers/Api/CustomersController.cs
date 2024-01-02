using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;

namespace V3.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }


        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer (int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return NotFound();

            return customer;    
        }

        //POST /Api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer (Customer customer)
        {
            if (!ModelState.IsValid)
                return NotFound();

            _context.Add(customer);
            _context.SaveChanges();

            return customer;
        }


        //PUT /Api/customers/1
        [HttpPut]
        public ActionResult<Customer> UpdateCustomer (int Id, Customer customer)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (CustomerInDb == null)
                return NotFound();

            CustomerInDb.name = customer.name;
            CustomerInDb.Birthdate = customer.Birthdate;
            CustomerInDb.MembershipTypeId = customer.MembershipTypeId;
            CustomerInDb.IsSubcribedTo = customer.IsSubcribedTo;

            _context.SaveChanges();
            return NoContent();
        }

        //DELETE /Api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int Id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customerInDb == null)
                return Content("here");

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
