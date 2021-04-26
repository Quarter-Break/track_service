using LocatieService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Models;

namespace TrackService.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(TrackContext context) : base(context) { }

        public async Task<Track> GetByIdAsync(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
