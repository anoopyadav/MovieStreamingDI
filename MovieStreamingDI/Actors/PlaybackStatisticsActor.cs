using System;
using Akka.Actor;
using Akka.Event;

namespace MovieStreamingDI.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger;

        public PlaybackStatisticsActor()
        {
            _logger = Context.GetLogger();
            Context.ActorOf<MoviePlayCounterActor>("MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if (exception is ActorInitializationException)
                {
                    _logger.Error(exception, "PlaybackStatisticsActor stopping child due to ActorInitializationException.");
                    return Directive.Stop;
                }

                _logger.Error("PlaybackStatisticsActor restarting child due to unexpected exception.");
                return Directive.Restart;
            });
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            _logger.Info("PlaybackStatistics PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            _logger.Info("PlaybackStatistics PostStop");
        }

        #endregion
    }
}
