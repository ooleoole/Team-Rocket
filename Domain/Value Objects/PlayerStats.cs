﻿using Domain.Value_Objects;
using System.Collections.Generic;

namespace DomainTests.Entities
{
    public class PlayerStats : ValueObject<PlayerStats>
    {
        private List<Goal> goalStats;
        private List<Assist> assistStats;
        private List<Card> cardStats;
        private List<Penalty> penaltyStats;

        public List<Goal> GoalStats { get { return goalStats; } }
        public List<Assist> AssistStats { get { return assistStats; } }
        public List<Card> CardStats { get { return cardStats; } }
        public List<Penalty> PenaltyStats { get { return penaltyStats; } }
        public int GoalCount { get { return goalStats.Count; } }
        public int AssistCount { get { return assistStats.Count; } }
        public int CardCount { get { return cardStats.Count; } }
        public int PenaltyCount { get { return penaltyStats.Count; } }

        public PlayerStats()
        {
            this.goalStats = new List<Goal>();
            this.assistStats = new List<Assist>();
            this.cardStats = new List<Card>();
            this.penaltyStats = new List<Penalty>();
        }
    }
}