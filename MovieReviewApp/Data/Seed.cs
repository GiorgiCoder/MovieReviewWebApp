//using MovieReviewApp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace MovieReviewApp.Data
//{
//    public static class Seed
//    {
//        public static void SeedData(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

//                context.Database.EnsureCreated();

//                if (!context.Countries.Any())
//                {
//                    context.Countries.AddRange(
//                        new Country { Name = "USA" },
//                        new Country { Name = "United Kingdom" },
//                        new Country { Name = "France" },
//                        new Country { Name = "Ireland" }
//                    );
//                    context.SaveChanges();
//                }

//                if (!context.Genres.Any())
//                {
//                    context.Genres.AddRange(
//                        new Genre { Name = "Action" },
//                        new Genre { Name = "Drama" },
//                        new Genre { Name = "Comedy" }
//                    );
//                    context.SaveChanges();
//                }

//                if (!context.Actors.Any())
//                {
//                    context.Actors.AddRange(
//                        new Actor
//                        {
//                            Name = "Robert Downey Jr.",
//                            Description = "American actor known for his roles in Iron Man and Sherlock Holmes.",
//                            DoB = new DateTime(1965, 4, 4),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "USA")!.Id
//                        },
//                        new Actor
//                        {
//                            Name = "Scarlett Johansson",
//                            Description = "American actress known for her roles in The Avengers and Lost in Translation.",
//                            DoB = new DateTime(1984, 11, 22),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "USA")!.Id
//                        },
//                        new Actor
//                        {
//                            Name = "Cillian Murphy",
//                            Description = "Irish actor known for his roles in many famous movies and series like Interstellar, Peaky Blinders, Dunkirk, Oppenheimer...",
//                            DoB = new DateTime(1976, 5, 25),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "Ireland")!.Id
//                        }
//                    );
//                    context.SaveChanges();
//                }

//                if (!context.Directors.Any())
//                {
//                    context.Directors.AddRange(
//                        new Director
//                        {
//                            Name = "Christopher Nolan",
//                            Description = "British-American filmmaker known for his work on The Dark Knight trilogy, Interstellar, Inception...",
//                            DoB = new DateTime(1970, 7, 30),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "United Kingdom")!.Id
//                        },
//                        new Director
//                        {
//                            Name = "Quentin Tarantino",
//                            Description = "American filmmaker known for his unique style and films such as Pulp Fiction, Inglourious Basterds, Kill Bill and many others.",
//                            DoB = new DateTime(1963, 3, 27),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "USA")!.Id
//                        },
//                        new Director
//                        {
//                            Name = "Steven Spielberg",
//                            Description = "American film director, producer and screenwriter. He is the most commercially successful director in history.",
//                            DoB = new DateTime(1946, 11, 18),
//                            CountryId = context.Countries.FirstOrDefault(c => c.Name == "USA")!.Id
//                        }
//                    );
//                    context.SaveChanges();
//                }

//                if (!context.Movies.Any())
//                {
//                    context.Movies.AddRange(
//                        new Movie
//                        {
//                            Title = "Inception",
//                            Description = "A thief who enters the dreams of others to steal their secrets from their subconscious.",
//                            Length = 148,
//                            ReleaseDate = new DateTime(2010, 7, 16),
//                            Budget = 160000000,
//                            BoxOffice = 829900000,
//                            DirectorId = context.Directors.FirstOrDefault(d => d.Name == "Christopher Nolan")!.Id
//                        },
//                        new Movie
//                        {
//                            Title = "The Dark Knight",
//                            Description = "Batman faces off against the Joker, a criminal mastermind.",
//                            Length = 152,
//                            ReleaseDate = new DateTime(2008, 7, 18),
//                            Budget = 185000000,
//                            BoxOffice = 1005000000,
//                            DirectorId = context.Directors.FirstOrDefault(d => d.Name == "Christopher Nolan")!.Id
//                        }
//                    );
//                    context.SaveChanges();
//                }

//                if (!context.ActorMovies.Any())
//                {
//                    context.ActorMovies.AddRange(
//                        new ActorMovie { ActorId = context.Actors.FirstOrDefault(a => a.Name == "Leonardo DiCaprio")!.Id, MovieId = context.Movies.FirstOrDefault(m => m.Title == "Inception")!.Id, ScreenTime = 120, Salary = 20000000 },
//                        new ActorMovie { ActorId = context.Actors.FirstOrDefault(a => a.Name == "Joseph Gordon-Levitt")!.Id, MovieId = context.Movies.FirstOrDefault(m => m.Title == "Inception")!.Id, ScreenTime = 100, Salary = 10000000 },
//                        new ActorMovie { ActorId = context.Actors.FirstOrDefault(a => a.Name == "Heath Ledger")!.Id, MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Dark Knight")!.Id, ScreenTime = 110, Salary = 15000000 },
//                        new ActorMovie { ActorId = context.Actors.FirstOrDefault(a => a.Name == "Christian Bale")!.Id, MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Dark Knight")!.Id, ScreenTime = 130, Salary = 18000000 }
//                    );
//                    context.SaveChanges();
//                }
//            }
//        }
//    }
//}
