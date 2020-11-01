using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DLLHomework
{
    class Metadata
    {
        public char Gender;
        public string Name;
        public int Rank;

        public Metadata(string name, char gender, int rank)
        {
            Name = name;
            Gender = gender;
            Rank = rank;
        }

        public override bool Equals(object obj)
        {
            return obj is Metadata metadata &&
                   Gender.ToString().ToLower() == metadata.Gender.ToString().ToLower() &&
                   Name.ToLower() == metadata.Name.ToLower() &&
                   Rank == metadata.Rank;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Gender, Name, Rank);
        }

        public static bool operator ==(Metadata left, Metadata right)
        {
            return EqualityComparer<Metadata>.Default.Equals(left, right);
        }

        public static bool operator !=(Metadata left, Metadata right)
        {
            return !(left == right);
        }

        public static bool operator <(Metadata left, Metadata right)
        {
            return (left.Name.ToLower().CompareTo(right.Name.ToLower()) < 0);
        }

        public static bool operator >(Metadata left, Metadata right)
        {
            return (left.Name.ToLower().CompareTo(right.Name.ToLower()) > 0);
        }

        public static bool operator <=(Metadata left, Metadata right)
        {
            return (left.Name.ToLower().CompareTo(right.Name.ToLower()) <= 0);
        }

        public static bool operator >=(Metadata left, Metadata right)
        {
            return (left.Name.ToLower().CompareTo(right.Name.ToLower()) >= 0);
        }
    }
}
