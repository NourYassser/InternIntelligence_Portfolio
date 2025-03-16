using InternIntelligence_Portfolio.Dtos.Contacts;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var cs = _Contactservice.GetById(id);
            if (cs == null)
            {
                return NotFound();
            }
            return Ok(cs);
        }

        [HttpPost("AddContacts")]
        public IActionResult Add(ContactsDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _Contactservice.Add(contact);
            return NoContent();
        }

        [HttpPut("EditContacts")]
        public IActionResult UpdateContacts(int Id, ContactsDto dtos)
        {
            var Contacts = _Contactservice.GetById(Id);
            if (Contacts == null)
            {
                return BadRequest();
            }
            _Contactservice.Update(Id, dtos);
            return NoContent();
        }

        [HttpDelete("DeleteContacts")]
        public IActionResult DeleteContacts(int id)
        {
            try
            {
                _Contactservice.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
