﻿using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RiotSharp.LeagueEndpoint;
using RiotSharp.SummonerEndpoint;

namespace LoLTournament.Models
{
    public class Participant
    {
        [BsonIgnore]
        private const double Season4Ratio = 0.67;

        public ObjectId Id { get; set; }
        public Summoner Summoner { get; set; }
        public Tier Season4Tier { get; set; }
        public Tier Season5Tier { get; set; }
        public int Season5Division { get; set; }
        public int Season4Wins { get; set; }
        public int Season4Losses { get; set; }
        public string SummonerName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public ObjectId TeamId { get; set; }

        /// <summary>
        /// Returns the Team this participant is competing with. Null if no team.
        /// </summary>
        [BsonIgnore]
        public Team Team
        {
            get
            {
                var client = new MongoClient();
                var server = client.GetServer();
                var db = server.GetDatabase("CLT");
                var col = db.GetCollection<Team>("Teams");

                return col.Find(Query<Team>.Where(x => x.Id == TeamId)).FirstOrDefault();
            }
        }

        public DateTime RegisterTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string StudyProgram { get; set; }
        public bool IsCaptain { get; set; }
        public bool RuStudent { get; set; }

        /// <summary>
        /// Returns the MMR for the participant, based on season 4 tier and season 5 tier and division.
        /// </summary>
        [BsonIgnore]
        public double MMR
        {
            get
            {
                double tierRanking = (int)Season4Tier * 5 + 3; // the +6 is because this is the average division
                // If also ranked in season 5, take weighted average (season 4 = 0.67, season 5 = 0.33)
                if (Season5Tier != Tier.Unranked)
                {
                    tierRanking *= Season4Ratio;
                    tierRanking += (1-Season4Ratio) * ((int) Season5Tier*5 + 5 - Season5Division);
                }

                return tierRanking * 5;
            }
        }
    }
}