using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    public class Result
    {
        //static string[] table = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public class Player
        {
            public float time;
            public int score;

            public Player(float time = 0, int score = 0)
            {
                this.time = time;
                this.score = score;
            }
        }

        static Player[] table = { new Player(), new Player(), new Player(), new Player(), new Player(), new Player(), new Player(), new Player(), new Player(), new Player() };

        public static void Place(Player args)
        {
            for (int i = 0; i < 10; i++)
            {
                if (table[i].score < args.score)
                {
                    Player temp = table[i];
                    table.SetValue(args, i);
                    Place(temp);
                    break;
                }
            }
        }

        static public void AddResult(Player args)
        {
            Place(args);
        }
        static public Player[] GetResult()
        {
            return table;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public Result.Player[] Get()
        {
            return Result.GetResult();
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Result.Player value)
        {
            
            Result.AddResult(value);
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
