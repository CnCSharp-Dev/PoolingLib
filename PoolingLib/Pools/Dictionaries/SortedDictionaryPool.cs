namespace PoolingLib.Pools.Dictionaries
{
    /// <summary>
    /// 排序字典对象池
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class SortedDictionaryPool<TKey, TValue> : BasePool<SortedDictionary<TKey, TValue>>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public new static SortedDictionaryPool<TKey, TValue> Pool { get; } = new();
        /// <inheritdoc/>
        public override SortedDictionary<TKey, TValue> Get()
        {
            if (_pool.TryDequeue(out var dict))
                return dict;
            return [];
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(SortedDictionary<TKey, TValue> dict)
        {
            if (dict == null)
                throw new ArgumentNullException(nameof(dict));

            dict.Clear();
            _pool.Enqueue(dict);
        }
        /// <summary>
        /// 将 <see cref="SortedDictionary{TKey, TValue}"/> 转换为键值对数组，同时返回至对象池
        /// </summary>
        /// <param name="dict">要返还的字典</param>
        /// <returns>键值对数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public KeyValuePair<TKey, TValue>[] ToArrayRelease(SortedDictionary<TKey, TValue> dict)
        {
            if (dict == null)
                throw new ArgumentNullException(nameof(dict));

            KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[dict.Count];
            ((ICollection<KeyValuePair<TKey, TValue>>)dict).CopyTo(array, 0);
            Release(dict);
            return array;
        }
    }
}
