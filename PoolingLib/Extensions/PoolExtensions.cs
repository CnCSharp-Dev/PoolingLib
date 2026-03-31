using PoolingLib.Pools;
using System.Text;

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
        public static void ReturnTo<TObject>(this TObject obj,IPool<TObject> pool) where TObject : new()
        {
            pool.Return(obj);
        }
        /// <summary>
        /// 将<see cref="StringBuilder"/>归还至指定的对象池
        /// </summary>
        /// <param name="sb">要归还的<see cref="StringBuilder"/></param>
        /// <param name="toString">是否返回文本</param>
        /// <returns><see cref="StringBuilder"/>转化成的文本，如果<see href="toString"/>参数为<see langword="false"/>则返回空字符串</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReturnTo(this StringBuilder sb, bool toString = false)
        {
            if (toString)
                return StringBuilderPool.Pool.ToStringReturn(sb);
            else
            {
                StringBuilderPool.Pool.Return(sb);
                return string.Empty;
            }
        }
    }
}
