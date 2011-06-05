using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWiki.Model
{
    /// <summary>
    /// Data model to LINQ query
    /// </summary>
    class Revision
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
