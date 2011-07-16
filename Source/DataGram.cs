/*=============================================================================
*
*	(C) Copyright 2011, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
*
*   http://www.TheCodeKing.co.uk
*  
*	All rights reserved.
*	The code and information is provided "as-is" without waranty of any kind,
*	either expressed or implied.
*
*=============================================================================
*/
using System;

namespace TheCodeKing.Net.Messaging
{
    /// <summary>
    ///   The data class that is passed between AppDomain boundaries. This is
    ///   sent as a delimited string containing the channel and message.
    /// </summary>
    public class DataGram
    {
        #region Constants and Fields

        /// <summary>
        ///   Stores the channel name associated with this message.
        /// </summary>
        private readonly string channel;

        /// <summary>
        ///   Stores the string message.
        /// </summary>
        private readonly string message;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor which creates the data gram from a message and channel name.
        /// </summary>
        /// <param name = "channel">The channel through which the message will be sent.</param>
        /// <param name = "message">The string message to send.</param>
        public DataGram(string channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        internal DataGram()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets the channel name.
        /// </summary>
        public string Channel
        {
            get { return channel; }
        }

        /// <summary>
        ///   Gets the message.
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        ///   Indicates whether the DataGram contains valid data.
        /// </summary>
        internal bool IsValid
        {
            get { return !string.IsNullOrEmpty(Channel) && !string.IsNullOrEmpty(Message); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the instance to the string delimited format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(Channel, ":", Message);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of DataGram from a raw delimited string.
        /// </summary>
        /// <param name="rawmessage"></param>
        /// <returns></returns>
        internal static DataGram ExpandFromRaw(string rawmessage)
        {
            // if the message contains valid data
            if (!string.IsNullOrEmpty(rawmessage) && rawmessage.Contains(":"))
            {
                // extract the channel name and message data
                string[] parts = rawmessage.Split(new[] {':'}, 2);
                return new DataGram(parts[0], parts[1]);
            }
            return new DataGram();
        }

        #endregion
    }
}