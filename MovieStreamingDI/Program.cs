using System;
using System.Threading;
using Akka.Actor;
using MovieStreamingDI.Actors;
using MovieStreamingDI.Messages;

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

            Console.WriteLine("Usage: <start/stop>,<UserId>,<MovieTitle>");

            while (true)
            {
                Thread.Sleep(200);
                Console.WriteLine("Enter a command:");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }


                if (input.StartsWith("play"))
                {
                    var userId = int.Parse(input.Split(',')[1]);
                    var movieTitle = input.Split(',')[2];

                    var playMovieMessage = new PlayMovieMessage(userId, movieTitle);
                    var userCoordinator = _movieStreamingActorSystem.ActorSelection
                        ("/user/Playback/UserCoordinator");
                    userCoordinator.Tell(playMovieMessage);
                }
                else if (input.StartsWith("stop"))
                {
                    var userId = int.Parse(input.Split(',')[1]);

                    var stopMovieMessage = new StopMovieMessage(userId);
                    var userCoordinator = _movieStreamingActorSystem.ActorSelection
                        ("/user/Playback/UserCoordinator");
                    userCoordinator.Tell(stopMovieMessage);
                }
                else if (input.StartsWith("exit"))
                {
                    break;
                }
            }
        }
    }
}
