using System;

namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// Supported sql entity object.
    /// </summary>
    public interface ISqlEntity
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetKeyCode();

        /// <summary>
        /// Message queue group id.
        /// </summary>
        /// <returns></returns>
        long GetMessageQueueId();

        /// <summary>
        ///
        /// </summary>
        string PersonalId { get; }

        /// <summary>
        ///
        /// </summary>
        bool IsDelete { get; }

        /// <summary>
        /// reset data
        /// </summary>
        void ResetState();

        /// <summary>
        /// reset data and trigger unchanged event.
        /// </summary>
        void Reset();

        /// <summary>
        /// Get table name of log
        /// </summary>
        DateTime GetCreateTime();

        /// <summary>
        ///
        /// </summary>
        DateTime TempTimeModify { get; set; }
    }
}
