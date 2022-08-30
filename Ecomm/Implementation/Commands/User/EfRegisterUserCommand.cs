    using Application.Commands.User;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Implementation.Validators;
using Application.DataTransfer;
using Application.DataTransfer.User;
using FluentValidation;
using Domain;
using System.Security.Cryptography;

namespace Implementation.Commands.User
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        private readonly Context _context;
        private readonly UserEntityValidator _validator;

        public EfRegisterUserCommand(Context context, UserEntityValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Create a User";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(request.Password);
            var md5Data = md5.ComputeHash(bytes);
            var hashedPassword = Convert.ToBase64String(md5Data); 

            request.Password = hashedPassword; 

            var user = new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                CreatedAt = DateTime.Now
            };

            if (request.Street != null && request.ZipCode != null)
            {
                user.Addresses.Add(
                     new Domain.Address
                     {
                         ZipCode = (int)request.ZipCode,
                         Street = request.Street
                     }
                 );
            }

            List<UserUseCase> collection = new List<UserUseCase>();

            for(var i = 0; i < 50; i++) 
            {
                collection.Add(new UserUseCase { UseCaseId = i, UserId = user.Id });
            }

            user.UserUserCases = collection;

            _context.Users.Add(user);

            _context.SaveChanges();
        }
    }
}
