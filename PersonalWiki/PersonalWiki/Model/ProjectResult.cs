﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PersonalWiki
{
    public class ProjectResult : PageResult
    {
        //todo:Laziness
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<PageResult> Pages { get; set; }
    }
}
