using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.Entities;
using BulkBuyd.Domain.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pinch.SDK;
using Pinch.SDK.Merchants;

namespace BulkBuyd.Services.Organiser
{
    public class OrganiserService
    {
        private readonly UserManager<User> _userManager;
        private readonly BulkBuydContext _context;        

        public OrganiserService(UserManager<User> userManager, BulkBuydContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Merchant> GetMerchant(string userId, bool isLive)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (string.IsNullOrEmpty(user.PinchMerchantId))
            {
                return null;
            }

            var api = new PinchApi(user.PinchMerchantId, user.PinchMerchantSecretKey, new PinchApiOptions()
            {
                IsLive = isLive
            });

            return await api.Merchant.GetMerchant();
        }

        public async Task<ServiceResponse<ManagedMerchant>> CreateMerchant(string userId, bool isLive, string pinchMerchantId, string pinchSecretKey)
        {
            var response = new ServiceResponse<ManagedMerchant>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (!string.IsNullOrEmpty(user.PinchMerchantId))
            {
                response.ErrorMessages.Add(new ValidationFailure("", "User already has a merchant"));
                return response;
            }
            if (string.IsNullOrEmpty(user.PinchPayerId))
            {
                response.ErrorMessages.Add(new ValidationFailure("paymentMethodMissing", "User does not have a payment method"));
                return response;
            }

            var api = new PinchApi(pinchMerchantId, pinchSecretKey, new PinchApiOptions()
            {
                IsLive = isLive
            });

            var payer = await api.Payer.Get(user.PinchPayerId);

            if (!payer.Success)
            {
                response.ErrorMessages.AddRange(payer.Errors.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)));
                return response;
            }

            var merchantResponse = await api.Merchant.CreateManagedMerchant(new ManagedMerchantCreateOptions()
            {
                CompanyName = $"BulkBuyd - {user.DisplayName}",
                StreetAddress = user.StreetAddress,
                Postcode = user.Postcode,
                Suburb = user.Suburb,
                Email = user.Email,
                AccountName = payer.Data.AccountName,
                AccountNumber = payer.Data.AccountNumber,
                BSB = payer.Data.BSB              
            });

            if (!merchantResponse.Success)
            {
                response.ErrorMessages.AddRange(merchantResponse.Errors.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)));
                return response;
            }

            user.PinchMerchantId = merchantResponse.Data.Id;
            user.PinchMerchantSecretKey = merchantResponse.Data.SecretKey;
            await _context.SaveChangesAsync();

            response.Data = merchantResponse.Data;
            return response;
        }

        public async Task<ServiceResponse<Merchant>> UpdateMerchant(string userId, bool isLive, string pinchMerchantId, string pinchSecretKey)
        {
            var response = new ServiceResponse<Merchant>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (string.IsNullOrEmpty(user.PinchMerchantId))
            {
                response.ErrorMessages.Add(new ValidationFailure("", "User does not have a merchant"));
                return response;
            }
            if (string.IsNullOrEmpty(user.PinchPayerId))
            {
                response.ErrorMessages.Add(new ValidationFailure("paymentMethodMissing", "User does not have a payment method"));
                return response;
            }

            var parentApi = new PinchApi(pinchMerchantId, pinchSecretKey, new PinchApiOptions()
            {
                IsLive = isLive
            });

            var managedApi = new PinchApi(user.PinchMerchantId, user.PinchMerchantSecretKey, new PinchApiOptions()
            {
                IsLive = isLive
            });

            var payer = await parentApi.Payer.Get(user.PinchPayerId);

            if (!payer.Success)
            {
                response.ErrorMessages.AddRange(payer.Errors.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)));
                return response;
            }

            var merchantResponse = await managedApi.Merchant.UpdateMerchant(new MerchantUpdateOptions()
            {
                CompanyName = $"BulkBuyd - {user.DisplayName}",
                StreetAddress = user.StreetAddress,
                Postcode = user.Postcode,
                Suburb = user.Suburb,
                Email = user.Email,
                AccountName = payer.Data.AccountName,
                AccountNumber = payer.Data.AccountNumber,
                BSB = payer.Data.BSB                
            });

            if (!merchantResponse.Success)
            {
                response.ErrorMessages.AddRange(merchantResponse.Errors.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)));
                return response;
            }

            response.Data = merchantResponse.Data;
            return response;
        }
    }
}
