namespace PoolingLib.Pools.Sets
{
    /// <summary>
    /// <see cref="HashSet{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class HashSetPool<TObject> : BasePool<HashSetPool<TObject>, HashSet<TObject>>
    {
        /// <summary>
        /// 获取一个有初始内容的<see cref="HashSet{TObject}"/>，如果池内没有则新建
        /// </summary>
        /// <param name="collection">初始的内容</param>
        /// <returns>返回的对象</returns>
        public HashSet<TObject> Get(IEnumerable<TObject> collection)
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
        /// <exception cref="ArgumentNullException"></exception>
        public override void Return(HashSet<TObject> hashSet)
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
        public TObject[] ToArrayReturn(HashSet<TObject> hashSet)
        {
            if (hashSet == null)
                throw new ArgumentNullException(nameof(hashSet));

            TObject[] array = [.. hashSet];
            Return(hashSet);
            return array;
        }
        /// <summary>
        /// 将<see cref="HashSet{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="hashSet">要返还的栈</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("已更名为ToArrayReturn方法")]
        public TObject[] ToArrayRelease(HashSet<TObject> hashSet)
        {
            return ToArrayReturn(hashSet);
        }
    }
}