namespace PoolingLib.Pools.Sets
{
    /// <summary>
    /// <see cref="SortedSet{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">元素类型</typeparam>
    public class SortedSetPool<TObject> : BasePool<SortedSetPool<TObject>, SortedSet<TObject>>
    {
        /// <inheritdoc/>
        public override SortedSet<TObject> Get()
        {
            if (_pool.TryDequeue(out var set))
                return set;
            return [];
        }
        /// <summary>
        /// 获取一个有初始内容的<see cref="SortedSet{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public SortedSet<TObject> Get(IEnumerable<TObject> collection)
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
        public override void Release(SortedSet<TObject> set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            set.Clear();
            _pool.Enqueue(set);
        }
        /// <summary>
        /// 将 <see cref="SortedSet{TObject}"/> 转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="set">要返还的集合</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] TObjectToArrayRelease(SortedSet<TObject> set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            TObject[] array = new TObject[set.Count];
            set.CopyTo(array);
            Release(set);
            return array;
        }
    }
}