using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Interfaces
{
    public interface IQuery
    {
    }
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<T> : IQueryHandler where T : IQuery
    {
        IList<IResult> Handle(T query);

    }


    public interface IQueryDispatcher
    {
        IList<IResult> Send<T>(T query) where T : IQuery;
    }
}
