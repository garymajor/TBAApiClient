using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace TbaApiClient.Cache
{
    public class FileCache
    {
        private const string extension = ".txt";

        /// <summary>
        /// The location of the Cache
        /// </summary>
        private StorageFolder folder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">The file location of the cache</param>
        public FileCache(StorageFolder location)
        {
            folder = location;
            CleanCache(14);
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
        /// <param name="age">The age (in days)</param>
        private void CleanCache(int age)
        {
            //TODO: Implement CleanCache
        }
    }
}
