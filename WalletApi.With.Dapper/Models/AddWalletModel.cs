namespace WalletApi.With.Dapper.Models
{
    public class AddWalletModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        public string Owner { get; set; }
    }
}
