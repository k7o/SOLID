using Crosscutting.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Loggers.Decorators
{
    public class IAmTraceableRequestHandlerDecorator<TQuery, TResult> : MediatR.IRequestHandler<TQuery, TResult> where TQuery : IAmTraceable, IRequest<TResult>
    {
        readonly MediatR.IRequestHandler<TQuery, TResult> _decoratee;
        readonly ITrace _queryTracer;

        public IAmTraceableRequestHandlerDecorator(ITrace queryTracer, MediatR.IRequestHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(queryTracer, nameof(queryTracer));

            _decoratee = decoratee;
            _queryTracer = queryTracer;
        }

        public Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            if (Equals(request, default(TQuery)))
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Start(_queryTracer);

            Task<TResult> result;
            try
            {
                request.Excute(_queryTracer);

                result = _decoratee.Handle(request, cancellationToken);

                request.Excuted(_queryTracer);
            }
            catch (Exception)
            {
                request.Exception(_queryTracer);
                throw;
            }
            finally
            {
                request.Stop(_queryTracer);
            }

            return result;
        }
    }
}
