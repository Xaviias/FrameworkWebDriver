using System;
using System.Collections.Generic;
using System.IO;

namespace FrameworkWebDriver.Utils
{
    public class PropertyReader
    {
        private readonly Dictionary<string, string> _properties;

        public PropertyReader(string environment)
        {
            _properties = new Dictionary<string, string>();
            LoadProperties(environment);
        }

        private void LoadProperties(string environment)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", $"{environment}.properties");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Property file '{environment}.properties' not found.");
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith('#'))
                {
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        _properties[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }

        public string GetProperty(string key)
        {
            return _properties.ContainsKey(key) ? _properties[key] : throw new KeyNotFoundException($"Key '{key}' not found in property file.");
        }

        public int GetIntProperty(string key)
        {
            return int.Parse(GetProperty(key));
        }
    }
}

