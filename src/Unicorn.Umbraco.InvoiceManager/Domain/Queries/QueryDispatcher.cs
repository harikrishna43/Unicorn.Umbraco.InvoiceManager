﻿using Unicorn.Umbraco.InvoiceManager.Exceptions;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _service;

        public QueryDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public TResult Send<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _service.GetService(typeof(IQueryHandler<TQuery, TResult>));
            if (handler != null)
                return ((IQueryHandler<TQuery, TResult>)handler).Handle(query);
            else
                throw new NotFoundException($"Query doesn't have any handler {query.GetType().Name}");

        }
    }
}
