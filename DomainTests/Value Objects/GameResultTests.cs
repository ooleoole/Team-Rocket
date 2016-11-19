﻿using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GameResultTests
    {
        private Team teamOne = new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"),
            new EmailAddress("ifkgbg@ifk.se"));

        private Team teamTwo = new Team(new TeamName("AIK"), new ArenaName("Friends Arena"),
            new EmailAddress("aik@aik.se"));

        [TestMethod]
        public void GameResultIsEqualToValidEntries()
        {
            var gameResult = new GameResult(teamOne.Id, teamTwo.Id, 3, 0);

            Assert.AreEqual(gameResult.HomeTeamId, teamOne.Id);
            Assert.AreEqual(gameResult.HomeTeam_Score, 3);
            Assert.AreEqual(gameResult.AwayTeamId, teamTwo.Id);
            Assert.AreEqual(gameResult.AwayTeam_Score, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfHomeTeamScoreIsLessThanZero()
        {
            var gameResult = new GameResult(teamOne.Id, teamTwo.Id, -1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfAwayTeamScoreIsLessThanZero()
        {
            var gameResult = new GameResult(teamOne.Id, teamTwo.Id, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfHomeTeamScoreIsGreaterThanFifty()
        {
            var gameResult = new GameResult(teamOne.Id, teamTwo.Id, 51, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfAwayTeamScoreIsGreaterThanFifty()
        {
            var gameResult = new GameResult(teamOne.Id, teamTwo.Id, 0, 51);
        }

        [TestMethod]
        public void GameResultIsComparableByValue()
        {
            var gameResultOne = new GameResult(teamOne.Id, teamTwo.Id, 3, 0);
            var gameResultTwo = new GameResult(teamOne.Id, teamTwo.Id, 3, 0);
            Assert.AreEqual(gameResultOne, gameResultTwo);
            Assert.IsTrue(gameResultOne == gameResultTwo);
        }

        [TestMethod]
        public void GameResultWorksWithHashSet()
        {
            var gameResultOne = new GameResult(teamOne.Id, teamTwo.Id, 3, 0);
            var gameResultTwo = new GameResult(teamOne.Id, teamTwo.Id, 3, 0);
            var gameResultHashSet = new HashSet<GameResult>();
            gameResultHashSet.Add(gameResultOne);
            gameResultHashSet.Add(gameResultTwo);
            Assert.IsTrue(gameResultHashSet.Count == 1);
        }

        [TestMethod]
        public void GameResultCalculationIsWorking()
        {
            var games = DomainService.GetAllGames();
            foreach (var game in games)
            {
                
                var result = game.Protocol.GameResult;
                var awayScore = 0;
                var homeScore = 0;
                awayScore += game.Protocol.Goals.Count(goal => goal.TeamId == result.AwayTeamId);
                homeScore += game.Protocol.Goals.Count(goal => goal.TeamId == result.HomeTeamId);

                Assert.IsTrue(awayScore == result.AwayTeam_Score);
                Assert.IsTrue(homeScore == result.HomeTeam_Score);

            }
            
        }
    }
}