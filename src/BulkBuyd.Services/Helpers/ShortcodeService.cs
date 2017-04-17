using BulkBuyd.Services.Helpers;
using System;
using BulkBuyd.Domain.Services;

namespace BulkBuyd.Services.Helpers
{
    public class ShortcodeService : IShortcodeService
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly IRandomService _randomService;

        public ShortcodeService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public string GenerateId(string prefix, int length = 14)
        {
            return prefix + GenerateShortcode(length);
        }

        public string GenerateShortcode(int length = 14)
        {
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = _chars[_randomService.Next(_chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
