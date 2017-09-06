using System.Collections.Generic;

namespace MovieStreamingDI.Analyzer
{
    public interface ITrendingMoviesAnalyzer
    {
        string CalculateMostPopularMovie(IEnumerable<string> movieTitles);
    }
}
