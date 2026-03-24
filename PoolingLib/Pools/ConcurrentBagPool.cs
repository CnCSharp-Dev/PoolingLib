using System.Collections.Concurrent;

namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="ConcurrentBag{T}"/> 对象池
    /// <br>当你的集合项过多时会造成主线程的阻塞</br>
    /// </summary>
    /// <typeparam name="TObject">元素的类型</typeparam>
    [Obsolete("如果你不想在归还池子的时候卡死主线程，别使用这个东西!")]
    public class ConcurrentBagPool<TObject> : BasePool<ConcurrentBag<TObject>>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public new static ConcurrentBag<TObject> Pool { get; } = new();
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
        /// 归还对象到对象池内
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(ConcurrentBag<TObject> bag)
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
        public TObject[] ToArrayRelease(ConcurrentBag<TObject> bag)
        {
            if (bag == null)
                throw new ArgumentNullException(nameof(bag));

            TObject[] array = [.. bag];
            Release(bag);
            return array;
        }
    }
}