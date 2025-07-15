using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DomainContracts;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }

    Task CompleteAsync();
}
