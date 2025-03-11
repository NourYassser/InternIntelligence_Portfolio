using InternIntelligence_Portfolio.Dtos.Contacts;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContact_FormService _Contactservice;

        public ContactsController(IContact_FormService Contactservice)
        {
            _Contactservice = Contactservice;
        }
        [HttpGet("GetContacts")]
        public IActionResult GetAll()
        {
            return Ok(_Contactservice.GetAll());
        }

        [HttpGet("GetContactsById")]
        public IActionResult GetById(int id)
        {
            return Ok(_Contactservice.GetById(id));
        }

        [HttpPost("AddContacts")]
        public IActionResult Add(ContactsDto contact)
        {
            _Contactservice.Add(contact);
            return NoContent();
        }

        [HttpPut("EditContacts")]
        public IActionResult UpdateContacts(ContactsDto dtos)
        {
            var Contacts = _Contactservice.GetById(dtos.Id);
            if (Contacts == null)
            {
                return BadRequest();
            }
            _Contactservice.Update(dtos);
            return NoContent();
        }

        [HttpDelete("DeleteContacts")]
        public IActionResult DeleteContacts(int id)
        {
            _Contactservice.Delete(id);
            return NoContent();
        }
    }
}
