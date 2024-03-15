using AutoMapper;
using Framework.Core.Enums;
using Framework.Core.Event;
using Framework.Core.Event.External;
using Newtonsoft.Json;
using Payment.Domain.Dtos;
using Payment.Domain.Entity;
using Payment.Domain.EventEnvelopes;
using Payment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> All();
        Task<PaymentDto> Paying(Guid CartId, decimal amount);
    }

    public class PaymentServices : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly ICartProductRepository _cartProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;

        public PaymentServices(
            IPaymentRepository repository,
            ICartProductRepository cartProductRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _cartProductRepository = cartProductRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<IEnumerable<PaymentDto>> All()
        {
            return _mapper.Map<IEnumerable<PaymentDto>>(await _repository.All());
        }

        public async Task<PaymentDto> Paying(Guid cartId, decimal amount)
        {
            PaymentDto dto = new PaymentDto();
            PaymentEntity entity = await _repository.GetById(cartId);

            if (entity != null)
            {
                if (entity.Total <= amount)
                {
                    entity.Pay = amount;
                    entity.Status = CartStatusEnum.Paid;

                    await _repository.Update(entity);
                    var list = await _cartProductRepository.GetByCartId(cartId);

                    //Checking stock
                    foreach (var item in list)
                    {
                        var product = await _productRepository.GetById(item.ProductId);
                        if (product != null)
                        {
                            if (product.Stock < item.Quantity)
                                return null;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    List<CartProductItem> paymentProducts = _mapper.Map<IEnumerable<CartProductItem>>(list).ToList();

                    //Update stock
                    foreach (var item in list)
                    {
                        var product = await _productRepository.GetById(item.ProductId);
                        if (product != null)
                        {
                            product.Stock = product.Stock - item.Quantity;

                            var selected = paymentProducts.Where(o => o.ProductId == item.ProductId).FirstOrDefault();

                            if (selected != null)
                                selected.Stock = product.Stock;

                            await _productRepository.Update(product);
                        }
                        else
                        {
                            return null;
                        }
                    }

                    //var res = await _productRepository.SaveChangesAsync();
                    var result = await _repository.SaveChangesAsync();

                    if (result > 0)
                    {
                        var externalEvent = new EventEnvelope<PaymentCreated>(
                            PaymentCreated.Create(
                                entity.Id,
                                paymentProducts,
                                entity.Status
                            )
                        );

                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        var payingEntity = _mapper.Map<PaymentDto>(entity);
                        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(payingEntity));
                        return payingEntity;
                    }
                }
                return null;
            }
            return null;
        }
    }
}
