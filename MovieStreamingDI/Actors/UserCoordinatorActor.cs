using Akka.Actor;
using System.Collections.Generic;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private Dictionary<int, IActorRef> _users; 
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {

            });

            Receive<StopMovieMessage>(message =>
            {

            });
        }

        private void CreateUserIfNotExists(int userId)
        {
            
        }
    }
}
