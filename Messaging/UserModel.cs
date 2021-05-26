using System;

namespace TrackService.Messaging
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public bool IsArtist { get; set; }
    }
}
