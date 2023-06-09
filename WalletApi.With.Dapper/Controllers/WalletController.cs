using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WalletApi.With.Dapper.Models;

namespace WalletApi.With.Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IConfiguration _Configuration;
        public WalletController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        //Get all wallets
        [HttpGet("Getwallets")]
        public async Task<IActionResult> Getwallets()
        {
            var sql = "Select * from Wallet";
            using (var connection = new SqlConnection(_Configuration.GetConnectionString("Conn")))
            {
                var wallets = await connection.QueryAsync<WalletModel>(sql);
                return Ok(wallets);
            };

        }


        [HttpGet("Getwallet/{id}")]
        public async Task<IActionResult> Getwallet(int id)
        {
            var sql = $"Select * from Wallet where id={id}";
            using (var connection = new SqlConnection(_Configuration.GetConnectionString("Conn")))
            {
                var wallet = await connection.QueryFirstOrDefaultAsync<WalletModel>(sql);
                return Ok(wallet);
            };

        }


        [HttpPost("Addwallet")]
        public async Task<IActionResult> Addwallet(AddWalletModel wallet)
        {
            string sql = $"insert into Wallet(Name,Type,AccountNumber,AccountScheme,CreatedAt,Owner)Values('{wallet.Name}','{wallet.Type}','{wallet.AccountNumber}','{wallet.AccountScheme}','{DateTime.Now.ToShortDateString()}','{wallet.Owner}')";
            using (var connection = new SqlConnection(_Configuration.GetConnectionString("Conn")))
            {
                await connection.ExecuteAsync(sql);
                return Ok();
            };

        }

        [HttpPut("Updatewallet")]
        public async Task<IActionResult> Updatewallet(WalletModel wallet)
        {
            string sql = $"update Wallet set Name='{wallet.Name}',Type = '{wallet.Type}',AccountNumber = '{wallet.AccountNumber}',AccountScheme = '{wallet.AccountScheme}',Owner='{wallet.Owner}' where Id='{wallet.Id}'";
            using (var connection = new SqlConnection(_Configuration.GetConnectionString("Conn")))
            {
                await connection.ExecuteAsync(sql);
                return Ok();
            };

        }


        [HttpDelete("Deletewallet/{id}")]
        public async Task<IActionResult> Deletewallet(int id)
        {
            string sql = $"Delete from Wallet where Id={id}";
            using (var connection = new SqlConnection(_Configuration.GetConnectionString("Conn")))
            {
                await connection.ExecuteAsync(sql);
                return Ok();
            };

        }


    }
}
