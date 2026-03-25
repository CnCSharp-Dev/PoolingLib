using System.Collections.ObjectModel;

namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="Collection{TObject}"/>对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class CollectionPool<TObject> : BasePool<CollectionPool<TObject>, Collection<TObject>>
    {
        /// <summary>
        /// 获取一个有初始内容的集合，如果池内没有则新建
        /// </summary>
        /// <param name="list">初始的内容</param>
        /// <returns>返回的对象</returns>
        public Collection<TObject> Get(IList<TObject> list)
        {
            if (_pool.TryDequeue(out Collection<TObject> collection))
            {
                foreach (var p in list)
                    collection.Add(p);
                return collection;
            }
            return new Collection<TObject>(list);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(Collection<TObject> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            collection.Clear();
            _pool.Enqueue(collection);
        }
        /// <summary>
        /// 将<see cref="Collection{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="collection">要返还的栈</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayRelease(Collection<TObject> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            TObject[] array = [.. collection];
            Release(collection);
            return array;
        }
    }
}
