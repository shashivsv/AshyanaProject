using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ashyana.UI.Web.Models;
using System.Web.Security;

using Ashyana.Common;
using System.Collections;
using System.Collections.Generic;
using Ashyana.UI.Web.ViewModel;

using System.Data.Entity;

namespace Ashyana.UI.Web.Controllers
{

    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        AshyanaDBEntities objEntity;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }

        public ActionResult test()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxMethod(User user)
        {
            //int personId = person.PersonId;
            //string name = person.Name;
            //string gender = person.Gender;
            //string city = person.City;
            // System.Threading.Thread.Sleep(1000);
            return Json(user);
        }
        //
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        // public async Task<ActionResult> Login(MyUserModel model, string returnUrl)

        [HttpPost]
        public JsonResult CheckUser(string userName, string userPassword)
        {
            int loginstatus = 0;
            bool logged = false;
            if (ModelState.IsValid)
            {
                objEntity = new AshyanaDBEntities();
                string pwd = Encode.Encrypt(userPassword);
                var result = objEntity.Users.Where(p => p.userName == userName).Where(q => q.userPassword == pwd)
                                    .Join(objEntity.Roles, u => u.RoleID, r => r.roleID,
                                    (u, r) => new
                                    {
                                        userID = u.userID,
                                        userName = u.userName,
                                        userFirstName = u.userFirstname,
                                        userLastName = u.userLastname,
                                        userPassword = u.userPassword,
                                        roleName = r.roleName,
                                        RoleID = u.RoleID,
                                    }
                                    ).FirstOrDefault();
                if (result != null)
                {
                    MyUserModel mm = new MyUserModel();
                    FormsAuthentication.SetAuthCookie(result.userName, false);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(result.userName, false, 60);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
                    logged = true;

                    Session["UserID"] = result.userID;
                    Session["Username"] = result.userName;
                    Session["Name"] = result.userFirstName + " " + result.userLastName;
                    Session["RoleID"] = result.RoleID;
                    Session["userType"] = result.roleName;

                    //calling stored procedure from entity framework

                    var menulist = (from i in objEntity.U_sp_Getmenu(result.RoleID)
                                    group i by new { i.linkName, i.subLinkName, i.subLinkPath } into g
                                    select new
                                    {
                                        linkName = g.Key.linkName,
                                        subLinkName = g.Key.subLinkName,
                                        subLinkPath = g.Key.subLinkPath
                                    }).ToList();

                    List<MenuList> lst = new List<MenuList>();
                    foreach (var item in menulist)
                    {
                        MenuList objmenu = new MenuList();
                        objmenu.linkName = item.linkName;
                        objmenu.subLinkName = item.subLinkName;
                        objmenu.subLinkPath = item.subLinkPath;
                        lst.Add(objmenu);
                    }
                    Session["mList"] = lst;
                    bool usersuccess = User.Identity.IsAuthenticated;
                    if (result.RoleID == 1)
                    {
                        //  returnUrl = "";
                        RedirectToAction("Index", "Home", lst);
                    }
                    else if (result.RoleID == 2)
                    {
                        RedirectToAction("Index", "Ashyana", lst);
                    }
                    else if (result.RoleID == 3)
                    {
                        RedirectToAction("Index", "Ashyana", lst);
                    }
                }

                else
                {
                    loginstatus = 1;
                    Json(loginstatus, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(loginstatus, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Login(MyUserModel model, string returnUrl)
        {
            bool isvaliduser;
            if (ModelState.IsValid)
            {

                objEntity = new AshyanaDBEntities();
                string pwd = Encode.Encrypt(model.userPassword);

                var res1 = objEntity.Users.Where(x => x.userName.Equals(model.userName)
                                                && x.userPassword.Equals(pwd)).FirstOrDefault();
                var res = objEntity.Users.Where(p => p.userName == model.userName).Where(q => q.userPassword == pwd)
                                     .Join(objEntity.Roles, u => u.RoleID, r => r.roleID,
                                     (u, r) => new
                                     {
                                         userID = u.userID,
                                         userName = u.userName,
                                         userFirstName = u.userFirstname,
                                         userLastName = u.userLastname,
                                         userPassword = u.userPassword,
                                         roleName = r.roleName,
                                         RoleID = u.RoleID,
                                     }
                                     ).FirstOrDefault();

                if (res != null)
                {
                    FormsAuthentication.SetAuthCookie(model.userName, model.RememberMe);
                    Session["UserID"] = res.userID;
                    Session["Username"] = res.userName;
                    Session["Name"] = res.userFirstName + " " + res.userLastName;
                    Session["RoleID"] = res.RoleID;
                    Session["userType"] = res.roleName;

                    //calling stored procedure from entity framework

                    var menulist = (from i in objEntity.U_sp_Getmenu(res.RoleID)
                                    group i by new { i.linkName, i.subLinkName, i.subLinkPath } into g
                                    select new
                                    {
                                        linkName = g.Key.linkName,
                                        subLinkName = g.Key.subLinkName,
                                        subLinkPath = g.Key.subLinkPath
                                    }).ToList();

                    List<MenuList> lst = new List<MenuList>();
                    foreach (var item in menulist)
                    {
                        MenuList objmenu = new MenuList();
                        objmenu.linkName = item.linkName;
                        objmenu.subLinkName = item.subLinkName;
                        objmenu.subLinkPath = item.subLinkPath;
                        lst.Add(objmenu);
                    }
                    Session["mList"] = lst;

                    if (res.RoleID == 1)
                    {
                        return RedirectToAction("Index", "Home", lst);
                    }
                    else if (res.RoleID == 2)
                    {
                        return RedirectToAction("Index", "Ashyana", lst);
                    }
                    else if (res.RoleID == 3)
                    {
                        return RedirectToAction("Index", "Ashyana", lst);
                    }
                }
                else
                {
                    isvaliduser = false;
                    ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                    ViewBag.error = isvaliduser;
                }

                //var result = await SignInManager.PasswordSignInAsync(model.userEmailID, model.userPassword, model.RememberMe, shouldLockout: false);

                //switch (result)
                //{
                //    case SignInStatus.Success:
                //        return RedirectToLocal(returnUrl);
                //    case SignInStatus.LockedOut:
                //        return View("Lockout");
                //    case SignInStatus.RequiresVerification:
                //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                //    case SignInStatus.Failure:
                //    default:
                //        ModelState.AddModelError("", "Invalid login attempt.");
                //        return View(model);
                //}

            }
            return View(model);
        }

        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(MyUserModel model)
        {
            if (ModelState.IsValid)
            {
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    User u = new User();
                    u.userName = model.userName;
                    u.userFirstname = model.userFirstname;
                    u.userLastname = model.userLastname;
                    u.userEmailID = model.userEmailID;
                    u.userPassword = Encode.Encrypt(model.userPassword);
                    u.userContactno = model.userContactno;
                    u.RoleID = model.RoleID;
                    u.userCreatedOn = Convert.ToDateTime(DateTime.Now.ToString());
                    u.countryID = model.countryID;
                    u.StateID = model.StateID;
                    u.cityID = model.cityID;
                    objEntity.Entry(u).State = EntityState.Added;


                    objEntity.SaveChanges();
                }


                var user = new ApplicationUser { UserName = model.userName, Email = model.userEmailID, PhoneNumber = model.userContactno };
                var result = await UserManager.CreateAsync(user, model.userPassword);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {

            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(User model)
        {
            if (ModelState.IsValid)
            {
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    User user = (from i in objEntity.Users
                                 where i.userEmailID == model.userEmailID
                                 select i).FirstOrDefault();
                    if (user != null)
                    {

                        user.userPassword = Encode.Encrypt(model.userPassword);
                        objEntity.SaveChanges();
                        return RedirectToAction("ResetPasswordConfirmation", "Account");
                    }
                }
            }
            return View();
        }
        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        //  POST: /Account/LogOff
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}