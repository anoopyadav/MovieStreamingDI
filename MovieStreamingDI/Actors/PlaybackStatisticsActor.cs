using System;
using Akka.Actor;

namespace MovieStreamingDI.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf<MoviePlayCounterActor>("MoviePlayCounter");
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            Console.WriteLine("PlaybackStatistics PreStart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("PlaybackStatistics PostStop");
        }

        #endregion
    }
}
