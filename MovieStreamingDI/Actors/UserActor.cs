using System;
using Akka.Actor;
using MovieStreamingDI.Messages;
using Akka.Event;

namespace MovieStreamingDI.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyPlaying;
        private ILoggingAdapter _logger;
        public UserActor()
        {
            _logger = Context.GetLogger();
            Become(Stopped);
        }

        public void Playing()
        {
            Receive<PlayMovieMessage>(message =>
            {
                _logger.Warning("Can't play a movie while another is playing.");
            });

            Receive<StopMovieMessage>(message =>
            {
                _currentlyPlaying = null;
                Become(Stopped);
                _logger.Info("The UserActor has now become Stopped.");
            });
        }

        public void Stopped()
        {
            Receive<PlayMovieMessage>(message =>
            {
                _currentlyPlaying = message.MovieTitle;
                Become(Playing);
                _logger.Info("The UserActor has now become Playing.");
                _logger.Info($"Now playing {message.MovieTitle}");

                var playCounterActor = Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter");
                playCounterActor.Tell(new IncrementPlayCountMessage(message.MovieTitle));
                var trendingMoviesActor =
                    Context.ActorSelection("/user/Playback/PlaybackStatistics/TrendingMovies");
                trendingMoviesActor.Tell(new IncrementPlayCountMessage(message.MovieTitle));
            });

            Receive<StopMovieMessage>(message =>
            {
                _logger.Warning("Can't stop while nothing is playing.");
            });
        }

        #region Lifecycle Hooks

        protected override void PreStart()
        {
            base.PreStart();
            _logger.Info("UserActor Prestart");
        }

        protected override void PostStop()
        {
            base.PostStop();
            _logger.Info("UserActor PostStop");
        }

        #endregion
    }
}
