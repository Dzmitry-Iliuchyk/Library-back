using FluentValidation;
using Library.Application.Auth.Interfaces;
using Library.Application.Implementations;
using Library.Application.Interfaces;
using Library.Application.Validator;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Library.Application.Tests {
    public class BookServiceTests {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ITokenService> _mockTokenService;
        private IValidator<User> _validator;
        private IPasswordHasher<User> _hasher;
        private UserService _userService;
        [SetUp]
        public void SetUp() {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTokenService = new Mock<ITokenService>();
            _validator = new UserValidator();
            _hasher = new PasswordHasher<User>();
            _userService = new UserService( _mockUnitOfWork.Object, _validator, _hasher, _mockTokenService.Object );
        }
    }
}