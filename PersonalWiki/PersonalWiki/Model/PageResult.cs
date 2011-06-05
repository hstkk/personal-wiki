//2011 Sami Hostikka <sami@hostikka.com>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWiki
{
    /// <summary>
    /// Data model to LINQ query
    /// </summary>
    public class PageResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
