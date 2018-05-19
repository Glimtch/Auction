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
        public async Task<ActionResult> CreateLot(RegisterLotViewModel lot/*, HttpPostedFile image*/)
        {
            //if (image != null)
            //{
            //    lot.Image = new byte[image.ContentLength];
            //    image.InputStream.Read(lot.Image, 0, image.ContentLength);
            //}
            DateTime expire = lot.ExpireDate;
            expire += lot.ExpireTime.TimeOfDay;
            if (expire < DateTime.Now + new TimeSpan(24, 0, 0))
            {
                ModelState.AddModelError("ExpireDate", "Expire date and time must be more than 24 hours from now");
                return View(lot);
            }
            await lotsService.CreateLotAsync(new LotDTO()
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
        public async Task<ActionResult> DetailsPartial(string lotJson, decimal? bid)
        {
            var lot = JsonConvert.DeserializeObject<DetailedLotViewModel>(lotJson);
            if (bid == null || bid <= lot.CurrentPrice)
            {
                ModelState.AddModelError("", "Bid price must be more than current");
            }
            else if(bid > 1000000)
            {
                ModelState.AddModelError("", "Bid price cannot be higher than $1'000'000");
            }
            else
            {
                lot.CurrentPrice = (decimal)bid;
                lot.BidderId = User.Identity.GetUserId();
                try
                {
                    await lotsService.UpdateBidAsync(
                        new LotDTO()
                        {
                            Id = lot.Id,
                            Name = lot.Name,
                            Description = lot.Description,
                            Image = lot.Image,
                            StartPrice = lot.StartPrice,
                            SellerId = lot.SellerId,
                            ExpireDate = lot.ExpireDate,
                            CurrentPrice = lot.CurrentPrice,
                            BidderId = lot.BidderId
                        });
                    ViewBag.Message = "Bid successfuly made!";
                }
                catch(Exception e)
                {
                    ViewBag.Message = e.Message;
                }
            }
            return PartialView("DetailsPartial", lot);
        }
    }
}