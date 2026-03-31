using System.Collections.Concurrent;

namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="ConcurrentBag{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">元素的类型</typeparam>
    public class ConcurrentBagPool<TObject> : BasePool<ConcurrentBagPool<TObject>, ConcurrentBag<TObject>>
    {
        /// <inheritdoc/>
        public override ConcurrentBag<TObject> Get()
        {
            if (_pool.TryDequeue(out ConcurrentBag<TObject> bag))
            {
                return bag;
            }
            return [];
        }
        /// <summary>
        /// 获取一个有初始内容的<see cref="ConcurrentBag{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public ConcurrentBag<TObject> Get(IEnumerable<TObject> collection)
        {
            if (_pool.TryDequeue(out var list))
            {
                foreach (var item in collection)
                    list.Add(item);
                return list;
            }
            return new(collection);
        }
        /// <summary>
        /// 归还对象到对象池内
        /// </summary>
        /// <param name="bag">要归还的对象</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Return(ConcurrentBag<TObject> bag)
        {
            if (bag == null)
                throw new ArgumentNullException(nameof(bag));

            while (bag.TryTake(out _)) { }

            _pool.Enqueue(bag);
        }
        /// <summary>
        /// 将 <see cref="ConcurrentBag{TObject}"/> 转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="bag">要返还的包</param>
        /// <returns>包含包中所有元素的数组</returns>
        public TObject[] ToArrayReturn(ConcurrentBag<TObject> bag)
        {
            if (bag == null)
                throw new ArgumentNullException(nameof(bag));

            TObject[] array = [.. bag];
            Return(bag);
            return array;
        }       
        /// <summary>
        /// 将 <see cref="ConcurrentBag{TObject}"/> 转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="bag">要返还的包</param>
        /// <returns>包含包中所有元素的数组</returns>
        [Obsolete("已更名为ToArrayReturn方法")]
        public TObject[] ToArrayRelease(ConcurrentBag<TObject> bag)
        {
            return ToArrayReturn(bag);
        }
    }
}