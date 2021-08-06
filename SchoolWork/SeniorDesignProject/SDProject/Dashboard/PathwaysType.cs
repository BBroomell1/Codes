using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dashboard
{
    class PathwaysType : INotifyPropertyChanged
    {
        private string _course;
        private string _iteration;

        public PathwaysType()
        {
        }

        public PathwaysType(string course, string iteration)
        {
            _course = course;
            _iteration = iteration;
        }

        public String Course
        {
            get { return _course; }
            set
            {
                _course = value;
                OnPropertyChanged("" + QueryLib.courseCD + "");
            }
        }

        public String Iteration
        {
            get { return _iteration; }
            set
            {
                _iteration = value;
                OnPropertyChanged("" + QueryLib.classNo + "");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => _course;       

        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}
