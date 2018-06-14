using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FinalniTest.Interfaces;
using FinalniTest.Models;

namespace FinalniTest.Repositories
{
    public class PlacesRepository : IDisposable, IPlaceRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Place> GetAll()
        {
            return db.Mesta;
        }

        public Place GetById(int id)
        {
            return db.Mesta.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Place> GetByPostCode(int kod)
        {
            return db.Mesta.Where(m => m.PostanskiBroj < kod).OrderBy(m => m.PostanskiBroj);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}