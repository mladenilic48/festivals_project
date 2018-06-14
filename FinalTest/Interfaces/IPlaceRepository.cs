using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalniTest.Interfaces
{
    public interface IPlaceRepository
    {
        IEnumerable<Place> GetAll();
        Place GetById(int id);
        IEnumerable<Place> GetByPostCode(int kod);
    }
}