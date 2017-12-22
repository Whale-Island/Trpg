using System;
using System.Collections.Generic;
using System.Linq;
using WhaleIsland.Trpg.GM.Handler;

namespace WhaleIsland.Trpg.GM.Logic
{
    public static class HandleFactory
    {
        public static readonly List<IHandler> handlers = new List<IHandler>();
        public static void Init()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(a => a.GetTypes().Where(t => t.IsAssignableFrom(typeof(IHandler))))
                       .ToArray();

            foreach (Type type in types) {
                

            }

        }


    }
}
