using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using V3.ViewModels;
using V3.Models;

namespace V3.Controllers
{
    
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser>userManager)
        {
            this.roleManager = roleManager;
            this.userManager= userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult CreateUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityuser = new IdentityUser
                {
                    UserName = model.UserName
                  
                };



                IdentityResult result = await userManager.CreateAsync(identityuser);

                if (result.Succeeded)
                {

                    return RedirectToAction("ListUsers", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            //if (ModelState.IsValid)
            //{
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return Content("No role to Edit");
                }

                var model = new EditRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name
                };

                foreach (var user in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.Users.Add(user.UserName);
                    }
                }
                return View(model);
            
           
        }
     

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            { 
            var role = await roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return Content("No role to Edit");
                }

                else
                {
                    role.Name = model.RoleName;
                    var result = await roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        var notificationMessage = "Role updated successfully";
                        TempData["NotificationMessage"] = notificationMessage;
                        return RedirectToAction("ListRoles");

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                   
                }
               
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return Content("No users to Edit");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return Content("No role found");
            }

            for (int i =0; i<model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected &&  !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                     result = await userManager.AddToRoleAsync(user, role.Name);
                }

                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }

                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        RedirectToAction("EditRole", new { Id = roleId });

                }
            }
           return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        [Authorize(Roles = RoleName.SystemAdmin)]
        
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return Content("No role found"); // Role not found, return a 404 response.
            }

            // Check if there are any users in this role before deleting it.
            //var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            //if (usersInRole.Count > 0)
            //{
            //    // You may want to handle this scenario according to your application's requirements.
            //    // You can choose to display an error message, prevent deletion, or remove users from the role.
            //    // In this example, we're just displaying a message.
                
            //    return Content("Can't delete role with users in it");
            //}

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            else
            {
                // Handle the case where role deletion fails. You can display an error message or handle it as needed.
                return Content("Error deleting");
            }
       
        }

    }
}