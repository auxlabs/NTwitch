﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.User;

namespace NTwitch.Helix.Rest
{
    public class RestUser : RestEntity<ulong>
    {
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Type { get; private set; }
        public string BroadcasterType { get; private set; }
        public string Description { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string OfflineImageUrl { get; private set; }
        public string ViewCount { get; private set; }

        internal RestUser(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal static RestUser Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestUser(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            if (model.Name.IsSpecified)
                Name = model.Name.Value;
            if (model.Username.IsSpecified)
                Username = model.Username.Value;
            if (model.Type.IsSpecified)
                Type = model.Type.Value;
            if (model.BroadcasterType.IsSpecified)
                BroadcasterType = model.BroadcasterType.Value;
            if (model.Description.IsSpecified)
                Description = model.Description.Value;
            if (model.ProfileImageUrl.IsSpecified)
                ProfileImageUrl = model.ProfileImageUrl.Value;
            if (model.OfflineImageUrl.IsSpecified)
                OfflineImageUrl = model.OfflineImageUrl.Value;
            if (model.ViewCount.IsSpecified)
                ViewCount = model.ViewCount.Value;
        }
        
        // Update User
        public async Task<RestUser> ModifyAsync(RequestOptions options = null) => throw new NotImplementedException();
        // Get Streams
        public async Task<RestBroadcast> GetBroadcastAsync(RequestOptions options = null) => throw new NotImplementedException();
        // Get User Followers <RestFollow[]>
        public async Task GetFollowersAsync(RequestOptions options = null) => throw new NotImplementedException();
        // Get User Following <RestFollow[]>
        public async Task GetFollowingAsync(RequestOptions options = null) => throw new NotImplementedException();
        // Get User Videos <RestVideo>
        public async Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(RequestOptions options = null) => throw new NotImplementedException();
        // Get User (Id)
        public Task UpdateAsync(RequestOptions options = null) => throw new NotImplementedException();
    }
}
