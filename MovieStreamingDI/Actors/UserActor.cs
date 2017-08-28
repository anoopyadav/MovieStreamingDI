using Akka.Actor;
using MovieStreamingDI.Messages;

namespace MovieStreamingDI.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyPlaying;
        public UserActor()
        {
            Receive<PlayMovieMessage>(message =>
            {
                if (string.IsNullOrEmpty(_currentlyPlaying))
                {
                    _currentlyPlaying = message.MovieTitle;
                }
                else
                {
                    // Print Error
                }
            });

            Receive<StopMovieMessage>(message =>
            {
                if (!string.IsNullOrEmpty(_currentlyPlaying))
                {
                    _currentlyPlaying = null;
                }
                else
                {
                    // Print Error
                }
            });
        }
    }
}
