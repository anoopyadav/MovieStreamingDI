using Akka.Actor;

namespace MovieStreamingDI
{
    public class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            var playbackActorRef = _movieStreamingActorSystem.ActorOf<PlaybackActor>("Playback");
        }
    }
}
