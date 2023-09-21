using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Exceptions;
using DSharpPlus.Net.Abstractions;
using DSharpPlus.Net.Models;
using Newtonsoft.Json;

namespace DSharpPlus.Entities
{
    public class DiscordThreadChannelMetadata
    {
        /// <summary>
        /// Gets whether this thread is archived or not.
        /// </summary>
        [JsonProperty("archived", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsArchived { get; internal set; }

        /// <summary>
        /// Gets the duration in minutes to automatically archive the thread after recent activity. Can be set to: 60, 1440, 4320, 10080.
        /// </summary>
        [JsonProperty("auto_archive_duration", NullValueHandling = NullValueHandling.Ignore)]
        public AutoArchiveDuration AutoArchiveDuration { get; internal set; }

        /// <summary>
        /// Gets the time timestamp for when the thread's archive status was last changed.
        /// </summary>
        [JsonProperty("archive_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ArchiveTimestamp { get; internal set; }

        /// <summary>
        /// Gets whether this thread is locked or not.
        /// </summary>
        [JsonProperty("locked", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsLocked { get; internal set; }


        /// <summary>
        /// Gets the time this thread was created. Only populated for threads created after 2022-01-09 (YYYY-MM-DD).
        /// </summary>
        public DateTimeOffset? CreationTimestamp
            => !string.IsNullOrWhiteSpace(this.CreateTimestampRaw) && DateTimeOffset.TryParse(this.CreateTimestampRaw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dto) ?
                dto : null;

        [JsonProperty("create_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        internal string CreateTimestampRaw { get; set; }

        internal DiscordThreadChannelMetadata() { }
    }
}
