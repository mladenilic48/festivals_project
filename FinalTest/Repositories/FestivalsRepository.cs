using FinalniTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FinalniTest.Models;
using System.Data.Entity.Infrastructure;

namespace FinalniTest.Repositories
{
    public class FestivalsRepository : IDisposable, IFestivalsRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Festival> GetAll()
        {
            return db.Festivali.Include(f => f.Mesto);
        }

        public Festival GetById(int id)
        {
            return db.Festivali.Include(f => f.Mesto).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Festival festival)
        {
            db.Festivali.Add(festival);
            db.SaveChanges();
        }

        public void Delete(Festival festival)
        {
            db.Festivali.Remove(festival);
            db.SaveChanges();
        }

        public void Update(Festival festival)
        {
            db.Entry(festival).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
        }

        public IEnumerable<Festival> PostByPeriod(int start, int kraj)
        {
            return db.Festivali.Include(f => f.Mesto).Where(f => f.GodinaPrvogOdrzavanja > start && f.GodinaPrvogOdrzavanja < kraj).OrderBy(f => f.GodinaPrvogOdrzavanja);
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