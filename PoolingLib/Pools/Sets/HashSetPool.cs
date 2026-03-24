namespace PoolingLib.Pools.Sets
{
    /// <summary>
    /// 哈希列表对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class HashSetPool<TObject> : BasePool<HashSet<TObject>>, ICapacityPool<HashSet<TObject>>
    {
        private const int DefaultCapacity = 512;
        /// <summary>
        /// 对象池
        /// </summary>
        public new static HashSetPool<TObject> Pool { get; } = new();
        /// <inheritdoc/>
        public override HashSet<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public HashSet<TObject> Get(int capacity)
        {
            if (_pool.TryDequeue(out HashSet<TObject> hashSet))
            {
                return hashSet;
            }
            return new HashSet<TObject>(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(HashSet<TObject> hashSet)
        {
            if (hashSet == null)
                throw new ArgumentNullException(nameof(hashSet));

            hashSet.Clear();
            _pool.Enqueue(hashSet);
        }
        /// <summary>
        /// 将<see cref="HashSet{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="hashSet">要返还的哈希列表</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayRelease(HashSet<TObject> hashSet)
        {
            if (hashSet == null)
                throw new ArgumentNullException(nameof(hashSet));

            TObject[] array = [.. hashSet];
            Release(hashSet);
            return array;
        }
    }
}