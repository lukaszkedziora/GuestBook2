using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using GuestBook3.Models;
using GuestBook3.Controllers.DAL;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace GuestBook3.Controllers
{
    public class AccountController : Controller
    {
        public AccessData _accessData;
        public IAccessData _iAccesData;


        public AccountController(AccessData accessData, IAccessData iAccesData)
        {
            _accessData = accessData;
            _iAccesData = iAccesData;

        }

        public IActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddUser(AddAccessData accesData)
        {
            if (ModelState.IsValid)
            {
                _accessData.UserName = accesData.UserName;
                _accessData.Password = GetHash(accesData.Password);
                _iAccesData.AddAccessData(_accessData);

                return RedirectToAction("Admin", "Crud");
            }
            return AddUser();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccessData accessData)
        {
            if (ModelState.IsValid)
            {
                var accessDataHashed = GetHash(accessData.Password);
                accessData.Password = accessDataHashed;
                var dbData = _iAccesData.GetAccessData(accessData);

                if (dbData != null && dbData.Password == accessDataHashed)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, accessData.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Admin", "Crud");
                }
                
                else return RedirectToAction("Login");

            }
            return Login();

        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public string GetHash(string password)
        {   //TODO: send salt to database
            byte[] salt = new byte[128 / 8];
            // using (var rng = RandomNumberGenerator.Create())
            // {
            //     rng.GetBytes(salt);
            // }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyUserName(string UserName)
        {
            _accessData.UserName = UserName;

            if (_iAccesData.GetAccessData(_accessData) != null)
            {
                return Json($"Username {_accessData.UserName} is already in use.");
            }

            return Json(true);

        }

    }
}