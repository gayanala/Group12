using DartaGram.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DartaGram.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        // GET: Authentication/Details/5
        public ActionResult Details(int id)
        {
            AuthenticationModel m = new AuthenticationModel();
            m.UserId = Guid.NewGuid();
            return View("Details", m);
        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View();
        }
        public static string Encrypt(string plainText)
        {
            string PasswordHash = "P@@Sw0rd";
            string SaltKey = "S@LT&KEY";
            string VIKey = "@1B2c3D4e5F6g7H8";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        [HttpPost]
        public JsonResult Authenticate(String userName, String password)
        {
            String encryptedPassword = Encrypt(password);
            var filePath = Server.MapPath("~/Users/UsersList.json");
            var jsonData = System.IO.File.ReadAllText(filePath);
            var usersList = JsonConvert.DeserializeObject<List<AuthenticationModel>>(jsonData)
                                  ?? new List<AuthenticationModel>();
            bool flag = false;
            foreach (var user in usersList)
            {
                if (user.userName == userName && user.password == encryptedPassword)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                return new JsonResult { Data = "{success:true}" };
            }
            else
            {
                return new JsonResult { Data = "{success:false}" };
            }
        }

        [HttpPost]
        public JsonResult Save(AuthenticationModel m)
        {
            m.UserId = Guid.NewGuid();
            m.password = Encrypt(m.password);
            var filePath = Server.MapPath("~/Users/UsersList.json");
            var jsonData = System.IO.File.ReadAllText(filePath);
            var usersList = JsonConvert.DeserializeObject<List<AuthenticationModel>>(jsonData)
                                  ?? new List<AuthenticationModel>();
            usersList.Add(m);
            jsonData = JsonConvert.SerializeObject(usersList);
            System.IO.File.WriteAllText(filePath, jsonData);
            return new JsonResult { Data = m };
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authentication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authentication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authentication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authentication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
