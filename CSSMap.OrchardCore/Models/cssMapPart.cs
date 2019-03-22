using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSSMap.OrchardCore.Models
{
    public class cssMapPart : ContentPart
    {
        /// <summary>
        /// The Id of the Map
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The Size of the Map
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Options used in Initialization
        /// </summary>
        public string Options { get; set; }
        /// <summary>
        /// The markup
        /// </summary>
        public string Markup { get; set; }
        /// <summary>
        /// Any additional markup. 
        /// You will need to configure the options also
        /// </summary>
        public string AdditionalMarkup { get; set; }

    }
}
