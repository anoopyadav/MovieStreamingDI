using System;
using Akka.Actor;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyPlaying;
        public UserActor()
        {
            Become(Stopped);
        }

        public void Playing()
        {
            Receive<PlayMovieMessage>(message =>
            {
                Console.WriteLine("Error: Can't play a movie while another is playing.");
            });

            Receive<StopMovieMessage>(message =>
            {
                _currentlyPlaying = null;
                Become(Stopped);
                Console.WriteLine("The UserActor has now become Stopped.");
            });
        }

        public void Stopped()
        {
            Receive<PlayMovieMessage>(message =>
            {
                _currentlyPlaying = message.MovieTitle;
                Become(Playing);
                Console.WriteLine("The UserActor has now become Playing.");
                Console.WriteLine($"Now playing {message.MovieTitle}");

                var playCounterActor = Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter");
                playCounterActor.Tell(new IncrementPlayCountMessage(message.MovieTitle));
            });

            Receive<StopMovieMessage>(message =>
            {
                Console.WriteLine("Error: Can't stop while nothing is playing.");
            });
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            Console.WriteLine("UserActor Prestart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("UserActor PostStop");
        }

        #endregion
    }
}
