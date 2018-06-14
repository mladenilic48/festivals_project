namespace FinalniTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinalniTest.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalniTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalniTest.Models.ApplicationDbContext context)
        {
            context.Mesta.AddOrUpdate(x => x.Id,
                new Place() { Id = 1, Naziv = "Budapest", PostanskiBroj = 1205 },
                new Place() { Id = 2, Naziv = "Novi Sad", PostanskiBroj = 21000 },
                new Place() { Id = 3, Naziv = "Budva", PostanskiBroj = 85310 }

                );

            context.Festivali.AddOrUpdate(x => x.Id,
                new Festival()
                {
                    Id = 1,
                    Naziv = "Sziged",
                    GodinaPrvogOdrzavanja = 1990,
                    MestoId = 1,
                    CenaKarte = 150M
                },
                new Festival()
                {
                    Id = 2,
                    Naziv = "Exit",
                    GodinaPrvogOdrzavanja = 2000,
                    MestoId = 2,
                    CenaKarte = 60M
                },
                new Festival()
                {
                    Id = 3,
                    Naziv = "Sea Dance",
                    GodinaPrvogOdrzavanja = 2014,
                    MestoId = 3,
                    CenaKarte = 30.50M
                }

                );
        }
    }
}
