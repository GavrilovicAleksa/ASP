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

namespace Implementation.Commands.Photo
{
    public class EfUpdatePhotoCommand : IUpdatePhotoCommand
    {
        public int Id => 9;

        public string Name => "Update a photo";

        private readonly Context _context;
        private readonly PhotoEntityValidator _validator;

        public EfUpdatePhotoCommand(Context context, PhotoEntityValidator validator)
        {
            this._context = context;
            this._validator = validator; 

        }

        public void Execute(PhotoDto request)
        { 
            this._validator.ValidateAndThrow(request);
             
            var photo = _context.Photos.Find(request.Id);

            if (photo == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Brand));
            }    

            if (request.UserId != null) {         
                var user = _context.Users.Find(request.UserId);
                         
                if (user != null) 
                {
                    photo.UserId = user.Id;
                }
            }

            if (request.ProductId != null)
            {
                var product = _context.Products.Find(request.ProductId);

                if (product != null)
                {
                    photo.ProductId = product.Id;
                }
            }

            if (request.Url != null)
            {
                photo.Url = request.Url;
            }
   
         
            photo.UpdatedAt = new DateTime();

            _context.SaveChanges();
        }
    }
}
