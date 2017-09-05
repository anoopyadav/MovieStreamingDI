using System;
using Akka.Actor;
using System.Collections.Generic;
using Akka.Event;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        private readonly ILoggingAdapter _logger; 
        public UserCoordinatorActor()
        {
            _logger = Context.GetLogger();
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateUserIfNotExists(message.UserId);

                _users[message.UserId].Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateUserIfNotExists(message.UserId);

                _users[message.UserId].Tell(message);
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
            _logger.Info("UserCoordinator PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            _logger.Info("UserCoordinator PostStop");
        }

        #endregion
    }
}
