using Gestion_User.Models.Authentication.SignUp;
using Gestion_User.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Gestion.Service.Services;
using User.Gestion.Service.Models;
using Gestion_User.Models.Authentication.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models.Authentication.User;

namespace Gestion_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IUserManagement _user;



        public AuthenticationController(UserManager<ApplicationUser> userManager, IEmailService emailService,
            RoleManager<IdentityRole> roleManager, IUserManagement user, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _signInManager = signInManager;
            _user = user;


        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var tokenResponse = await _user.CreateUserWithTokenAsync(registerUser);
            if (tokenResponse.IsSuccess)
            {
                await _user.AssignRoleToUserAsync(registerUser.Roles, tokenResponse.Response.User);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenResponse.Response.Token, email = registerUser.Email }, Request.Scheme);
                var message = new Message(new string[] { registerUser.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                 new Response { Status = "Success", Message = "Email Verified Successfully" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
          new Response { Message = tokenResponse.Message, IsSuccess = false });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id) // Ajoutez l'ID utilisateur ici
        };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }



        //[HttpPost]
        //[Route("loginOTP")]
        //public async Task<IActionResult> LoginWithOTP([FromBody] LoginF2AModel loginF2AModel)
        //{
        //    var jwt = await _user.LoginUserWithJWTokenAsync(loginF2AModel.code, loginF2AModel.userName);
        //    if (jwt.IsSuccess)
        //    {
        //        return Ok(jwt);
        //    }
        //    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Success", Message = $"Invalid Code" });
        //}

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var userProfile = new
            {
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.Address, // Assuming 'Address' exists in ApplicationUser
                user.ProfileImage,  // Inclure l'image de profil
                Roles = await _userManager.GetRolesAsync(user) // Correction pour obtenir les rôles
            };

            return Ok(userProfile);
        }
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileModel model, [FromForm] IFormFile? profileImage)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            // Si une nouvelle image est téléchargée
            if (profileImage != null && profileImage.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images/profiles", $"{userId}_{profileImage.FileName}");
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }
                user.ProfileImage = $"/images/profiles/{userId}_{profileImage.FileName}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Profile updated successfully" });
            }

            return BadRequest(new { message = "Profile update failed" });
        }


        [Authorize]
        [HttpPut("profile/upload-image")]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile profileImage)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (profileImage == null || profileImage.Length == 0)
            {
                return BadRequest(new { message = "No image uploaded" });
            }

            // Sauvegarder l'image sur le serveur
            var imagePath = Path.Combine("wwwroot/images/profiles", $"{userId}_{profileImage.FileName}");
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(stream);
            }

            // Mettre à jour le chemin de l'image de profil dans l'utilisateur
            user.ProfileImage = $"/images/profiles/{userId}_{profileImage.FileName}";
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = "Profile image uploaded successfully", imagePath = user.ProfileImage });
            }

            return BadRequest(new { message = "Failed to upload profile image" });
        }


        [HttpPost]
        [Route("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(LoginResponse tokens)
        {
            var jwt = await _user.RenewAccessTokenAsync(tokens);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Success", Message = $"Invalid Code" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]

        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password link", forgotPasswordLink!);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Password changed request is sent on Email {user.Email}. Please open your email" });

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error ", Message = $"Could not send link to email , please try again." });

        }


        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new
            {
                model
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]

        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Could not send link to email , please try again ." });
        }



        [HttpGet("validate-token")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName
            });
        }
    }
}