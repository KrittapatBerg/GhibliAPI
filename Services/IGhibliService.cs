using GhibliAPI.Model;

namespace GhibliAPI.Services;

public interface IGhibliService
{
    Task<List<Movie>> GetMovies();
    Task<Movie> GetMovie(Guid id);
    Task<List<Movie>> AddMovie(Movie movie);
    Task<Movie> UpdateMovie(Movie updatedMovie);
}
