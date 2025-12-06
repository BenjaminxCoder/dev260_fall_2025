using System;
using System.Collections.Generic;

namespace Assignment10
{
    /// <summary>
    /// Represents a flight route between two airports in the flight network graph.
    /// Each Flight object serves as a weighted, directed edge in the graph structure.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Three-letter code of the departure airport (e.g., "SEA")
        /// This represents the source vertex of the edge
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Three-letter code of the arrival airport (e.g., "LAX")
        /// This represents the destination vertex of the edge
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Name of the airline operating this flight (e.g., "Alaska Airlines")
        /// </summary>
        public string Airline { get; set; }

        /// <summary>
        /// Flight duration in minutes
        /// This can serve as one weight metric for the edge
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Flight cost in dollars
        /// This serves as the primary weight metric for cost-based pathfinding
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Flight number/identifier (optional)
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Default constructor for creating an empty Flight object
        /// </summary>
        public Flight()
        {
            Origin = string.Empty;
            Destination = string.Empty;
            Airline = string.Empty;
            Duration = 0;
            Cost = 0.0m;
            FlightNumber = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor for creating a Flight with all required details
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <param name="destination">Arrival airport code</param>
        /// <param name="airline">Operating airline name</param>
        /// <param name="duration">Flight duration in minutes</param>
        /// <param name="cost">Flight cost in dollars</param>
        /// <param name="flightNumber">Optional flight number</param>
        public Flight(string origin, string destination, string airline, int duration, decimal cost, string flightNumber = "")
        {
            Origin = origin?.ToUpperInvariant() ?? string.Empty;
            Destination = destination?.ToUpperInvariant() ?? string.Empty;
            Airline = airline ?? string.Empty;
            Duration = duration;
            Cost = cost;
            FlightNumber = flightNumber ?? string.Empty;
        }

        /// <summary>
        /// Returns a formatted string representation of the flight
        /// </summary>
        /// <returns>Formatted flight information</returns>
        public override string ToString()
        {
            string flightInfo = $"{Origin} → {Destination} | {Airline}";
            
            if (!string.IsNullOrEmpty(FlightNumber))
            {
                flightInfo += $" (Flight {FlightNumber})";
            }
            
            flightInfo += $" | {Duration} min | ${Cost:F2}";
            
            return flightInfo;
        }

        /// <summary>
        /// Returns a compact display format for the flight
        /// </summary>
        /// <returns>Compact flight display string</returns>
        public string ToShortString()
        {
            return $"{Origin}→{Destination} ({Airline}, ${Cost:F2})";
        }

        /// <summary>
        /// Returns detailed flight information with formatted output
        /// </summary>
        /// <returns>Detailed flight description</returns>
        public string ToDetailedString()
        {
            var details = $"Flight: {Origin} to {Destination}\n";
            details += $"  Airline: {Airline}\n";
            
            if (!string.IsNullOrEmpty(FlightNumber))
            {
                details += $"  Flight Number: {FlightNumber}\n";
            }
            
            details += $"  Duration: {Duration} minutes ({Duration / 60}h {Duration % 60}m)\n";
            details += $"  Cost: ${Cost:F2}";
            
            return details;
        }

        /// <summary>
        /// Determines whether two Flight objects are equal based on origin, destination, and airline
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if flights have same route and airline</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Flight other = (Flight)obj;
            return Origin.Equals(other.Origin, StringComparison.OrdinalIgnoreCase) &&
                   Destination.Equals(other.Destination, StringComparison.OrdinalIgnoreCase) &&
                   Airline.Equals(other.Airline, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns hash code for use in collections
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                Origin.ToUpperInvariant(),
                Destination.ToUpperInvariant(),
                Airline.ToUpperInvariant()
            );
        }

        /// <summary>
        /// Creates a reverse flight (return route) with the same properties
        /// Useful for creating bidirectional edges in the graph
        /// </summary>
        /// <returns>New Flight object with origin and destination swapped</returns>
        public Flight CreateReverseRoute()
        {
            return new Flight(
                Destination,
                Origin,
                Airline,
                Duration,
                Cost,
                FlightNumber
            );
        }
    }
}
