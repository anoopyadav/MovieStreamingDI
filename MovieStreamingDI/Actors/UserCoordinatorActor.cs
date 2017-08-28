using System;
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
                CreateUserIfNotExists(message.UserId);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateUserIfNotExists(message.UserId);
            });
        }

        private void CreateUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                _users[userId] = Context.ActorOf<UserActor>($@"User{userId}");
            }
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            Console.WriteLine("UserCoordinator PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("UserCoordinator PostStop");
        }

        #endregion
    }
}
