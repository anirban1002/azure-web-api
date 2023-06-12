using Azure.Security.KeyVault.Secrets;

namespace azure_web_api.Data
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;

        public KeyVaultManager(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            try
            {
                KeyVaultSecret keyValueSecret = await
                _secretClient.GetSecretAsync(secretName);
                return keyValueSecret.Value;
            }
            catch
            {
                throw;
            }
        }
        public string GetSecret(string secretName)
        {
            try
            {
                KeyVaultSecret keyValueSecret =
                _secretClient.GetSecret(secretName);
                return keyValueSecret.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}
