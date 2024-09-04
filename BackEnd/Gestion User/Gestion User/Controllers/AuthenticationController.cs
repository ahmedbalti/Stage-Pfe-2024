﻿using Gestion_User.Models.Authentication.SignUp;
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
            var loginOtpResponse = await _user.GetOtpByLoginAsync(loginModel);

            if (loginOtpResponse.Response != null)
            {
                var user = loginOtpResponse.Response.User;

                // Assurez-vous que le mot de passe est correct avant d'envoyer l'OTP
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    if (user.TwoFactorEnabled)
                    {
                        var token = loginOtpResponse.Response.Token;
                        var message = new Message(new string[] { user.Email! }, "OTP Confirmation", token);
                        _emailService.SendEmail(message);

                        return StatusCode(StatusCodes.Status200OK,
                            new Response { IsSuccess = loginOtpResponse.IsSuccess, Message = $"We have sent an OTP to your Email {user.Email}" });
                    }

                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id) // Ajoutez cette ligne

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
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }

        [HttpPost]
        [Route("login-otp")]
        public async Task<IActionResult> LoginOTP([FromBody] LoginOTPModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "User not found!" });
            }

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.OTP);
            if (result)
            {
                var jwtResponse = await _user.GetJwtTokenAsync(user);
                if (jwtResponse.IsSuccess)
                {
                    return Ok(jwtResponse.Response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "Failed to generate JWT token." });
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = "Invalid OTP." });
        }

        [HttpPost]
        [Route("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] LoginOTPModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "User not found!" });
            }

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.OTP);
            if (result)
            {
                var jwtResponse = await _user.GetJwtTokenAsync(user);
                if (jwtResponse.IsSuccess)
                {
                    // Obtenir le rôle de l'utilisateur
                    var userRoles = await _userManager.GetRolesAsync(user);
                    string redirectUrl;

                    if (userRoles.Contains("Client"))
                    {
                        // Rediriger vers /ticket si le rôle est Client
                        redirectUrl = $"http://localhost:4200/#/ticket?token={jwtResponse.Response.AccessToken.Token}";
                    }
                    else
                    {
                        // Rediriger vers /ticketManagement pour tous les autres rôles
                        redirectUrl = $"http://localhost:4200/#/ticketManagement?token={jwtResponse.Response.AccessToken.Token}";
                    }

                    var message = new Message(new string[] { user.Email! }, "Login Link", redirectUrl);
                    _emailService.SendEmail(message);

                    return Ok(new Response { Status = "Success", Message = "Login link sent to your email." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "Failed to generate JWT token." });
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = "Invalid OTP." });
        }

        [HttpGet("LoginWithToken")]
        public async Task<IActionResult> LoginWithToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            var principal = GetClaimsPrincipal(token);
            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            string redirectUrl;

            if (userRoles.Contains("Client"))
            {
                redirectUrl = $"http://localhost:4200/#/ticket?token={token}";
            }
            else
            {
                redirectUrl = $"http://localhost:4200/#/ticketManagement?token={token}";
            }

            return Redirect(redirectUrl);
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }


        //[HttpPost]
        //[Route("verify-otp")]
        //public async Task<IActionResult> VerifyOtp([FromBody] LoginOTPModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.Username);
        //    if (user == null)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound,
        //            new Response { Status = "Error", Message = "User not found!" });
        //    }

        //    var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.OTP);
        //    if (result)
        //    {
        //        var jwtResponse = await _user.GetJwtTokenAsync(user);
        //        if (jwtResponse.IsSuccess)
        //        {
        //            // Générer l'URL avec le token JWT pour le frontend Angular
        //            var frontendUrl = $"http://localhost:4200/#/ticket?token={jwtResponse.Response.AccessToken.Token}";
        //            var message = new Message(new string[] { user.Email! }, "Login Link", frontendUrl);
        //            _emailService.SendEmail(message);

        //            return Ok(new Response { Status = "Success", Message = "Login link sent to your email." });
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError,
        //                new Response { Status = "Error", Message = "Failed to generate JWT token." });
        //        }
        //    }

        //    return StatusCode(StatusCodes.Status400BadRequest,
        //        new Response { Status = "Error", Message = "Invalid OTP." });
        //}

        //[HttpGet("LoginWithToken")]
        //public async Task<IActionResult> LoginWithToken(string token)
        //{
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized(new { message = "Token is missing" });
        //    }

        //    // Redirige vers l'application Angular avec le token dans le fragment de l'URL
        //    var redirectUrl = $"http://localhost:4200/#/ticket?token={token}";
        //    return Redirect(redirectUrl);
        //}



        [HttpPost]
        [Route("loginOTP1")]
        public async Task<IActionResult> LoginWithOTP([FromBody] LoginF2AModel loginF2AModel)
        {
            var jwt = await _user.LoginUserWithJWTokenAsync(loginF2AModel.code, loginF2AModel.userName);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Success", Message = $"Invalid Code" });
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("forgot-password")]

        //public async Task<IActionResult> ForgotPassword(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
        //        var message = new Message(new string[] { user.Email! }, "Forgot Password link", forgotPasswordLink!);
        //        _emailService.SendEmail(message);
        //        return StatusCode(StatusCodes.Status200OK,
        //            new Response { Status = "Success", Message = $"Password changed request is sent on Email {user.Email}. Please open your email" });

        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest,
        //        new Response { Status = "Error ", Message = $"Could not send link to email , please try again." });

        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = $"http://localhost:4200/#/resetPassword?token={token}&email={user.Email}";
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
                        // Log the errors for debugging
                        Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Could not send link to email, please try again." });
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("reset-password")]

        //public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        //{
        //    var user = await _userManager.FindByEmailAsync(resetPassword.Email);
        //    if (user != null)
        //    {
        //        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
        //        if (!resetPassResult.Succeeded)
        //        {
        //            foreach (var error in resetPassResult.Errors)
        //            {
        //                ModelState.AddModelError(error.Code, error.Description);
        //            }
        //            return Ok(ModelState);
        //        }
        //        return StatusCode(StatusCodes.Status200OK,
        //            new Response { Status = "Success", Message = $"Password has been changed" });
        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest,
        //        new Response { Status = "Error", Message = $"Could not send link to email , please try again ." });
        //}



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