
using ContactList.Core.Entities;
using ContactList.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.Contracts;


namespace ContactList.Api.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly IContactRepositoryAsync _contact;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IContactRepositoryAsync contact, IUnitOfWork unitOfWork, IRazorRenderService renderService)
        {
            _logger = logger;
            _contact = contact;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
        }
        public IEnumerable<Contact> Contacts { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Contacts = await _contact.GetAllAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<Contact>>(ViewData, Contacts)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new Contact()) });
            else
            {
                var thisContact = await _contact.GetByIdAsync(id);
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", thisContact) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _contact.AddAsync(contact);
                    await _unitOfWork.Commit();
                }
                else
                {
                    await _contact.UpdateAsync(contact);
                    await _unitOfWork.Commit();
                }
                Contacts = await _contact.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", Contacts);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", contact);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var customer = await _contact.GetByIdAsync(id);
            await _contact.DeleteAsync(customer);
            await _unitOfWork.Commit();
            Contacts = await _contact.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", Contacts);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
