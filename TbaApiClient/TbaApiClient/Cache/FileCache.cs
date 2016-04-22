using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace TbaApiClient.Cache
{
    public class FileCache
    {
        private const string extension = ".txt";
        private const double age = 14; // default age

        /// <summary>
        /// The location of the Cache
        /// </summary>
        private StorageFolder folder;

        /// <summary>
        /// The age in days when the cache becomes invalid
        /// </summary>
        private double cacheInvalidAge;

        private NotifyTaskCompletion<bool> cacheCleaned;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">The file location of the cache</param>
        /// <param name="ageoverride">The max age of the cache in days. The default is 14.</param>
        public FileCache(StorageFolder location, double ageoverride = age)
        {
            folder = location;
            cacheInvalidAge = ageoverride;
            cacheCleaned = new NotifyTaskCompletion<bool>(CleanCache());
        }

        /// <summary>
        /// Returns the cache entry for the key
        /// </summary>
        /// <param name="cachekey">the key</param>
        /// <returns>The cache item contents if it exists; otherwise, string.Emtpy</returns>
        public async Task<string> TryGetCacheItem(string cachekey)
        {
            string filename = cachekey + extension;
            IStorageItem item = await folder.TryGetItemAsync(filename);
            string contents = string.Empty;

            if (item != null)
            {
                StorageFile file = await folder.GetFileAsync(filename);
                contents = await FileIO.ReadTextAsync(file);
            }

            return contents;
        }

        /// <summary>
        /// Stores a cache entry for the key
        /// </summary>
        /// <param name="cachekey">the key</param>
        /// <returns></returns>
        public async Task<bool> StoreCache(string cachekey, string cachevalue)
        {
            string filename = cachekey + extension;
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, cachevalue);
            return true;
        }

        /// <summary>
        /// Removes all cache entries over 'age' days old.
        /// </summary>
        private async Task<bool> CleanCache()
        {
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now.AddDays(-1 * cacheInvalidAge));
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                if (DateTimeOffset.Compare(file.DateCreated, dto) < 0) // file is older than 'age' days ago
                {
                    await file.DeleteAsync();
                }
             }

            return true;
        }
    }
}
