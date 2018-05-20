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
using Auction.BLL.Exceptions;
<<<<<<< HEAD
using Auction.WEB.Services;
=======
<<<<<<< HEAD
using Auction.WEB.Services;
=======
<<<<<<< HEAD
using Auction.WEB.Services;
=======
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed

namespace Auction.WEB.Controllers
{
    public class AuctionController : Controller
    {
        ILotsService lotsService;

        public AuctionController(ILotsService lotsService)
        {
            this.lotsService = lotsService;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            await IndexPartial("");
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> IndexPartial(string pattern)
        {
            if (pattern == null)
                pattern = "";
            var lotDtos = await lotsService.SearchActiveLotsAsync(pattern);
            var lots = new List<DetailedLotViewModel>();
            foreach (var lotDto in lotDtos)
            {
<<<<<<< HEAD
                lots.Add(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
=======
<<<<<<< HEAD
                lots.Add(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
=======
<<<<<<< HEAD
                lots.Add(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
=======
                lots.Add(await DetailedLotFromLotDTO(lotDto));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
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
            if (lot == null)
            {
                ModelState.AddModelError("", "Invalid lot info");
                return View();
            }

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
                return View();
<<<<<<< HEAD
            }
            try
            {
                await lotsService.CreateLotAsync(new LotDTO()
                {
                    Name = lot.Name,
                    Description = lot.Description,
                    Image = lot.Image,
                    StartPrice = lot.StartPrice,
                    SellerId = User.Identity.GetUserId(),
                    ExpireDate = expire
                });
            }
            catch(Exception e) when (e is ArgumentException || e is LotsManagementException)
            {
=======
<<<<<<< HEAD
            }
            try
            {
                await lotsService.CreateLotAsync(new LotDTO()
                {
                    Name = lot.Name,
                    Description = lot.Description,
                    Image = lot.Image,
                    StartPrice = lot.StartPrice,
                    SellerId = User.Identity.GetUserId(),
                    ExpireDate = expire
                });
            }
            catch(Exception e) when (e is ArgumentException || e is LotsManagementException)
            {
=======
            }
            try
            {
                await lotsService.CreateLotAsync(new LotDTO()
                {
                    Name = lot.Name,
                    Description = lot.Description,
                    Image = lot.Image,
                    StartPrice = lot.StartPrice,
                    SellerId = User.Identity.GetUserId(),
                    ExpireDate = expire
                });
            }
<<<<<<< HEAD
            catch(Exception e) when (e is ArgumentException || e is LotsManagementException)
=======
            await lotsService.CreateLotAsync(new LotDTO()
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
            {
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
                ModelState.AddModelError("", e.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return HttpNotFound();
<<<<<<< HEAD
            try
            {
                var lotDto = await lotsService.GetLotByIdAsync((int)id);
                if (lotDto.ExpireDate < DateTime.Now &&
                    lotDto.BidderId != User.Identity.GetUserId() &&
                    lotDto.SellerId != User.Identity.GetUserId() &&
                    !User.IsInRole("admin"))
                    return HttpNotFound();
                return View(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
            }
            catch (LotsManagementException)
            {
                return HttpNotFound();
            }
=======
<<<<<<< HEAD
            try
            {
                var lotDto = await lotsService.GetLotByIdAsync((int)id);
                if (lotDto.ExpireDate < DateTime.Now &&
                    lotDto.BidderId != User.Identity.GetUserId() &&
                    lotDto.SellerId != User.Identity.GetUserId() &&
                    !User.IsInRole("admin"))
                    return HttpNotFound();
                return View(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
            }
            catch (LotsManagementException)
            {
                return HttpNotFound();
            }
=======
<<<<<<< HEAD
            try
            {
                var lotDto = await lotsService.GetLotByIdAsync((int)id);
                if (lotDto.ExpireDate < DateTime.Now &&
                    lotDto.BidderId != User.Identity.GetUserId() &&
                    lotDto.SellerId != User.Identity.GetUserId() &&
                    !User.IsInRole("admin"))
                    return HttpNotFound();
                return View(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
            }
            catch (LotsManagementException)
            {
                return HttpNotFound();
            }
=======
            var lotDto = await lotsService.GetLotByIdAsync((int)id);
            if (lotDto == null)
                return HttpNotFound();
            if(lotDto.ExpireDate < DateTime.Now &&
                lotDto.BidderId != User.Identity.GetUserId() &&
                lotDto.SellerId != User.Identity.GetUserId() &&
                !User.IsInRole("admin"))
                return HttpNotFound();
            return View(await DetailedLotFromLotDTO(lotDto));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }

        [HttpPost]
        public async Task<ActionResult> DetailsPartial(int id, decimal current, decimal? bid)
        {
            LotDTO lotDto = null;
            try
            {
                if (bid == null || bid <= current)
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
                {
                    ModelState.AddModelError("", "Bid price must be more than current");
                }
                else if (bid > 1000000)
<<<<<<< HEAD
=======
<<<<<<< HEAD
                {
                    ModelState.AddModelError("", "Bid price cannot be higher than $1'000'000");
                }
                else
                {
=======
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
                {
                    ModelState.AddModelError("", "Bid price cannot be higher than $1'000'000");
                }
                else
                {
<<<<<<< HEAD
=======
=======
                {
                    ModelState.AddModelError("", "Bid price must be more than current");
                }
                else if (bid > 1000000)
                {
                    ModelState.AddModelError("", "Bid price cannot be higher than $1'000'000");
                }
                else
                {
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
                    try
                    {
                        await lotsService.UpdateBidAsync(id, (decimal)bid, User.Identity.GetUserId());
                        ViewBag.Message = "Bid successfuly made!";
                    }
<<<<<<< HEAD
                    catch (LotsManagementException)
=======
<<<<<<< HEAD
                    catch (LotsManagementException)
=======
<<<<<<< HEAD
                    catch (LotsManagementException)
=======
                    catch (NotFoundException)
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
                    {
                        return HttpNotFound();
                    }
                }
            }
            catch (ExpiredException e)
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            {
                ViewBag.Message = e.Message;
            }
            try
            {
                lotDto = await lotsService.GetLotByIdAsync(id);
            }
            catch (LotsManagementException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return PartialView("DetailsPartial", LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            {
                ViewBag.Message = e.Message;
            }
            finally
            {
                lotDto = await lotsService.GetLotByIdAsync(id);
            }
            return PartialView("DetailsPartial", await DetailedLotFromLotDTO(lotDto));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            try
            {
                var lotDto = await lotsService.GetLotByIdAsync((int)id);
                if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                    return RedirectToActionPermanent("Details", new { id = id });
                return View(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
            }
            catch (LotsManagementException)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            var lotDto = await lotsService.GetLotByIdAsync((int)id);
            if(lotDto == null)
                return HttpNotFound();
            if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                return RedirectToActionPermanent("Details", new { id = id });
            return View(await DetailedLotFromLotDTO(lotDto));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DetailedLotViewModel lot)
        {
            if (lot == null)
            {
                ModelState.AddModelError("", "Invalid lot info");
                return View(lot);
            }

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

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            try
            {
                await lotsService.UpdateLotAsync(LotViewModelsMapper.LotDTOFromDetailedLot(lot));
            }
            catch (Exception e) when (e is ArgumentException || e is LotsManagementException)
            {
                ModelState.AddModelError("", e.Message);
                return View(lot);
            }
            catch (ExpiredException) { }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            await lotsService.UpdateLotAsync(LotDTOFromDetailedLot(lot));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            return RedirectToAction("Details", new { id = lot.Id });
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
                return HttpNotFound();
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            try
            {
                var lotDto = await lotsService.GetLotByIdAsync((int)id);
                if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                    return RedirectToActionPermanent("Details", new { id = id });
                return View(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
            }
            catch (LotsManagementException)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            var lotDto = await lotsService.GetLotByIdAsync((int)id);
            if (lotDto == null)
                return HttpNotFound();
            if (!User.IsInRole("admin") && User.Identity.GetUserId() != lotDto.SellerId)
                return RedirectToActionPermanent("Details", new { id = id });
            return View( await DetailedLotFromLotDTO(lotDto));
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await lotsService.DeleteLotAsync(id);
            return RedirectToAction("Index");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        }


        private LotDTO LotDTOFromDetailedLot(DetailedLotViewModel lot)
        {
            return new LotDTO()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                Image = lot.Image,
                StartPrice = lot.StartPrice,
                SellerId = lot.SellerId,
                ExpireDate = lot.ExpireDate,
                BidderId = lot.BidderId,
                CurrentPrice = lot.CurrentPrice
            };
        }

        private async Task<DetailedLotViewModel> DetailedLotFromLotDTO(LotDTO lotDto)
        {
            return new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                StartPrice = lotDto.StartPrice,
                SellerId = lotDto.SellerId,
                SellerNickname = (await usersService.GetUserByIdAsync(lotDto.SellerId)).Nickname,
                CurrentPrice = lotDto.CurrentPrice,
                ExpireDate = lotDto.ExpireDate,
                BidderId = lotDto.BidderId
            };
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }
    }
}