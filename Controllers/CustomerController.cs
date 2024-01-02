 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V3.Data;
using V3.Models;
using V3.ViewModels;

namespace V3.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        public IActionResult Form()
        {
            var membershiptypes = _context.MembershipTypes.ToList();
            var viewmodel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershiptypes
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                
                return View("Form", viewModel);
            }

            if (customer.Id == 0)            
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.name = customer.name;
                customerInDb.Birthdate = customer.Birthdate;
                customer.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubcribedTo = customer.IsSubcribedTo;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        //[Authorize]
        public IActionResult Index()
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList();       
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
           var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

        return View("Form", viewModel);
            
        }

        public IActionResult Details(int ID)
        {
            var customers = _context.Customers.SingleOrDefault(c => c.Id == ID);
            if (customers == null)
                return Content("No customer is available");

            return View(customers);
        }
    }
}