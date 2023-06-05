using AutoMapper;
using Daily_Status_Report_task.Models;
using Daily_Status_Report_task.Models.DTO;
using Daily_Status_Report_task.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Status_Report_task.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IEnumerable<UserTable> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserTable userTable)
        {
            if (ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(userTable.Email, userTable.Name);
                if (!isUniqueUser)
                    return BadRequest("UserName Is In Use");
                var userInfo = _userRepository.Register(userTable.Email, userTable.Password,
                    userTable.Name, userTable.Address,userTable.Department_Id,userTable.Role_Id);

                if (userInfo == null) return BadRequest(); //400
                else
                {
                    if (userInfo.Email != null)
                    {
                        _emailSenderRepository.SendEmailAsync(userTable.Email, "E-Mail Confirmation",
                                  $"Hi,{userTable.Name} your E-Mail is successfully registered,Kindly visit again ", userInfo.Id);
                    }
                }
            }
            return Ok();  //200
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserVM userVM)
        {
            var user = _userRepository.Authenticate(userVM.Email, Encryption(userVM.Password));
            if (user == null) return BadRequest("Wrong Username & Password please enter valid Usename & Password");  //400
            return Ok(user); //200
        }

        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromBody] UserTableDto userTableDto)
        {
            if (userTableDto == null) return BadRequest(ModelState);
            var User = _mapper.Map<UserTableDto, UserTable>(userTableDto);
            _userRepository.UpdateUser(User);
            return Ok(User);
        }

        [HttpPut("updateUserDetail")]
        public IActionResult UpdateUserDetail([FromBody] UserTablesDto userTablesDto)
        {
            if (userTablesDto == null) return BadRequest(ModelState);
            var User = _mapper.Map<UserTablesDto, UserTable>(userTablesDto);
            _userRepository.UpdateUsers(User);
            return Ok(User);
        }

        [HttpDelete("{id:int}")]
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public static string Encryption(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;
            else
            {
                byte[] storepassword = Encoding.ASCII.GetBytes(password);
                string encryption = Convert.ToBase64String(storepassword);
                return encryption;
            }
        }
    }
}
