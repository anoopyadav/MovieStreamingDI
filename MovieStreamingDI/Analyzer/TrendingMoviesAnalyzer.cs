using System.Collections.Generic;
using System.Linq;

namespace MovieStreamingDI.Analyzer
{
    public class TrendingMoviesAnalyzer : ITrendingMoviesAnalyzer
    {
        public string CalculateMostPopularMovie(IEnumerable<string> movieTitles)
        {
            var movieCounts = movieTitles.GroupBy(title => title, (key ,value) => new {MovieTitle = key, playCount = value.Count()});
            return movieCounts.OrderByDescending(x => x.playCount).First().MovieTitle;
        }
    }
}