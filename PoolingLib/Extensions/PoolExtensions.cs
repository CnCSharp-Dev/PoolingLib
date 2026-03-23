namespace PoolingLib.Extensions
{
    /// <summary>
    /// 对象池扩展类
    /// </summary>
    public static class PoolExtensions
    {
        /// <summary>
        /// 将对象归还至指定的对象池
        /// </summary>
        /// <typeparam name="TObject">对象池的泛型</typeparam>
        /// <param name="obj">要归还的对象</param>
        /// <param name="pool">目标对象池</param>
        public static void ReleaseTo<TObject>(TObject obj,IPool<TObject> pool) where TObject : new()
        {
            pool.Release(obj);
        }
    }
}
