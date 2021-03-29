using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public static class Settings
    {
        private static readonly string secret = "fedaf7d8863b48e197b9287d492b708e";
        public static string Secret => secret;
    }
}
