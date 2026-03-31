using System.Collections.Concurrent;

namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="LinkedList{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class LinkedListPool<TObject> : BasePool<LinkedListPool<TObject>,LinkedList<TObject>>
    {
        /// <inheritdoc/>
        public override LinkedList<TObject> Get()
        {
            if (_pool.TryDequeue(out var list))
            {
                return list;
            }
            return [];
        }
        /// <summary>
        /// 获取一个有初始内容的<see cref="LinkedList{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public LinkedList<TObject> Get(IEnumerable<TObject> collection)
        {
            if (_pool.TryDequeue(out var list))
            {
                foreach (var item in collection)
                    list.AddLast(item);
                return list;
            }
            return new(collection);
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Return(LinkedList<TObject> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            list.Clear();
            _pool.Enqueue(list);
        }
        /// <summary>
        /// 将<see cref="LinkedList{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="list">要返还的哈希列表</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayReturn(LinkedList<TObject> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            TObject[] array = [.. list];
            Return(list);
            return array;
        }
        /// <summary>
        /// 将<see cref="LinkedList{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="list">要返还的哈希列表</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("已更名为ToArrayReturn方法")]
        public TObject[] ToArrayRelease(LinkedList<TObject> list)
        {
            return ToArrayReturn(list);
        }
    }
}
