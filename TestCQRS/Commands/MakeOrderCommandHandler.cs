using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestCQRS.Models;
using TestCQRS.Models.ResponseModels;

namespace TestCQRS.Commands
{
    public class MakeOrderCommandHandler : IRequestHandler<MakeOrderRequestModel, OrderResponseModel>
    {
        public Task<OrderResponseModel> Handle(MakeOrderRequestModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new OrderResponseModel { OrderName = request.OrderName, Quantity = request.Quantity });
        }
    }
}
