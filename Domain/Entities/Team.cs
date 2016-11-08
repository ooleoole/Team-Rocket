﻿using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team
    {
        public Guid Id { get; }
        public TeamName Name { get; set; }
        public HashSet<Guid> PlayerIds { get; }
        public ArenaName Arena { get; set; }
        public EmailAddress Email { get ; set; }

        public Team(TeamName name, ArenaName arenaName,EmailAddress email)
        {
            this.Id = new Guid();
            this.Name = name;
            this.PlayerIds = new HashSet<Guid>();
            this.Arena = arenaName;
            this.Email = email;
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}