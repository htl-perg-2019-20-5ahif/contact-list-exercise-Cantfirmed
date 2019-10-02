using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ContactList.Controllers {
    [ApiController]
    [Route("api/contacts")]
    public class CLController : ControllerBase {
        private static List<Person> contactList = new List<Person>();
        [HttpGet]
        public IActionResult GetAllContacts() {
            return Ok(contactList);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Person newPerson) {
            contactList.Add(newPerson);
            return Created("", newPerson);
        }

        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateContact(int index, [FromBody] Person newPerson) {
            if (index >= 0 && index < contactList.Count) {
                contactList[index] = newPerson;
                return Ok();
            }
            return BadRequest("Invalid Index");
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteContact(int personId) {
            foreach (var contact in contactList) {
                if (contact.id.Equals(personId)) {
                    contactList.Remove(contact);
                    return NoContent();
                }
            }
            return BadRequest("Invalid Index");
        }

        [HttpGet]
        [Route("findByName")]
        public IActionResult findByName([FromQuery]String name) {
            if (string.IsNullOrEmpty(name)) {
                return BadRequest("Invalid or missing name");
            }
            return Ok(contactList.Where(person => person.firstName.Equals(name) || person.lastName.Equals(name)));
        }
    }
}
