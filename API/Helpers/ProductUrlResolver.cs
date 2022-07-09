using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDtos, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            this._config = config;
        }

        public string Resolve(Product source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                 return _config["ApiUrl"] + source.PictureURL;
            }
            return null;
        }

        public string Resolve(ProductUrlResolver source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}