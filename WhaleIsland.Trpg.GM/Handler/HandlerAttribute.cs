using System;

namespace WhaleIsland.Trpg.GM.Handler
{
    public class HandlerAttribute : Attribute
    {
        public string Key { get; set; }
        public HandlerAttribute(string key)
        {
            Key = key;
        }
    }
}
