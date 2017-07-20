﻿using NTwitch.Chat.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat
{
    internal class CacheManager
    {
        private readonly ICacheClient<ulong, ChatSimpleUser> _users;
        private readonly ICacheClient<ulong, ChatSimpleChannel> _channels;
        private readonly ICacheClient<string, ChatMessage> _messages;
        private readonly ICacheClient<string, UserStateEvent> _userStates;

        private readonly ConcurrentDictionary<string, List<string>> _names;
        
        public IReadOnlyCollection<ChatSimpleUser> Users => _users.Entities;
        public IReadOnlyCollection<ChatSimpleChannel> Channels => _channels.Entities;
        public IReadOnlyCollection<ChatMessage> Messages => _messages.Entities;
        public IReadOnlyCollection<UserStateEvent> UserStates => _userStates.Entities;

        public CacheManager(int msgCacheSize, ICacheClientProvider cacheProvider)
        {
            _users = cacheProvider.Create<ulong, ChatSimpleUser>(-1);
            _channels = cacheProvider.Create<ulong, ChatSimpleChannel>(-1);
            _messages = cacheProvider.Create<string, ChatMessage>(msgCacheSize);
            _userStates = cacheProvider.Create<string, UserStateEvent>(-1);
            _names = new ConcurrentDictionary<string, List<string>>();
        }

        public void AddUser(ChatSimpleUser user)
            => _users.Add(user.Id, user);
        public void AddChannel(ChatSimpleChannel channel)
            => _channels.Add(channel.Id, channel);
        public void AddMessage(ChatMessage message)
            => _messages.Add(message.Id, message);
        public void AddUserState(UserStateEvent userState)
            => _userStates.Add(userState.ChannelName, userState);
        public void AddNames(string channelName, string[] names)
        {
            if (_names.TryGetValue(channelName, out List<string> existing))
            {
                existing.AddRange(names);
                _names.AddOrUpdate(channelName, existing, (n, l) => l);
            } else
            {
                _names.TryAdd(channelName, new List<string>(names));
            }
        }

        public ChatSimpleUser RemoveUser(ulong userId)
            => _users.Remove(userId);
        public ChatSimpleChannel RemoveChannel(ulong channelId)
            => _channels.Remove(channelId);
        public ChatMessage RemoveMessage(string messageId)
            => _messages.Remove(messageId);
        public UserStateEvent RemoveUserState(string channelName)
            => _userStates.Remove(channelName);
        public List<string> RemoveNames(string channelName)
        {
            if (_names.TryRemove(channelName, out List<string> value))
                return value;
            return null;
        }

        public ChatSimpleUser GetUser(ulong userId)
            => _users.Get(userId);
        public ChatSimpleChannel GetChannel(ulong channelId)
            => _channels.Get(channelId);
        public ChatMessage GetMessage(string messageId)
            => _messages.Get(messageId);

        public ChatSimpleUser GetUser(string userName)
            => Users.SingleOrDefault(x => x.Name == userName);
        public ChatSimpleChannel GetChannel(string channelName)
            => Channels.SingleOrDefault(x => x.Name == channelName);
        public UserStateEvent GetUserState(string channelName)
            => _userStates.Get(channelName);
        public List<string> GetNames(string channelName)
        {
            if (_names.TryGetValue(channelName, out List<string> value))
                return value;
            return null;
        }

        public ChatSimpleUser GetOrAddUser(ulong userId, Func<ulong, ChatSimpleUser> userFactory)
            => _users.GetOrAdd(userId, userFactory);
        public ChatSimpleChannel GetOrAddChannel(ulong channelId, Func<ulong, ChatSimpleChannel> channelFactory)
            => _channels.GetOrAdd(channelId, channelFactory);
        public ChatMessage GetOrAddMessage(string messageId, Func<string, ChatMessage> messageFactory)
            => _messages.GetOrAdd(messageId, messageFactory);
    }
}
