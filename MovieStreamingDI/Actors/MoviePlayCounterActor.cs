using System.Collections.Generic;
using Akka.Actor;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _playCounter;
        public MoviePlayCounterActor()
        {
            _playCounter = new Dictionary<string, int>();

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
        }
    }
}
