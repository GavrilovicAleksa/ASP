using Application.Commands.Brand;
using Application.Commands.Photo;
using Application.DataTransfer.BrandDataTransfer;
using Application.DataTransfer.PhotoDataTransfer;
using Application.Exceptions;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Brand
{
    public class EfInsertPhotoCommand : ICreatePhotoCommand
    {
        public int Id => 8;

        public string Name => "Create a Photo";

        private readonly Context _context;

        private readonly PhotoEntityValidator _validator;

        public EfInsertPhotoCommand(Context context, PhotoEntityValidator validator)
        {
            this._validator = validator;
            this._context = context;
        }

        public void Execute(PhotoDto request)
        {
            //this._validator.ValidateAndThrow(request);

            var user = _context.Users.Find(request.UserId);

            var product = _context.Products.Find(request.ProductId);

            if(user == null && product == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Photo));
            }

            var photo = new Domain.Photo
            {
                Url = request.Url,
                CreatedAt = DateTime.Now
            }; 

            if (user != null)
            {
                photo.UserId = user.Id;
            } else if (product != null)
            {
                photo.ProductId = product.Id;
            }

            this._context.Photos.Add(photo);

            this._context.SaveChanges();
        }
    }
}
