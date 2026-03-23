namespace PoolingLib.Pools
{
    /// <summary>
    /// 栈对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public class StackPool<TObject> : BasePool<Stack<TObject>>, ICapacityPool<Stack<TObject>>
    {
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override Stack<TObject> Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public Stack<TObject> Get(int capacity)
        {
            if (_pool.TryDequeue(out Stack<TObject> stack))
            {
                return stack;
            }
            return new Stack<TObject>(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(Stack<TObject> stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));

            stack.Clear();
            _pool.Enqueue(stack);
        }
        /// <summary>
        /// 将<see cref="Stack{TObject}"/>转化为数组，同时返回至对象池
        /// </summary>
        /// <param name="stack">要返还的栈</param>
        /// <returns>对象数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TObject[] ToArrayRelease(Stack<TObject> stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));

            TObject[] array = [.. stack];
            Release(stack);
            return array;
        }
    }
}