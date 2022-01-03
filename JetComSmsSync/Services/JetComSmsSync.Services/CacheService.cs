using JetComSmsSync.Services.Interfaces;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JetComSmsSync.Services
{
    public class CacheService : ICacheService
    {
        private readonly string _cacheDir;
        public CacheService()
        {
            _cacheDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "JetCom", "Telemetric", "Cache");
            if (!Directory.Exists(_cacheDir))
            {
                Directory.CreateDirectory(_cacheDir);
            }
        }

        public bool TryRead<T>(string key, out T[] output)
        {
            var path = Path.Combine(_cacheDir, $"{key}.dat");
            if (File.Exists(path))
            {
                // Read from path
                var lines = File.ReadAllLines(path);
                output = new T[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    var item = JsonConvert.DeserializeObject<T>(lines[i]);
                    output[i] = item;
                }
                return true;
            }

            output = new T[0];
            return false;
        }

        public void Append<T>(string key, IEnumerable<T> items)
        {
            var path = Path.Combine(_cacheDir, $"{key}.dat");
            File.AppendAllLines(path, items.Select(x => JsonConvert.SerializeObject(x)));
        }

        public long GetCacheSize()
        {
            var directory = new DirectoryInfo(_cacheDir);
            return DirSize(directory);
        }

        public string GetCacheSizeStr()
        {
            var size = GetCacheSize();
            return SizeToText(size);
        }

        public void ClearCache()
        {
            var d = new DirectoryInfo(_cacheDir);
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                fi.Delete();
            }

            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                di.Delete(true);
            }
        }

        private static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private string SizeToText(double size)
        {
            var multiplier = "B";
            if (size > 1024)
            {
                multiplier = "KB";
                size /= 1024;
            }
            if (size > 1024)
            {
                multiplier = "MB";
                size /= 1024;
            }
            if (size > 1024)
            {
                multiplier = "GB";
                size /= 1024;
            }
            if (size > 1024)
            {
                multiplier = "TB";
                size /= 1024;
            }
            if (size > 1024)
            {
                multiplier = "PB";
                size /= 1024;
            }

            return $"{size:N1} {multiplier}";
        }
    }
}
