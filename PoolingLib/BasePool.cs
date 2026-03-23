using System.Collections.Concurrent;

namespace PoolingLib
{
    /// <summary>
    /// 一个基础的池化，可以继承该类创建其他对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class BasePool<TObject> : IPool<TObject> where TObject : new()
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public static BasePool<TObject> Pool { get; } = new();
        /// <summary>
        /// 受保护的对象池，主要用于执行归还和获取操作
        /// </summary>
        protected readonly ConcurrentQueue<TObject> _pool = new();
        /// <summary>
        /// 受保护的实例创建
        /// </summary>
        protected BasePool() { }
        /// <inheritdoc/>
        public virtual TObject Get()
        {
            if (_pool.TryDequeue(out TObject obj))
                return obj;

            return new();
        }
        /// <inheritdoc/>
        public virtual void Release(TObject obj)
        {
            _pool.Enqueue(obj);
        }
    }
}
