using Auction.BLL.DTOs;
using Auction.BLL.Interfaces;
using Auction.WEB.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auction.WEB.Controllers
{
    public class AuctionController : Controller
    {
        ILotsService lotsService;
        IUsersService usersService;

        public AuctionController(ILotsService auctionService, IUsersService userService)
        {
            this.lotsService = auctionService;
            this.usersService = userService;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var lotDtos = await lotsService.GetAllActiveLotsAsync();
            var lots = new List<DetailedLotViewModel>();
            foreach (var lotDto in lotDtos)
            {
                lots.Add(new DetailedLotViewModel()
                {
                    Id = lotDto.Id,
                    Name = lotDto.Name,
                    Description = lotDto.Description,
                    Image = lotDto.Image,
                    StartPrice = lotDto.StartPrice,
                    CurrentPrice = lotDto.CurrentPrice,
                    SellerId = lotDto.SellerId,
                    ExpireDate = lotDto.ExpireDate
                });
            }
            return View(lots);
        }

        //[ChildActionOnly]
        [HttpPost]
        public async Task<ActionResult> IndexPartial(string pattern)
        {
            var lotDtos = await lotsService.SearchActiveLotsAsync(pattern);
           // if(lotDtos.Count == 0)
           //     return
            var lots = new List<DetailedLotViewModel>();
            foreach (var lotDto in lotDtos)
            {
                lots.Add(new DetailedLotViewModel()
                {
                    Id = lotDto.Id,
                    Name = lotDto.Name,
                    Description = lotDto.Description,
                    Image = lotDto.Image,
                    StartPrice = lotDto.StartPrice,
                    CurrentPrice = lotDto.CurrentPrice,
                    SellerId = lotDto.SellerId,
                    ExpireDate = lotDto.ExpireDate
                });
            }
            return PartialView(lots);
        }

        [Authorize(Roles = "user")]
        public ActionResult CreateLot()
        {
            return View(new RegisterLotViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> CreateLot(RegisterLotViewModel lot)
        {
            if (lot.ImageFile != null)
            {
                if (lot.ImageFile.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2 MB");
                    return View();
                }
                if (!(lot.ImageFile.ContentType == "image/jpeg" || lot.ImageFile.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("ImageFile", "File type allowed : jpeg and gif");
                    return View();
                }

                byte[] data = new byte[lot.ImageFile.ContentLength];
                lot.ImageFile.InputStream.Read(data, 0, lot.ImageFile.ContentLength);
                lot.Image = data;
            }

            DateTime expire = lot.ExpireDate;
            expire += lot.ExpireTime.TimeOfDay;
            if (expire < DateTime.Now + new TimeSpan(0, 0, 0))
            {
                ModelState.AddModelError("ExpireDate", "Expire date and time must be more than 24 hours from now");
                return View(lot);
            }
            await lotsService.CreateOrUpdateLotAsync(new LotDTO()
            {
                Name = lot.Name,
                Description = lot.Description,
                Image = lot.Image,
                StartPrice = lot.StartPrice,
                SellerId = User.Identity.GetUserId(),
                ExpireDate = expire
            });
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var lotDto = await lotsService.GetLotByIdAsync(id);
            return View(new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                StartPrice = lotDto.StartPrice,
                CurrentPrice = lotDto.CurrentPrice,
                SellerId = lotDto.SellerId,
                SellerNickname = (await usersService.GetUserByIdAsync(lotDto.SellerId)).Nickname,
                ExpireDate = lotDto.ExpireDate,
                BidderId = lotDto.BidderId
            });
        }

        [HttpPost]
        public async Task<ActionResult> DetailsPartial(int id, decimal current, decimal? bid)
        {
            LotDTO lotDto = null;
            if (bid == null || bid <= current)
            {
                ModelState.AddModelError("", "Bid price must be more than current");
            }
            else if (bid > 1000000)
            {
                ModelState.AddModelError("", "Bid price cannot be higher than $1'000'000");
            }
            else
            {
                try
                {
                    lotDto = await lotsService.UpdateBidAsync(id, (decimal)bid, User.Identity.GetUserId());
                    ViewBag.Message = "Bid successfuly made!";
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                }
            }
            if (lotDto == null)
                lotDto = await lotsService.GetLotByIdAsync(id);
            return PartialView("DetailsPartial", new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                StartPrice = lotDto.StartPrice,
                CurrentPrice = lotDto.CurrentPrice,
                SellerId = lotDto.SellerId,
                SellerNickname = (await usersService.GetUserByIdAsync(lotDto.SellerId)).Nickname,
                ExpireDate = lotDto.ExpireDate,
                BidderId = lotDto.BidderId
            });
        }

        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            var lotDto = await lotsService.GetLotByIdAsync(id);
            if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                return RedirectToActionPermanent("Details", new { id = id });
            return View(new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                StartPrice = lotDto.StartPrice,
                CurrentPrice = lotDto.CurrentPrice,
                SellerId = lotDto.SellerId,
                ExpireDate = lotDto.ExpireDate,
                BidderId = lotDto.BidderId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DetailedLotViewModel lot)
        {
            if (lot.ImageFile != null)
            {
                if (lot.ImageFile.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2 MB");
                    return View(lot);
                }
                if (!(lot.ImageFile.ContentType == "image/jpeg" || lot.ImageFile.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("ImageFile", "File type allowed : jpeg and gif");
                    return View(lot);
                }

                byte[] data = new byte[lot.ImageFile.ContentLength];
                lot.ImageFile.InputStream.Read(data, 0, lot.ImageFile.ContentLength);
                lot.Image = data;
            }

            await lotsService.CreateOrUpdateLotAsync(new LotDTO()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                BidderId = lot.BidderId,
                CurrentPrice = lot.CurrentPrice,
                Image = lot.Image,
                ExpireDate = lot.ExpireDate,
                StartPrice = lot.StartPrice,
                SellerId = lot.SellerId
            });
            return RedirectToAction("Details", new { id = lot.Id });
        }

        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var lotDto = await lotsService.GetLotByIdAsync(id);
            if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                return RedirectToActionPermanent("Details", new { id = id });
            return View(new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                CurrentPrice = lotDto.CurrentPrice,
                ExpireDate = lotDto.ExpireDate
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await lotsService.DeleteLotAsync(id);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index");
        }
    }
}