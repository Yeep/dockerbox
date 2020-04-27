using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using obvious_password.Data;
using obvious_password.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace obvious_password.Pages.Account
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		[TempData]
		public string ErrorMessage { get; set; }

		public class InputModel
		{
			[Required]
			[DataType(DataType.Text)]
			public string Username { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			if (!string.IsNullOrEmpty(ErrorMessage))
			{
				ModelState.AddModelError(string.Empty, ErrorMessage);
			}

			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);

			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var user = await AuthenticateUser(Input.Username, Input.Password);

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt");
					return Page();
				}

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim("FullName", user.FullName),
					new Claim(ClaimTypes.Role, "Administrator"),
				};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				return LocalRedirect(Url.GetLocalUrl(returnUrl));
			}

			return Page();
		}

		private async Task<ApplicationUser> AuthenticateUser(string username, string password)
		{
			await Task.Delay(0);

			if (username == "admin" && password == "admin")
			{
				return new ApplicationUser
				{
					Username = "admin",
					FullName = "Adam Ministrator"
				};
			}
			else
			{
				return null;
			}
		}
	}
}