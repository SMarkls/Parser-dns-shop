using System.Threading;

namespace Parser_dns_shop.Model
{
   static class InstanceChecker
    {
        private static Mutex mutex = new Mutex(false);
        public static bool Wait()
        {
             return mutex.WaitOne(0, true);
        }
        public static void Release()
        {
            mutex.ReleaseMutex();
        }
    }
}
