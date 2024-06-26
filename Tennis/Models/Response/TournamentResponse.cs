﻿namespace Tennis.Models.Response
{
    public class TournamentResponse
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Capacity { get; set; }
        public int Prize { get; set; }
        public string ? Winner { get; set; }
    }
}
