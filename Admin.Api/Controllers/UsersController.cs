using Admin.Shared;
using Admin.Shared.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameter, CancellationToken cancellationToken = default)
        {
            try
            {
                //var users = await _userManager.Users.ToListAsync(cancellationToken);
                var users = GetMockUsers();

                return Ok(new PagedList<User>(users.ToList(), users.Count(), pagingParameter.Page, pagingParameter.PageSize));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load users");
                return Problem("Failed to load users", e.Message, StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        #region Mocking

        private IEnumerable<User> GetMockUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user1",
                    Email = "test@test.nl",
                    FirstName = "Test",
                    LastName = "Test1"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user2",
                    Email = "test@2test.nl",
                    FirstName = "Test",
                    LastName = "Test1"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user3",
                    Email = "test@3test.nl",
                    FirstName = "Test",
                    LastName = "Test3"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user4",
                    Email = "test@4test.nl",
                    FirstName = "Test",
                    LastName = "Test4"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user5",
                    Email = "test@5test.nl",
                    FirstName = "Test",
                    LastName = "Test5"
                },
                    new User
                    {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user6",
                     Email = "test@6test.nl",
                    FirstName = "Test",
                    LastName = "Test6"
                },
                    new User
                    {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user7",
                     Email = "test@7test.nl",
                    FirstName = "Test",
                    LastName = "Test7"
                },
                    new User
                    {
                    Id = Guid.NewGuid().ToString(),
                    Email = "test@8test.nl",
                    UserName = "user8",
                    FirstName = "Test",
                    LastName = "Test8"
                },
                    new User
                    {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user9",
                      Email = "test@9test.nl",
                    FirstName = "Test",
                    LastName = "Test9"
                },
                    new User
                    {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user10",
                     Email = "test@10test.nl",
                    FirstName = "Test",
                    LastName = "Test10"
                }
            };
        }

        #endregion
    }
}
