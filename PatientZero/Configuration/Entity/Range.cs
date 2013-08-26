using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration.Entity
{
    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Range(int min, int max) {
            Min = min;
            Max = max;
        }

        public Range(int value)
            : this(value, value) {
        }

        public static Range Parse(string range) {
            if (range == "unbounded") return new Range(0, int.MaxValue);
            if (range.Contains('-')) {
                string[] parts = range.Split('-');
                return new Range(int.Parse(parts[0]), int.Parse(parts[1]));
            }
            return new Range(int.Parse(range));
        }
    }
}