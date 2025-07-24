using Dapper;
using karzinka.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace karzinka.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private const string constr = "Host=localhost;Port=5432;Database=karzinka;Username=postgres;Password=1234";
        [HttpPost]

        public async Task<ActionResult<Productdet>> PostProductdetAsync(Productdet productdet)
        {
            using var connection = new NpgsqlConnection(constr);

            var insertSql = """
                INSERT INTO product (id, product_id, product, unit_price, quantity)
                VALUES (@Id, @Product_id, @Product, @Unit_price, @Quantity);
            """;

            await connection.ExecuteAsync(insertSql, productdet);
            return Ok(productdet);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Productdet>> PutProductdetAsync(Productdet productdet)
        {
            using var connection = new NpgsqlConnection(constr);

            var updateSql = """
                UPDATE product_details
                SET product = @Product,
                    unit_price = @Unit_price
                WHERE id = @Id;
            """;


            DynamicParameters dynm = new DynamicParameters();
            dynm.Add("Id", productdet.Id);
            dynm.Add("Product", productdet.Product);
            dynm.Add("Unit_Price", productdet.Unit_price);
            dynm.Add("Quantity", productdet.Quantity);

            int row = await connection.ExecuteAsync(updateSql, dynm);
            if (row == 0)
            {
                return NotFound();
            }
            return Ok(productdet);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Productdet>> DeleteProductdetAsync(int id)
        {
            using var connection = new NpgsqlConnection(constr);

            var deletesql = "delete * from product_details where id=@Id";
            int rows = await connection.ExecuteAsync(deletesql, new { Id = id });
            if (rows == 0)
                return NotFound();

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KarzinkaModel>>> GetAllKarzinkaAsync()
        {
            using var connection = new NpgsqlConnection(constr);

            var selectSql = "SELECT * FROM product_details;";
            var result = await connection.QueryAsync<KarzinkaModel>(selectSql);

            return Ok(result);
        }
    }

}

