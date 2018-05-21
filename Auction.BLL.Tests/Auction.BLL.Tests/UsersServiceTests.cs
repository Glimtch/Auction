using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.BLL.Interfaces;
using Auction.BLL.Services;
using Auction.DAL.EF;
using Auction.DAL.Entities;
using Auction.DAL.Repositories;
using Auction.DAL.Repositories.Identity;
using Moq;
using NUnit.Framework;
using Microsoft.AspNet.Identity.EntityFramework;
using Auction.BLL.Exceptions;
using Auction.BLL.DTOs;

namespace Auction.BLL.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        Mock<IdentityUnitOfWork> uow;
        Mock<AuctionUserManager> userManager;
        Mock<AuctionRoleManager> roleManager;
        IUsersService service;

        [SetUp]
        public void SetUp()
        {
            uow = new Mock<IdentityUnitOfWork>("DefaultConnection");
            userManager = new Mock<AuctionUserManager>(new UserStore<AuctionUser>());
            roleManager = new Mock<AuctionRoleManager>(new RoleStore<AuctionRole>());

            uow.Setup(u => u.UserManager).Returns(userManager.Object);
            uow.Setup(u => u.RoleManager).Returns(roleManager.Object);
            uow.Setup(u => u.SaveAsync()).Returns(Task.Run(() => { }));

            service = new UsersService(uow.Object);
        }

        [Test]
        public void CreateAsync_ProvidingNull_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(null));
        }

        [Test]
        public void CreateAsync_ProvidingInvalid_ThrowsUsersManagement()
        {
            Assert.ThrowsAsync<UsersManagementException>(() => service.CreateAsync(new UserDTO() { Email = "123@123@123", Password = "+-" }));
        }

        [Test]
        public void AuthenticateAsync_ProvidingNull_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AuthenticateAsync(null));
        }

        [Test]
        public void AuthenticateAsync_ProvidingInvalid_ThrowsUsersManagement()
        {
            Assert.ThrowsAsync<UsersManagementException>(() => service.AuthenticateAsync(new UserDTO() { Email = "123@123.123", Password = "Qwe123" }));
        }

        [Test]
        public void GetUserByIdAsync_ProvidingNull_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetUserByIdAsync(null));
        }

        [Test]
        public void GetUserByIdAsync_ProvidingInvalid_ThrowsUsersManagement()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetUserByIdAsync("1234"));
        }

        [Test]
        public void ChangeUserRoleAsync_ProvidingNullId_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.ChangeUserRoleAsync(null, "1", "2"));
        }

        [Test]
        public void ChangeUserRoleAsync_ProvidingNullRole_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.ChangeUserRoleAsync("id", "1", null));
        }

        [Test]
        public void ChangeUserRoleAsync_ProvidingEmptyRole_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.ChangeUserRoleAsync("id", "", "2"));
        }

        [Test]
        public void ChangeUserRoleAsync_ProvidingInvalidId_ThrowsUsersManagement()
        {
            Assert.ThrowsAsync<UsersManagementException>(() => service.ChangeUserRoleAsync("id", "1", "2"));
        }
    }
}
