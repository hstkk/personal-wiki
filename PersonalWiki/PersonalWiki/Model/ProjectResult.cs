using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PersonalWiki
{
    /// <summary>
    /// Data model to LINQ query
    /// </summary>
    public class ProjectResult : PageResult
    {
        public ObservableCollection<PageResult> Pages { get; set; }
    }
}
