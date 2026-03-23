namespace PoolingLib.Pools
{
    /// <summary>
    /// 队列对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class QueuePool<TObject> : BasePool<Queue<TObject>>,ICapacityPool<Queue<TObject>>
    {
        private const int DefaultCapacity = 512;

        /// <inheritdoc/>
        public override Queue<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        public Queue<TObject> Get(int capacity)
        {
            if (_pool.TryDequeue(out Queue<TObject> queue))
            {
                return queue;
            }
            return new Queue<TObject>(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(Queue<TObject> queue)
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
        public TObject[] ToArrayRelease(Queue<TObject> queue)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            TObject[] array = [.. queue];
            Release(queue);
            return array;
        }
    }
}
