using System;

namespace Bunq2Ynab
{
    public static class Settings
    {
        public static string YnabApiKey => GetEnvironmentVariable("YnabApiKey");
        public static string YnabBudgetGuid => GetEnvironmentVariable("YnabBudgetGuid");
        public static string BunqApiKey => GetEnvironmentVariable("BunqApiKey");
        public static string PayeeTransforms => GetEnvironmentVariable("PayeeTransforms");
        public static string MoneyTaryAccountId => GetEnvironmentVariable("MoneyTaryAccountId");

        public static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
