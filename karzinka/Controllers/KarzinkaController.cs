using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using karzinka.Models;

namespace Karzinka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KarzinkaController : ControllerBase
    {
        private const string constr = "Host=localhost;Port=5432;Database=karzinka;Username=postgres;Password=1234";

        [HttpPost]
        public async Task<ActionResult<KarzinkaModel>> PostKarzinkaAsync(KarzinkaModel karzinka)
        {
            using var connection = new NpgsqlConnection(constr);

            var insertSql = """
                INSERT INTO product (id, customer_name, date)
                VALUES (@Id, @Customer_name, @Date);
            """;

            await connection.ExecuteAsync(insertSql, karzinka);
            return Ok(karzinka);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<KarzinkaModel>> UpdateKarzinkaAsync(KarzinkaModel karzinka)
        {
            using var connection = new NpgsqlConnection(constr);

            var updateSql = """
                UPDATE product
                SET customer_name = @Customer_name,
                    date = @Date
                WHERE id = @Id;
            """;

            int affectedRows = await connection.ExecuteAsync(updateSql, karzinka);
            if (affectedRows == 0)
                return NotFound();

            return Ok(karzinka);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteKarzinkaAsync(int id)
        {
            using var connection = new NpgsqlConnection(constr);

            var deleteSql = "DELETE FROM product WHERE id = @Id;";

            int rows = await connection.ExecuteAsync(deleteSql, new { Id = id });
            if (rows == 0)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KarzinkaModel>>> GetAllKarzinkaAsync()
        {
            using var connection = new NpgsqlConnection(constr);

            var selectSql = "SELECT * FROM product;";
            var result = await connection.QueryAsync<KarzinkaModel>(selectSql);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KarzinkaModel>> GetKarzinkaByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(constr);

            var sql = "SELECT * FROM product WHERE id = @Id;";
            var result = await connection.QueryFirstOrDefaultAsync<KarzinkaModel>(sql, new { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
