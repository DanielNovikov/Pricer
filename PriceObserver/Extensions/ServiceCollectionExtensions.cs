using System;
using Certes;
using FluffySpoon.AspNet.EncryptWeMust;
using FluffySpoon.AspNet.EncryptWeMust.Certes;
using Microsoft.Extensions.DependencyInjection;

namespace PriceObserver.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSslSupport(this IServiceCollection services)
        {
#if !DEBUG // used only on production
            services.AddFluffySpoonLetsEncrypt(new LetsEncryptOptions
            {
                Email = "danil.novikov.dev@gmail.com",
                UseStaging = false,
                Domains = new[] { "pricer.ink" },
                TimeUntilExpiryBeforeRenewal = TimeSpan.FromDays(30),
                TimeAfterIssueDateBeforeRenewal = TimeSpan.FromDays(7),
                CertificateSigningRequest = new CsrInfo
                {
                    CountryName = "Ukraine",
                    Locality = "DK",
                    Organization = "Pricer",
                    OrganizationUnit = "Development",
                    State = "UA"
                }
            });
            
            services.AddFluffySpoonLetsEncryptFileCertificatePersistence();
            services.AddFluffySpoonLetsEncryptMemoryChallengePersistence();
#endif
        }
    }
}