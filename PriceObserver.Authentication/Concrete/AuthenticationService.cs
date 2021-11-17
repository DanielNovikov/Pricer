using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Authentication.Abstract;
using PriceObserver.Authentication.Options;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Authentication;

namespace PriceObserver.Authentication.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserTokenService _userTokenService;
        
        public AuthenticationService(
            IUserTokenRepository userTokenRepository, 
            IUserTokenService userTokenService)
        {
            _userTokenRepository = userTokenRepository;
            _userTokenService = userTokenService;
        }

        public async Task<AuthenticationServiceResult> Authenticate(Guid token)
        {
            var userToken = await _userTokenRepository.GetByToken(token);

            if (userToken == null)
                return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenNotFound);

            if (userToken.Expired)
                return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenExpired);

            await _userTokenService.Expire(userToken);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString())
            };

            var securityToken = PrivateKey.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityToken, SecurityAlgorithms.HmacSha256Signature);
            
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials);

            var responseModel = new AuthenticationResponseModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken)
            };

            return AuthenticationServiceResult.Success(responseModel);
        }
    }
}