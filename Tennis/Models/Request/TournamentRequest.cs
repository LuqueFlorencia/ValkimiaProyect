﻿using System.Text.Json.Serialization;
using Tennis.Helpers;

namespace Tennis.Models.Request
{
    public class TournamentRequest
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        //public DateOnly StartDate { get; set; }
        //public DateOnly EndDate { get; set; }
        public int Capacity { get; set; }
        public int Prize { get; set; }
    }
}
