using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWiki.Model
{
    public class RefreshUi
    {
        public event EventHandler refreshUi;
        bool refresh = false;
//        public static bool refresh { get; set; }
    }
}
