using System.Collections;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using MovieStreamingDI.Analyzer;
using MovieStreamingDI.Messages;
using NLog;

namespace MovieStreamingDI.Actors
{
    public class TrendingMoviesActor : ReceiveActor
    {
        private readonly Queue<string> _trendingMovies;
        private const int NumberOfMoviesToAnalyse = 5;
        private readonly ITrendingMoviesAnalyzer _trendingMoviesAnalyzer;
        private readonly ILoggingAdapter _logger;

        public TrendingMoviesActor(ITrendingMoviesAnalyzer trendAnalyzer)
        {
            _logger = Context.GetLogger();
            _trendingMoviesAnalyzer = trendAnalyzer;
            _trendingMovies = new Queue<string>(NumberOfMoviesToAnalyse);
            Receive<IncrementPlayCountMessage>(message => HandleIncrementPlayCountMessage(message));
        }

        private void HandleIncrementPlayCountMessage(IncrementPlayCountMessage message)
        {
            var isBufferFull = NumberOfMoviesToAnalyse == _trendingMovies.Count;
            if (isBufferFull)
            {
                _trendingMovies.Dequeue();
            }

            _trendingMovies.Enqueue(message.MovieTitle);

            var trendingMovie = _trendingMoviesAnalyzer.CalculateMostPopularMovie(_trendingMovies);

            _logger.Info($"{trendingMovie} is trending.");
        }
    }
}
