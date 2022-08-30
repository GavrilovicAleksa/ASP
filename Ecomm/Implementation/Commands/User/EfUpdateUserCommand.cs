using Application.Commands.User;
using Application.DataTransfer.User;
using Application.Exceptions;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.User
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    { 
        private readonly Context _context;

        private readonly UserEntityValidator _validator;
      
        public EfUpdateUserCommand(Context context, UserEntityValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Update a user";

        public void Execute(UserDto request)
        {
            var user = _context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.User));
            }

            if (request.UserName != null)
            {
                user.FirstName = request.FirstName;
            }
            if (request.LastName != null)
            {
                user.LastName = request.LastName;
            }
            if (request.UserName != null)
            {
                user.UserName = request.UserName;
            }
            if (request.Password != null)
            {
                user.Password = request.Password;
            }
            if (request.Email != null)
            {
                user.Email = request.Email;
            } 
            if (request.IsAdmin == true)
            {
                user.IsAdmin = true;
                for (var i = 100;i < 150;i++)
                {
                    _context.UserUseCases.Add(new UserUseCase { UseCaseId = i, UserId = user.Id});
                }
            }

            user.UpdatedAt = new DateTime();

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
