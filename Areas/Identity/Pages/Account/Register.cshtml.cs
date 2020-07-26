using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using T2RMSWS.Data;

namespace T2RMSWS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        //add field for context
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context //update constructor to include context
)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            //assign injected context to field
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
            [Required]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            // ADD ADDITIONAL PROPS
           
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {

                        await _userManager.AddToRoleAsync(user, "Member");
                        //add person to asp.user
                        var person = new Person { Id = Guid.NewGuid().ToString(), FirstName = Input.FirstName, LastName = Input.LastName, Phone = Input.Phone, Email = Input.Email };
                        //add person to db
                        var last = new Person();
                        last = _context.People.ToList().Last();
                        var lastId = last.Id.ToString();
                        int returnInt;
                        bool intResultTryParse = int.TryParse(lastId.ToString(), out returnInt);
                        var intLastId = 0;
                        if (intResultTryParse == true)
                        {
                            intLastId = returnInt;
                        }
                        else
                        {
                            intLastId = 5;
                        }
                        var peopleList = _context.People.Where(p => p.Id == (intLastId + 1).ToString());

                        while (peopleList.ToList().Count() != 0)
                        {
                            intLastId++;
                            peopleList = _context.People.Where(p => p.Id == intLastId.ToString());
                        }
                        //get last StaffId
                        int lastMemberId = _context.Customers
                            .Select(s => s.CustomerId).ToList().Max();
                        //increment to next Id

                        int nextMemberId = lastMemberId + 1;

                        var newMember = new T2RMSWS.Data.Customer
                        {
                            Id = intLastId.ToString(),
                            CustomerId = nextMemberId,
                            FirstName = Input.FirstName,
                            LastName = Input.LastName,
                            Email = Input.Email,
                            Phone = Input.Phone
                        };
                        _context.People.Add(newMember);

                        await _context.SaveChangesAsync();
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //add new user to member role

                        //// MEMBER //
                        await _userManager.AddToRoleAsync(user,"Customer");
                        //// MANAGER //
                        //await _userManager.AddToRoleAsync(user, "Manager");
                        //// STAFF //
                        //await _userManager.AddToRoleAsync(user, "Staff");

                        //add logic to create a new person
                        var restaurant = _context.Restaurants.First();
                        
                      

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
