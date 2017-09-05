namespace MovieStreamingDI.Messages
{
    class PlayMovieMessage
    {
        public string MovieTitle { get; }
        public int UserId { get; }
        public PlayMovieMessage(int userId, string movieTitle)
        {
            UserId = userId;
            MovieTitle = movieTitle;
        }
    }
}
