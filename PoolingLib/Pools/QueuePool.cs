namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="Queue{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class QueuePool<TObject> : BasePool<QueuePool<TObject>,Queue<TObject>>,ICapacityPool<Queue<TObject>>
    {
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override Queue<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public Queue<TObject> Get(int capacity)
        {
            if (_pool.TryDequeue(out Queue<TObject> queue))
            {
                return queue;
            }
            return new Queue<TObject>(Math.Max(DefaultCapacity, capacity));
        }
        /// <summary>
        /// 获取一个有初始内容的<see cref="Queue{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public Queue<TObject> Get(IEnumerable<TObject> collection)
        {
            if (_pool.TryDequeue(out var list))
            {
                foreach (var item in collection)
                    list.Enqueue(item);
                return list;
            }
            return new(collection);
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Return(Queue<TObject> queue)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            queue.Clear();
            _pool.Enqueue(queue);
        }
        /// <summary>
        /// 将<see cref="Queue{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="queue">要返还的队列</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayReturn(Queue<TObject> queue)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            TObject[] array = [.. queue];
            Return(queue);
            return array;
        }
        /// <summary>
        /// 将<see cref="Queue{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="queue">要返还的队列</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("已更名为ToArrayReturn方法")]
        public TObject[] ToArrayRelease(Queue<TObject> queue)
        {
            return ToArrayReturn(queue);
        }
    }
}
