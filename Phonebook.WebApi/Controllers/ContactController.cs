using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Filters;

namespace Phonebook.WebApi.Controllers
{
	[ApiAuthenticationFilter]
    public class ContactController : ApiController
    {
		private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        // GET api/<controller>/7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf
		public IHttpActionResult GetUser(Guid id) 
        {
            User user = _userService.Get(id);

            if (user != null)
            {
				return Ok(_contactService.GetAllByUserId(id));
            }

			return Unauthorized();
        }

        // GET api/<controller>/7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf/81c4763c-b225-4756-903a-750064167813
		public IHttpActionResult GetContact(Guid id, Guid itemId)
        {
            User user = _userService.Get(id);

            if (user != null)
            {
				var contact = _contactService.Get(itemId);

				if (contact == null)
				{
					return NotFound();
				}

				return Ok(contact);             
            }

			return Unauthorized();
        }

        // POST api/<controller>
		public IHttpActionResult Post([FromBody]Contact newContact)
        {
            var httpResponse = new HttpResponseMessage();

            if (ModelState.IsValid)
            {
                User user = _userService.Get(newContact.UserId);

                if (user != null)
                {
                    try
                    {
                        _contactService.Create(newContact);

						return CreatedAtRoute("DefaultDoubleGuidApi", new { id = newContact.UserId, itemId = newContact.Id}, newContact);
                    }
                    catch (Exception ex)
                    {
						return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
				return BadRequest(ModelState);
            }

			return Unauthorized();
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody]Contact existingContact)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != existingContact.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var httpResponse = new HttpResponseMessage();

            User user = _userService.Get(existingContact.UserId);

            if (user != null)
            {
                try
                {
                    Contact contact = _contactService.Get(existingContact.Id);

                    if (contact != null)
                    {
                        _contactService.Update(existingContact);

                        httpResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                        httpResponse.StatusCode = HttpStatusCode.NotFound;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            else
            {
                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            }

            return httpResponse;
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(Guid id)
        {
            Contact contact = _contactService.Get(id);

            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                _contactService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, contact);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _contactService.Dispose();
            _userService.Dispose();

            base.Dispose(disposing);
        }
    }
}
