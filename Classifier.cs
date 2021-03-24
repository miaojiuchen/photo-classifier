using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace io
{
    class Classifier
    {
        private List<Rule> _rules;

        private string _sourceDir;

        private string _targetDir;

        public Classifier(string sourceDir, string targetDir, List<Rule> rules)
        {
            if (!Directory.Exists(sourceDir))
            {
                throw new InvalidDataException($"sourceDir not exist, {sourceDir}");
            }

            var di = new DirectoryInfo(targetDir);
            if (!di.Exists)
            {
                di.Create();
            }

            _rules = rules;
            _sourceDir = sourceDir;
            _targetDir = targetDir;
        }

        public void Run()
        {
            var files = Directory.EnumerateFiles(_sourceDir);
            var parts = new List<string>() { _targetDir };

            var count = files.Count();
            var tasks = new List<Task>(count);
            int index = 1;

            foreach (var file in files)
            {
                foreach (var rule in _rules)
                {
                    parts.Add(rule.Apply(new FileInfo(file)));
                }
                parts.Add(Path.GetFileName(file));
                var targetFilePath = Path.Combine(parts.ToArray());
                tasks.Add(
                    Task.Run(() =>
                    {
                        if (!File.Exists(targetFilePath))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
                            File.Copy(file, targetFilePath);
                        }
                        Console.WriteLine($"\r{index}/{count} items moved");
                        Interlocked.Increment(ref index);
                    })
                );
                parts.RemoveRange(1, parts.Count - 1);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}