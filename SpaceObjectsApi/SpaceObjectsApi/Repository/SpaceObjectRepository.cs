using Microsoft.EntityFrameworkCore;
using SpaceChecker.Models;
using SpaceChecker.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceChecker.Repository
{
    public class SpaceObjectRepository : IRepository
    {
        protected readonly SpaceObjectsContext _db;

        public SpaceObjectRepository(SpaceObjectsContext db)
        {
            _db = db;
        }

        public async Task<IQueryable<T>> GetAsync<T>() where T : SpaceObject
        {
            var asteroidList = await _db.Set<T>().ToListAsync();

            return asteroidList.AsQueryable();
        }

        public async Task<T> GetAsync<T>(int id) where T : SpaceObject
        {
            return await _db.FindAsync<T>(id); ;
        }

        public async virtual Task<int> AddAsync<T>(T customObject) where T : SpaceObject
        {
            await _db.AddAsync(customObject);
            await _db.SaveChangesAsync();

            return customObject.Id;
        }

        public async virtual Task<bool> RemoveAsync<T>(T spaceObject) where T : SpaceObject
        {
            _db.Remove(spaceObject);
            await _db.SaveChangesAsync();

            return true;
        }

        public async virtual Task<bool> UpdateAsync<T>(T spaceObject) where T : SpaceObject
        {
            _db.Update(spaceObject);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}