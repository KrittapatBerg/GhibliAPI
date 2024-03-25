using GhibliAPI.Data;
using GhibliAPI.Model;
using GhibliAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GhibliAPI.Service;

public class GhibliService : IGhibliService
{
    private readonly DataContext context;

    public GhibliService(DataContext context)
    {
        this.context = context;
    }

    public async Task<List<Movie>> GetMovies()
    {
        if (context.Movies == null)
        {
            return null; 
        }
        return await context.Movies.ToListAsync();
    }

    public async Task<Movie> GetMovie(Guid id)
    {
        if (context.Movies == null)
        {
            return null;
        }
        return await context.Movies.FindAsync(id);
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        bool exists = context.Movies.Any(m => m.Title == movie.Title);
        if (exists)
        {
            throw new InvalidOperationException("Movie already exists");
        }
        _ = context.Add(movie);
        _ = await context.SaveChangesAsync();
        
        return movie;
    }

    public async Task<Movie> UpdateMovie(Movie updatedMovie)
    {
        bool exists = context.Movies.Any(m => m.Id == updatedMovie.Id); 
        if (!exists)
        {
            throw new InvalidOperationException("Movie does not exist");
        }
        
        _ = context.Update(updatedMovie);
        _ = await context.SaveChangesAsync();
        
        return updatedMovie;
    }

    public async Task<Movie> DeleteMovie(Guid id)
    {
        Movie? movie = await context.Movies.FindAsync(id) ?? throw new InvalidOperationException("Movie does not exist");
        _ = context.Remove(movie);
        _ = await context.SaveChangesAsync();
        
        return movie;
    }

    Task<List<Movie>> IGhibliService.AddMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
