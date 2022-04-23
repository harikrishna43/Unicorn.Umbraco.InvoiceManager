using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Persistence.Querying;

namespace Invoice_Manager.Interfaces
{
    public interface IQuery<TResult>
    {
    }
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult> 
    {
        TResult Handle(TQuery query);

    }

    public interface IQueryDispatcher
    {
        TResult Send<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
