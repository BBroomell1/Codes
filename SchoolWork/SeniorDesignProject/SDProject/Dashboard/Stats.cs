using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Dashboard
{
    class Stats : ObservableCollection<StatsType>
    {

        // collection of objects
        public Stats()
        {

        }

        public void StatsAdd(string type, object content)
        {

            Add(new StatsType(type, content));

        }

    }
}