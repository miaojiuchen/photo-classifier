using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace io
{
    class Rule
    {
        private Func<FileInfo, string> _partition;

        public Rule(Func<FileInfo, string> partition)
        {
            _partition = partition;
        }

        public string Apply(FileInfo fi)
        {
            return _partition(fi);
        }
    }
}