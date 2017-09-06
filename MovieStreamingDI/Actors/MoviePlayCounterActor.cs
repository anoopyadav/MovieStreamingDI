using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _playCounter;
        private readonly ILoggingAdapter _logger;
        public MoviePlayCounterActor()
        {
            _playCounter = new Dictionary<string, int>();
            _logger = Context.GetLogger();

            Receive<IncrementPlayCountMessage>(message =>
            {
                HandleIncrementPlayCountMessage(message.MovieTitle);
            });
        }

        private void HandleIncrementPlayCountMessage(string movieTitle)
        {
            if (_playCounter.ContainsKey(movieTitle))
            {
                _playCounter[movieTitle]++;
            }
            else
            {
                _playCounter.Add(movieTitle, 1);
            }

            _logger.Info($"{movieTitle} has been played {_playCounter[movieTitle]} times.");
        }
    }
}
