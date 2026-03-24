namespace PoolingLib.Pools.Dictionaries
{
    /// <summary>
    /// 字典对象池
    /// </summary>
    /// <typeparam name="TKey">字典的<see langword="Key"/></typeparam>
    /// <typeparam name="TValue">字典的<see langword="Value"/></typeparam>
    public class DictionaryPool<TKey, TValue> : BasePool<Dictionary<TKey, TValue>>, ICapacityPool<Dictionary<TKey, TValue>>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public new static DictionaryPool<TKey, TValue> Pool { get; } = new();
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override Dictionary<TKey, TValue> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public Dictionary<TKey, TValue> Get(int capacity)
        {
            if (_pool.TryDequeue(out Dictionary<TKey, TValue> dict))
            {
                return dict;
            }
            return new Dictionary<TKey, TValue>(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(Dictionary<TKey, TValue> dict)
        {
            if (dict == null)
                throw new ArgumentNullException(nameof(dict));

            dict.Clear();
            _pool.Enqueue(dict);
        }
        /// <summary>
        /// 将<see cref="Dictionary{TKey, TValue}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="obj">要返还的字典</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public KeyValuePair<TKey, TValue>[] ToArrayRelease(Dictionary<TKey, TValue> obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            KeyValuePair<TKey, TValue>[] array = [.. obj];
            Release(obj);
            return array;
        }
    }
}