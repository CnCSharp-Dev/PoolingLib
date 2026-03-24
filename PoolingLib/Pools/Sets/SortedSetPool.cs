namespace PoolingLib.Pools.Sets
{
    /// <summary>
    /// 排序集合对象池
    /// </summary>
    /// <typeparam name="TObject">元素类型</typeparam>
    public class SortedSetPool<TObject> : BasePool<SortedSet<TObject>>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public new static SortedSetPool<TObject> Pool { get; } = new();
        /// <inheritdoc/>
        public override SortedSet<TObject> Get()
        {
            if (_pool.TryDequeue(out var set))
                return set;
            return [];
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
