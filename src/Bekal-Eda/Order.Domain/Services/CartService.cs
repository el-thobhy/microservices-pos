using AutoMapper;
using Framework.Core.Enums;
using Framework.Core.Event;
using Framework.Core.Event.External;
using Order.Domain.Dtos;
using Order.Domain.Entities;
using Order.Domain.EventEnvelopes;
using Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Services
{
    public interface ICartService
    {
        Task<CartDto> AddCart(CartDto dto);
        Task<IEnumerable<CartDto>> All();
        Task<CartDto> GetCartById(Guid id);
        Task<bool> UpdateCartStatus(CartStatusEnum status, Guid id);
    }
    public class CartService : ICartService
    {

        private ICartRepository _repository;
        private ICartProductRepository _cartProductRepository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public CartService(ICartRepository repository, ICartProductRepository cartProductRepository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
            _cartProductRepository = cartProductRepository;
        }
        public async Task<CartDto> AddCart(CartDto dto)
        {
            try
            {
                if (dto != null)
                {
                    dto.Status = CartStatusEnum.Pending;
                    var dtoToEntity = _mapper.Map<CartEntity>(dto);
                    var entity = await _repository.Add(dtoToEntity);
                    var result = await _repository.SaveChangesAsync();

                    if (result > 0)
                    {
                        var externalEvent = new EventEnvelope<CartCreated>(
                           CartCreated.Created(
                               entity.Id,
                               entity.CustomerId,
                               entity.Status
                           )
                       );
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        return _mapper.Map<CartDto>(entity);
                    }
                        
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await Console.Out.WriteLineAsync(e.InnerException.Message);
                throw;
            }

            return new CartDto();
        }

        public async Task<IEnumerable<CartDto>> All()
        {

            return _mapper.Map<IEnumerable<CartDto>>(await _repository.GetAll());
        }

        public async Task<CartDto> GetCartById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _repository.GetById(id);
                if (result != null)
                {
                    return _mapper.Map<CartDto>(result);
                }
            }
            return null;
        }

        public async Task<bool> UpdateCartStatus(CartStatusEnum status, Guid id)
        {
            var cart = await _repository.GetById(id);
            if (cart != null)
            {
                cart.Status = status;
                var entity = await _repository.Update(cart);
                var result = await _repository.SaveChangesAsync();
                if (result > 0)
                {
                    List<CartProductItem> items = new List<CartProductItem>();
                    if (status == CartStatusEnum.Confirmed)
                    {
                        var cartProducts = await _cartProductRepository.GetByCartId(cart.Id);
                        foreach (var item in cartProducts)
                        {
                            items.Add(new CartProductItem()
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            });
                        }
                    }
                    var externalEvent = new EventEnvelope<CartStatusChanged>(
                             CartStatusChanged.UpdateStatus(
                                 entity.Id,
                                 entity.CustomerId,
                                 entity.Status,
                                 items,
                                 entity.ModifiedDate
                                 )
                             );

                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                    return true;
                }
            }
            return false;
        }
    }
}
