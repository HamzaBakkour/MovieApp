using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class ActorAlreayAssignedException : Exception
{
    public ActorAlreayAssignedException(int actorId) : base($"The actor with id: {actorId} is already asigned to this movie.") { }
}
