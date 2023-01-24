using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    /// <summary>
    /// A DTO for a city without points of interest
    /// </summary>
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// Id of city
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of city
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of city
        /// </summary>
        public string? Description { get; set; }

    }
}
