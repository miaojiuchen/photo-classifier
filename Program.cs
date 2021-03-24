using System;
using System.Collections.Generic;
using System.IO;

namespace io
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"E:\ios_backups\XR\相机胶卷";
            var rules = new List<Rule>
            {
                new Rule(fi => fi.Extension),
                new Rule(fi => fi.CreationTime.Year.ToString()),
                // new Rule(fi => fi.CreationTime.Month.ToString()),
            };
            var classifier = new Classifier(path, @"E:\ios_backups\XR\cat", rules);
            classifier.Run();
        }
    }
}
