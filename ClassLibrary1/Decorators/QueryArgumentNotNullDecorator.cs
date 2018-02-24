﻿using System;
using Infrastructure;

namespace ClassLibrary1.Decorators
{
    public class QueryArgumentNotNullDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryArgumentNotNullDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public TResult Handle(TQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            return _decorated.Handle(query);
        }
    }
}
