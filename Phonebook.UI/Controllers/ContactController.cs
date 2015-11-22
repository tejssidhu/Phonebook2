using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
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

        public ActionResult Index(Guid UserId)
        {
            var contacts = _contactService.GetAllByUserId(UserId);
            ViewBag.UserId = UserId;

            return View(contacts);
        }

        public ActionResult ManageContact(Guid ContactId, Guid UserId)
        {
            if (ContactId != Guid.Empty)
                return View(_contactService.Get(ContactId));
            else
                return View(new Contact { Id = Guid.Empty, UserId = UserId });
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

            return RedirectToAction("Index", new { UserId = contact.UserId });
        }

        public ActionResult DeleteContact(Guid ContactId, Guid UserId)
        {
            _contactService.Delete(ContactId);

            return RedirectToAction("Index", new { UserId = UserId });
        }
    }
}
