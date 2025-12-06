using System;
using System.Collections.Generic;

namespace Assignment10
{
    /// <summary>
    /// Represents an airport in the flight network graph.
    /// Each Airport object serves as a vertex in the graph structure.
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Three-letter IATA airport code (e.g., "SEA", "LAX", "JFK")
        /// This serves as the unique identifier for the airport
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Full name of the airport (e.g., "Seattle-Tacoma International Airport")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// City where the airport is located (e.g., "Seattle")
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country where the airport is located (e.g., "USA")
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Default constructor for creating an empty Airport object
        /// </summary>
        public Airport()
        {
            Code = string.Empty;
            Name = string.Empty;
            City = string.Empty;
            Country = "USA"; // Default to USA
        }

        /// <summary>
        /// Parameterized constructor for creating an Airport with all details
        /// </summary>
        /// <param name="code">Three-letter IATA airport code</param>
        /// <param name="name">Full airport name</param>
        /// <param name="city">City location</param>
        /// <param name="country">Country location</param>
        public Airport(string code, string name, string city, string country = "USA")
        {
            Code = code?.ToUpperInvariant() ?? string.Empty;
            Name = name ?? string.Empty;
            City = city ?? string.Empty;
            Country = country ?? "USA";
        }

        /// <summary>
        /// Returns a formatted string representation of the airport
        /// </summary>
        /// <returns>Formatted airport information</returns>
        public override string ToString()
        {
            return $"{Code} - {Name} ({City}, {Country})";
        }

        /// <summary>
        /// Determines whether two Airport objects are equal based on airport code
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if airport codes match (case-insensitive)</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Airport other = (Airport)obj;
            return Code.Equals(other.Code, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns hash code based on airport code for use in collections
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return Code.ToUpperInvariant().GetHashCode();
        }

        /// <summary>
        /// Returns a compact display format for the airport
        /// </summary>
        /// <returns>Compact airport display string</returns>
        public string ToShortString()
        {
            return $"{Code} ({City})";
        }
    }
}
