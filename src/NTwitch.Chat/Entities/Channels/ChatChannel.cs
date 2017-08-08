﻿using RoomStateModel = NTwitch.Chat.API.RoomStateEvent;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatSimpleChannel//, IChannel
    {
        /// <summary>  </summary>
        public string BroadcasterLanguage { get; internal set; }
        /// <summary>  </summary>
        public int FollowersOnlyMode { get; internal set; }
        /// <summary>  </summary>
        public bool IsFollowersOnly { get; internal set; }
        /// <summary>  </summary>
        public bool IsEmoteOnly { get; internal set; }
        /// <summary>  </summary>
        public bool IsR9k { get; internal set; }
        /// <summary>  </summary>
        public bool IsSlow { get; internal set; }
        /// <summary>  </summary>
        public bool IsSubsOnly { get; internal set; }

        public ChatChannel(TwitchChatClient client, ulong id)
            : base(client, id) { }

        internal new static ChatChannel Create(TwitchChatClient client, RoomStateModel model)
        {
            var entity = new ChatChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal override void Update(RoomStateModel model)
        {
            base.Update(model);
            BroadcasterLanguage = model.BroadcasterLang;
            FollowersOnlyMode = model.FollowersOnlyMode;
            IsFollowersOnly = model.FollowersOnlyMode > 0;
            IsR9k = model.IsR9k;
            IsSlow = model.IsSlow;
            IsSubsOnly = model.IsSubsOnly;
        }
    }
}
