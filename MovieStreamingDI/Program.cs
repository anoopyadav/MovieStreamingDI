using System;
using System.Threading;
using Akka.Actor;
using Akka.DI.Unity;
using Microsoft.Practices.Unity;
using MovieStreamingDI.Actors;
using MovieStreamingDI.Analyzer;
using MovieStreamingDI.Messages;
using NLog;

namespace MovieStreamingDI
{
    public class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<ITrendingMoviesAnalyzer, TrendingMoviesAnalyzer>();
            unityContainer.RegisterType<TrendingMoviesActor>();
            var resolver = new UnityDependencyResolver(unityContainer, _movieStreamingActorSystem);

            var playbackActorRef = _movieStreamingActorSystem.ActorOf<PlaybackActor>("Playback");

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
