using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Ningguang.Models;
using Ningguang.Models.NingItemObject;

namespace Ningguang.Component
{
    //
    public static class QuartzParameter
    {
        //job执行参数，key为taskName value 为IEnumerator<NingItem>
        //存放任务创建时的执行参数，即items
        private static readonly Hashtable JobParameterCache = new Hashtable();

        private static ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

        public static void PutJobParameterCacheSyn(string taskName, IEnumerator<NingItem> items)
        {
            rwlock.EnterWriteLock();
            try
            {
                JobParameterCache.Add(taskName, items);
            }
            finally
            {
                rwlock.ExitWriteLock();
            }
        }

        public static IEnumerator<NingItem> GetJobParameterCacheSyn(string taskName)
        {
            rwlock.EnterReadLock();
            try
            {
                return (IEnumerator<NingItem>) JobParameterCache[taskName];
            }
            finally
            {
                rwlock.ExitReadLock();
            }
        }
    }
}