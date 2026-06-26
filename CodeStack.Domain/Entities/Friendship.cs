using CodeStack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeStack.Domain.Entities
{
    public class Friendship
    {
        public Guid Id { get; set; }

        public Guid RequesterId { get; set; }
        public User Requester { get; set; } = null!;

        public Guid AddresseeId { get; set; }
        public User Addressee { get; set; } = null!;

        public FriendshipStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
    }
}
