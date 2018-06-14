using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalniTest.Interfaces
{
    public interface IFestivalsRepository
    {
        IEnumerable<Festival> GetAll();
        Festival GetById(int id);
        void Add(Festival festival);
        void Delete(Festival festival);
        void Update(Festival festival);
        IEnumerable<Festival> PostByPeriod(int start, int kraj);
    }
}