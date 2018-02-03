using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Infrastructure
{
    class DecoratorChainBuilder<TResult>
    {
        Tuple<Type, List<object>> _decorated;
        List<Tuple<Type, List<object>>> _decorators;

        private DecoratorChainBuilder(Type decorated, params object[] parameters)
        {
            _decorated = new Tuple<Type, List<object>>(decorated, new List<object>(parameters));
            _decorators = new List<Tuple<Type, List<object>>>();
        }

        public static DecoratorChainBuilder<TResult> Construct(Type decorated, params object[] parameters)
        {
            return new DecoratorChainBuilder<TResult>(decorated, parameters);
        }

        public DecoratorChainBuilder<TResult> Add(Type decorator, params object[] parameters)
        {
            return Add(() => true, decorator, parameters);
        }

        public DecoratorChainBuilder<TResult> Add(Func<bool> onCondition, Type decorator, params object[] parameters)
        {
            if (onCondition.Invoke())
            {
                _decorators.Add(new Tuple<Type, List<object>>(decorator, new List<object>(parameters)));
            }

            return this;
        }

        public TResult Build()
        {
            var decorated = Activator.CreateInstance(_decorated.Item1, _decorated.Item2.ToArray());
            
            foreach (var decorator in _decorators)
            {
                decorator.Item2.Add(decorated);
                decorated = Activator.CreateInstance(decorator.Item1, decorator.Item2.ToArray());
            }

            return (TResult)decorated;
        }
    }
}
