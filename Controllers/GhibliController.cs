using GhibliAPI.Data;
using GhibliAPI.Model;
using GhibliAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GhibliAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GhibliController : ControllerBase
{
    private readonly DataContext context;
    //private readonly IGhibliService service;

    public GhibliController(DataContext context)   //IGhibliService service
    {
        this.context = context;
        //this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAllMovies()
    {
        var movies = await context.Movies.ToListAsync();  
            
        //inject service 
        //var movies = await _movieService.GetMovies();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(Guid id)
    {

        var film = await context.Movies.FindAsync(id); 
        if(film == null)
        {
            return BadRequest("Film not found"); 
        }
        //var movies = await _movieService.GetMovies();

        return Ok(film);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> AddMovie(Movie movie)
    {
        
        context.Movies.Add(movie);
        await context.SaveChangesAsync(); 

        return Ok(await context.Movies.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<Movie>> UpdateMovie(Movie updatedMovie)
    {

        var updateMovie = await context.Movies.FindAsync(updatedMovie.Id);
        if (updateMovie == null)
        {
            return BadRequest("Movie not found");
        }
        updateMovie.Title = updatedMovie.Title;
        updateMovie.Description = updatedMovie.Description;
        updateMovie.Director = updatedMovie.Director;
        updateMovie.ReleaseYear = updatedMovie.ReleaseYear;
        updateMovie.Award = updatedMovie.Award;

        await context.SaveChangesAsync();

        return Ok(await context.Movies.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Movie>>> DeleteMovie(Guid id)
    {
        var movie = await context.Movies.FindAsync(id);
        if (movie == null)
        {
            return BadRequest("Movie not found");
        }
        context.Movies.Remove(movie);
        await context.SaveChangesAsync();

        return Ok(await context.Movies.ToListAsync());
    }   

    //this is a sample data 
    //var movies = new List<Movie>
    //{
    //    new Movie
    //    {
    //        Id = Guid.NewGuid(),
    //        Title = "My Neighbor Totoro",
    //        Description = "Two sisters move to the country with their father in order to be closer to their hospitalized mother, and discover the surrounding trees are inhabited by Totoros, magical spirits of the forest.",
    //        Director = "Hayao Miyazaki",
    //        ReleaseYear = 1988,
    //        Award = "Blue Ribbon Award for Best Film"
    //    },
    //    new Movie
    //    {
    //        Id = Guid.NewGuid(),
    //        Title = "Spirited Away",
    //        Description = "During her family's move to the suburbs, a sullen girl get to learn her life lesson from a spirited world.",
    //        Director = "Hayao Miyazaki",
    //        ReleaseYear = 2001,
    //        Award = "Academy Award for Best Animated Feature"
    //    }
    //};
    //return Ok(movies);   
}
