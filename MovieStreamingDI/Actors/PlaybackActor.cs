using System;
using Akka.Actor;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf<UserCoordinatorActor>("UserCoordinator");
            Context.ActorOf<PlaybackStatisticsActor>("PlaybackStatistics");

            Receive<PlayMovieMessage>(message =>
            {
               
            });

            Receive<StopMovieMessage>(message =>
            {

            });
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            Console.WriteLine("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("PlaybackActor PostStop");
        }

        #endregion
    }
}
