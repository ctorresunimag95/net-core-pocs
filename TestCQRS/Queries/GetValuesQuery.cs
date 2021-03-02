using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TestCQRS.Queries
{
    public class GetValuesQuery
    {
        public class Query : IRequest<string> { }

        public class Handler : IRequestHandler<Query, string>
        {
            public Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult("Hello world");
            }
        }
    }
}
