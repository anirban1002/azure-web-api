namespace azure_web_api.Data
{
    public interface IKeyVaultManager
    {
        public Task<string> GetSecretAsync(string secretName);
        public string GetSecret(string secretName);
    }
}
