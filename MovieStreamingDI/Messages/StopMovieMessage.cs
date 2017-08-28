namespace MovieStreamingDI.Messages
{
    public class StopMovieMessage
    {
        private int UserId { get; }
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }
    }
}
