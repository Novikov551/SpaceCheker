using SpaceObjectsApi.Models;
using System.Collections.Generic;

namespace SpaceObjectsApi.IntegrationTests
{
    public static class DatabaseInitializer
    {
        public static void InitializeDataBase(SpaceObjectsContext context)
        {
            context.Database.EnsureCreated();
            context.AddRange(AddObjectsToDataBase());
            context.SaveChanges();
        }

        public static void ReInitializeDataBase(SpaceObjectsContext context)
        {
            context.RemoveRange(context.Planets);
            context.RemoveRange(context.Stars);
            context.RemoveRange(context.Asteroids);
            context.RemoveRange(context.BlackHoles);
            InitializeDataBase(context);
        }

        public static List<SpaceObject> AddObjectsToDataBase()
        {
            return new List<SpaceObject>()
            {
                new Star()
                {
                    Name="Sun",
                    DistanceToEarth=12,
                    Description=""
                },
                new Planet()
                {
                    Name="Earth",
                    ExistenceOfLife=true,
                    Description=""
                },
                new Asteroid()
                {
                    Name="Icon-618",
                    Diameter=24,
                    Description=""
                },
                new BlackHole()
                {
                    Name="Earth",
                    Weight=20,
                    Description=""
                }
            };
        }
    }
}