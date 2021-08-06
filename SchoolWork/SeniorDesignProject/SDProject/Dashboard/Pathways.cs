using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Dashboard
{
    class Pathways : ObservableCollection<PathwaysType>
    {
        public Pathways()
        {

        }

        public void PathwaysAdd(string course, string iteration)
        {
            Add(new PathwaysType(course, iteration));
        }
    }
}
