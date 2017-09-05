using System;
using Akka.Actor;
using Akka.Event;

namespace MovieStreamingDI.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger;
        public PlaybackActor()
        {
            _logger = Context.GetLogger();
            Context.ActorOf<UserCoordinatorActor>("UserCoordinator");
            Context.ActorOf<PlaybackStatisticsActor>("PlaybackStatistics");
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            _logger.Info("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            _logger.Info("PlaybackActor PostStop");
        }

        #endregion
    }
}
