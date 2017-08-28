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
    }
}
