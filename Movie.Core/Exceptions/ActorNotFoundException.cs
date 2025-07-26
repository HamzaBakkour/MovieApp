using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class ActorNotFoundException : NotFoundException
{
    public ActorNotFoundException(int id) : base($"The actor with id: {id} was not found") { }
}
