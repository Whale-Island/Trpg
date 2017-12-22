using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class DefaultDataConfigger : DataConfigger
    {
        /// <summary>
        ///
        /// </summary>
        protected override void LoadConfigData()
        {
        }

        internal void Add(ConfigSection nodeData)
        {
            AddNodeData(nodeData);
        }
    }
}
