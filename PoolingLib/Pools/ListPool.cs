namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="List{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class ListPool<TObject> : BasePool<ListPool<TObject>,List<TObject>>, ICapacityPool<List<TObject>>
    {
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override List<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <summary>
        /// 获取一个有初始内容的<see cref="List{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public List<TObject> Get(IEnumerable<TObject> collection)
        {
            if (_pool.TryDequeue(out var list))
            {
                foreach (var item in collection)
                    list.Add(item);
                return list;
            }
            return new(collection);
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
        public override void Return(List<TObject> list)
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
        public TObject[] ToArrayReturn(List<TObject> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            TObject[] array = [.. list];
            Return(list);
            return array;
        }
        /// <summary>
        /// 将<see cref="List{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="list">要返还的列表</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("已更名为ToArrayReturn方法")]
        public TObject[] ToArrayRelease(List<TObject> list)
        {
            return ToArrayReturn(list);
        }
    }
}
