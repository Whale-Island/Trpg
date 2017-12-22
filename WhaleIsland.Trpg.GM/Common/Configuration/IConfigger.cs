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
    public interface IConfigger : IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        void Install();

        /// <summary>
        ///
        /// </summary>
        void Reload();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetFirstConfig<T>() where T : ConfigSection;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetFirstOrAddConfig<T>() where T : ConfigSection, new();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createFactory"></param>
        /// <returns></returns>
        T GetFirstOrAddConfig<T>(Lazy<T> createFactory) where T : ConfigSection, new();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetConfig<T>() where T : ConfigSection;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IList<ConfigSection> GetAllConfig();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetConnetion<T>(string name) where T : ConnectionSection;

    }
}
