using System;
using Akka.Actor;
using MovieStreamingDI.Actors;

namespace MovieStreamingDI
{
    public class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("ActorSystem Created.");

            var playbackActorRef = _movieStreamingActorSystem.ActorOf<PlaybackActor>("Playback");
            Console.ReadLine();
        }
    }
}
