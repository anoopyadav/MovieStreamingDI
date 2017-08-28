namespace MovieStreamingDI.Messages
{
    class PlayMovieMessage
    {
        private string MovieTitle { get; }
        private int UserId { get; }
        public PlayMovieMessage(int userId, string movieTitle)
        {
            UserId = userId;
            MovieTitle = movieTitle;
        }
    }
}
