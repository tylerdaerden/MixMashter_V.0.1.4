//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;


////Classe temporaire , laissée pour pédagogie , de test de connexion à la DB avant de lancer la migration afin d'éviter n roll back inutile et gonflant , et comme on nous dis à l'école
////toujours tester !!! 

//namespace MixMashter.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DbTestController : ControllerBase
//    {
//        private readonly IConfiguration _config;

//        public DbTestController(IConfiguration config)
//        {
//            _config = config;
//        }

//        [HttpGet("ping")]
//        public IActionResult PingDatabase()
//        {
//            var connectionString = _config.GetConnectionString("DefaultConnection");

//            try
//            {
//                using var connection = new SqlConnection(connectionString);
//                connection.Open();

//                return Ok($"✅ Connexion réussie à la base , ça maaaaaaarche !!! : {connection.Database} sur {connection.DataSource}");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"❌ Erreur de connexion , ça craiiiiiiiint !!! : {ex.Message}");
//            }
//        }
//    }
//}
