using System.Net;
using WomenMarket.App.Models.ViewModels;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Models.BindingModels;
    using WomenMarket.Models;

    public class FarmsController : BaseAdminController
    {
        public FarmsController(IWomenMarketData data) 
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FarmBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var farm = new Farm()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Email = model.Email,
                    ImageUrl = model.ImageUrl,
                    PhoneNumber = model.PhoneNumber
                };

                this.Data.Farms.Add(farm);
                this.Data.SaveChanges();
            }

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Farm farm = this.Data.Farms.Find(id);

            FarmViewModel viewModel = new FarmViewModel()
            {
                Id = farm.Id,
                Name = farm.Name,
                Address = farm.Address,
                PhoneNumber = farm.PhoneNumber,
                ImageUrl = farm.ImageUrl,
                Description = farm.Description,
                Email = farm.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FarmBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Farm farm = this.Data.Farms.Find(model.Id);
                farm.Name = model.Name;
                farm.Description = model.Description;
                farm.ImageUrl = model.ImageUrl;
                farm.Address = model.Address;
                farm.Email = model.Email;
                farm.PhoneNumber = model.PhoneNumber;

                this.Data.SaveChanges();
            }

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Farm farm = this.Data.Farms.Find(id);

            if (farm == null)
            {
                return HttpNotFound();
            }

            FarmViewModel viewModel = new FarmViewModel()
            {
                Id = farm.Id,
                Name = farm.Name,
                Description = farm.Description,
                Address = farm.Address,
                Email = farm.Email,
                PhoneNumber = farm.PhoneNumber,
                ImageUrl = farm.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Farm farm = this.Data.Farms.Find(id);

            this.Data.Farms.Remove(farm);
            this.Data.SaveChanges();

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }
    }
}