using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    public class ContactNumberController : Controller
    {
        private readonly IContactNumberService _contactNumberService;
        private readonly IContactService _contactService;

        public ContactNumberController(IContactNumberService contactNumberService, IContactService contactService)
        {
            _contactNumberService = contactNumberService;
            _contactService = contactService;
        }

        public ActionResult Index(Guid ContactId)
        {
            var contactNumbers = _contactNumberService.GetAllByContactId(ContactId);

            var contact = _contactService.Get(ContactId);
            ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;
            ViewBag.ContactId = contact.Id;
            ViewBag.UserId = contact.UserId;

            return View(contactNumbers);
        }

        public ActionResult ManageContactNumber(Guid ContactNumberId, Guid ContactId)
        {
            var contact = _contactService.Get(ContactId);
            ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

            if (ContactNumberId != Guid.Empty)
                return View(_contactNumberService.Get(ContactNumberId));
            else
                return View(new ContactNumber { Id = Guid.Empty, ContactId = ContactId });
        }

        [HttpPost]
        public ActionResult ManageContactNumber(ContactNumber contactNumber)
        {
            if (!ModelState.IsValid)
            {
                return View(contactNumber);
            }

            if (contactNumber.Id != Guid.Empty)
            {
                try
                {
                    _contactNumberService.Update(contactNumber);
                }
                catch (ObjectAlreadyExistException oae)
                {
                    var contact = _contactService.Get(contactNumber.ContactId);
                    ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

                    ModelState.AddModelError("ContactNumber", oae.Message);
                    return View(contactNumber);
                }
            }
            else
            {
                try
                {
                    _contactNumberService.Create(contactNumber);
                }
                catch (ObjectNotFoundException onf)
                {
                    var contact = _contactService.Get(contactNumber.ContactId);
                    ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

                    ModelState.AddModelError("TelephoneNumber", onf.Message);
                    return View(contactNumber);
                }
                catch (ObjectAlreadyExistException oae)
                {
                    var contact = _contactService.Get(contactNumber.ContactId);
                    ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

                    ModelState.AddModelError("TelephoneNumber", oae.Message);
                    return View(contactNumber);
                }
            }

            return RedirectToAction("Index", new { ContactId = contactNumber.ContactId });
        }

        public ActionResult DeleteContactNumber(Guid ContactNumberId, Guid ContactId)
        {
            _contactNumberService.Delete(ContactNumberId);

            return RedirectToAction("Index", new { ContactId = ContactId });
        }
    }
}
