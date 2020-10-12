using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.DAC;
using WebAPIDemo.Entities;

namespace WebAPIDemo.Controllers
{
    [Route("mycompany/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        // GET: api/Contact
        [Route("TestRoute")]
        [HttpGet]
        public IEnumerable<Contact> Get([FromQuery] int id,string name)
        {
            ContactDAC contactDAC = new ContactDAC();
            contactDAC.GetContacts();
            return contactDAC.GetContacts();
        }

        [HttpGet("{id}")]
        public Contact Get(int id)
        {
            ContactDAC contactDAC = new ContactDAC();
            return contactDAC.GetContactsByID(id);
        }

        // POST: api/Contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            ContactDAC contactDAC = new ContactDAC();

            if (contactDAC.AddContact(contact) > 0)
                return Ok();
            else
                return BadRequest();

        }


    }
}
