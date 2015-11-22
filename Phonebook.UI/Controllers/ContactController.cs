using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public ActionResult Index(Guid userId)
        {
            var contacts = _contactService.GetAllByUserId(userId);
            ViewBag.UserId = userId;

            return View(contacts);
        }

        public ActionResult ManageContact(Guid contactId, Guid userId)
        {
            if (contactId != Guid.Empty)
                return View(_contactService.Get(contactId));
            else
                return View(new Contact { Id = Guid.Empty, UserId = userId });
        }

        [HttpPost]
        public ActionResult ManageContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            if (contact.Id != Guid.Empty)
            {
                try
                {
                    _contactService.Update(contact);
                }
                catch (ObjectAlreadyExistException oae)
                {
                    ModelState.AddModelError("Email", oae.Message);
                    return View(contact);
                }
            }
            else
            {
                try
                {
                    _contactService.Create(contact);
                }
                catch (ObjectNotFoundException onf)
                {
                    ModelState.AddModelError("User", onf.Message);
                    return View(contact);
                }
                catch (ObjectAlreadyExistException oae)
                {
                    ModelState.AddModelError("Email", oae.Message);
                    return View(contact);
                }
            }

            return RedirectToAction("Index", new {contact.UserId });
        }

        public ActionResult DeleteContact(Guid contactId, Guid userId)
        {
            _contactService.Delete(contactId);

            return RedirectToAction("Index", new { UserId = userId });
        }
    }
}
