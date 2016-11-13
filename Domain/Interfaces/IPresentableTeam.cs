﻿using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentableTeam
    {
        TeamName Name { get; set; }
        IEnumerable<Player> Players { get; }
        ArenaName ArenaName { get; set; }
        EmailAddress Email { get; set; }
        IReadOnlyDictionary<Guid, List<Match>> TeamsSeriesSchedule { get; }
        IReadOnlyDictionary<Guid, SeriesEvents> SeriesEvents { get; }
        IReadOnlyDictionary<Guid, SeriesStats> SeriesStats { get; }
        ShirtNumbers ShirtNumbers { get; }
    }
}