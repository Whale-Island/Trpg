using System.Collections.Generic;

namespace WhaleIsland.Trpg.GM.Handler
{
    public abstract class IHandler
    {
        public string SendId { get; set; }
        public string Name { get; set; }
        public abstract string Handle(List<string> cmd);
    }
}
