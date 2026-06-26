using Domain.ProductAggregate.Services;
using Domain.SellerAggregate;
using Domain.SellerAggregate.Repository;
using Domain.SellerAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sellers
{
    public class SellerDomainService : ISellerDomainService
    {
        private readonly ISellerRepository _repository;

        public SellerDomainService(ISellerRepository repository)
        {
            _repository = repository;
        }

        public bool IsValidSellerInformation(Seller seller)
        {
            return !_repository.Exists(
                x => x.UserId == seller.UserId
               || x.NationalCode == seller.NationalCode);
        }

        public bool NationalCodeExistInDataBase(string nationalCode)
        {
            return _repository.Exists(r=>r.NationalCode == nationalCode);

        }
    }
}
