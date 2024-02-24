using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class Fighter
    {
        [Key]
        public int FighterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? Headshot { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Draws { get; set; }
        public int? NoContests { get; set; }
        public string? LeftStance { get; set; }
        public string? RightStance { get; set; }

        public override string ToString()
        {
            return $"FighterId: {FighterId}\nFirstName: {FirstName}\nLastName: {LastName}\nNickName: {NickName}\n" +
                   $"Weight: {Weight}\nHeight: {Height}\nAge: {Age}\nGender: {Gender}\nCitizenship: {Citizenship}\n" +
                   $"Headshot: {Headshot}\nWins: {Wins}\nLosses: {Losses}\nDraws: {Draws}\nNoContests: {NoContests}\n" +
                   $"LeftStance: {LeftStance}\nRightStance: {RightStance}";
        }
    }
}