using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ConSauKho.Data.Models;
using ConSauKho.Data.Models.Domains;
using ConSauKho.Data.Models.Views;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConSauKho.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        public LoginController(IUnitOfWork uow) : base(uow)
        {

        }

        //desc: login api
        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            var repo = _uow.GetService<UserDomain>();
            var user = repo.GetByUsername(model.Username);
            if (user != null)
            {
                string saltSaved = user.Salt;
                string password = user.Password;

                string[] byteArrString = saltSaved.Split(" ");
                byteArrString = byteArrString.Take(byteArrString.Count() - 1).ToArray();
                var salt = Array.ConvertAll(byteArrString, Byte.Parse); ;

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                       password: model.Password,
                       salt: salt,
                       prf: KeyDerivationPrf.HMACSHA1,
                       iterationCount: 10000,
                       numBytesRequested: 256 / 8));
                if (hashed != password)
                {
                    return BadRequest("Username or password have wrong.");
                }
                else
                {
                    //var role = user.Role;
                    var resp = new Dictionary<string, object>();
                    //generate token
                    #region Generate JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.Default.GetBytes(JWT.SECRET_KEY);
                    var issuer = JWT.ISSUER;
                    var audience = JWT.AUDIENCE;

                    var identity = new ClaimsIdentity("Application");
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
                    //identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                    identity.AddClaim(new Claim(ClaimTypes.Actor, user.Fullname));

                    var now = DateTime.UtcNow;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = issuer,
                        Audience = audience,
                        Subject = identity,
                        IssuedAt = now,
                        Expires = now.AddDays(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature),
                        NotBefore = now
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    #endregion
                    resp["access_token"] = tokenString;
                    resp["token_type"] = "bearer";
                    resp["expires_utc"] = tokenDescriptor.Expires;
                    resp["issued_utc"] = tokenDescriptor.IssuedAt;

                    return Ok(resp);
                }
            }
            else
            {

                byte[] newSalt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(newSalt);
                }
                string saltSave = "";
                for (int i = 0; i < newSalt.Length; i++)
                {
                    saltSave = saltSave + newSalt[i] + " ";
                }
                //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: model.Password,
                    salt: newSalt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                var newUser = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = model.Username,
                    Password = hashedPassword,
                    Salt = saltSave,
                    Fullname = model.Username,
                };
                repo.Create(newUser);
                _uow.SaveChanges();
                return Ok(newUser);
            }
        }
    }
}
