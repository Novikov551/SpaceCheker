using SpaceChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceChecker.Repository.Interface
{
    public interface IRepository
    {
        public Task<IQueryable<T>> GetAsync<T>() where T : SpaceObject;

        public Task<T> GetAsync<T>(int id) where T : SpaceObject;

        public Task<int> AddAsync<T>(T customObject) where T : SpaceObject;

        public Task<bool> RemoveAsync<T>(T spaceObject) where T : SpaceObject;

        public Task<bool> UpdateAsync<T>(T spaceObject) where T : SpaceObject;
    }
}
