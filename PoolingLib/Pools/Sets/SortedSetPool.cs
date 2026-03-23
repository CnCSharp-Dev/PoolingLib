namespace PoolingLib.Pools.Sets
{
    /// <summary>
    /// 排序集合对象池
    /// </summary>
    /// <typeparam name="TObjectT">元素类型</typeparam>
    public class SortedSetPool<TObjectT> : BasePool<SortedSet<TObjectT>>
    {
        /// <inheritdoc/>
        public override SortedSet<TObjectT> Get()
        {
            if (_pool.TryDequeue(out var set))
                return set;
            return [];
        }
        /// <inheritdoc/>
        public override void Release(SortedSet<TObjectT> set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            set.Clear();
            _pool.Enqueue(set);
        }
        /// <summary>
        /// 将 <see cref="SortedSet{TObjectT}"/> 转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="set">要返还的集合</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObjectT[] TObjectToArrayRelease(SortedSet<TObjectT> set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            TObjectT[] array = new TObjectT[set.Count];
            set.CopyTo(array);
            Release(set);
            return array;
        }
    }
}
