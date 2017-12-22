﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleIsland.Trpg.GM.Common.RPC.IO
{
    /// <summary>
    ///
    /// </summary>
    public enum MessageError
    {
        /// <summary>
        ///
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 系统错误
        /// </summary>
        SystemError = 10000
    }

    /// <summary>
    /// 消息头
    /// </summary>
    public class MessageHead
    {
        /// <summary>
        /// default st
        /// </summary>
        public const string DefaultSt = "st";

        /// <summary>
        ///
        /// </summary>
        public MessageHead()
        {
            ErrorInfo = string.Empty;
            St = DefaultSt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorInfo"></param>
        public MessageHead(int action, int errorCode = 0, string errorInfo = "")
            : this(0, action, DefaultSt, errorCode, errorInfo)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="action"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorInfo"></param>
        public MessageHead(int msgId, int action, int errorCode = 0, string errorInfo = "")
            : this(msgId, action, DefaultSt, errorCode, errorInfo)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="action"></param>
        /// <param name="st"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorInfo"></param>
        public MessageHead(int msgId, int action, string st = DefaultSt, int errorCode = 0, string errorInfo = "")
        {
            MsgId = msgId;
            Action = action;
            St = st;
            ErrorCode = errorCode;
            ErrorInfo = errorInfo;
        }

        /// <summary>
        ///
        /// </summary>
        public void SetSystemError()
        {
            ErrorCode = (int)MessageError.SystemError;
        }

        /// <summary>
        ///
        /// </summary>
        public bool Success
        {
            get { return ErrorCode == 0; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Faild
        {
            get { return ErrorCode == (int)MessageError.SystemError; }
        }

        /// <summary>
        /// 消息包总字节
        /// </summary>
        public long PacketLength
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gzip压缩包的长度
        /// </summary>
        public long GzipLength { get; internal set; }

        /// <summary>
        /// 消息体总字节
        /// </summary>
        public long TotalLength
        {
            get;
            internal set;
        }

        /// <summary>
        /// Push:固定下发0,R-R:下发请求的MsgId
        /// </summary>
        public int MsgId
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public int Action
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public int ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string ErrorInfo
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string St
        {
            get;
            set;
        }

        /// <summary>
        /// 是否包括Gzip压缩头部长度信息
        /// </summary>
        public bool HasGzip { get; set; }

        /// <summary>
        /// 客户端版本，0：旧版本
        /// </summary>
        public int ClientVersion { get; set; }
    }

}
