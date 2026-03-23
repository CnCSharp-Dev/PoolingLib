namespace PoolingLib.Pools
{
    /// <summary>
    /// 列表对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class ListPool<TObject> : BasePool<List<TObject>>, ICapacityPool<List<TObject>>
    {
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override List<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public List<TObject> Get(int capacity)
        {
            if (_pool.TryDequeue(out List<TObject> list))
            {
                if (list.Capacity < capacity)
                    list.Capacity = capacity;
                return list;
            }
            return new List<TObject>(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(List<TObject> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            list.Clear();
            _pool.Enqueue(list);
        }
        /// <summary>
        /// 将<see cref="List{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="list">要返还的列表</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayRelease(List<TObject> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            TObject[] array = [.. list];
            Release(list);
            return array;
        }
    }
}
