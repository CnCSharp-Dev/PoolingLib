using System.Text;

namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="StringBuilder"/>对象池
    /// </summary>
    public class StringBuilderPool : BasePool<StringBuilder>, ICapacityPool<StringBuilder>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public new static StringBuilderPool Pool { get; } = new();
        private const int DefaultCapacity = 512;
        /// <inheritdoc/>
        public override StringBuilder Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public StringBuilder Get(int capacity)
        {
            if (_pool.TryDequeue(out StringBuilder sb))
            {
                if (sb.Capacity < capacity)
                {
                    sb.Capacity = capacity;
                }
                return sb;
            }
            return new StringBuilder(Math.Max(capacity, DefaultCapacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Release(StringBuilder sb)
        {
            if (sb == null)
                throw new ArgumentNullException(nameof(sb));

            sb.Clear();
            _pool.Enqueue(sb);
        }
        /// <summary>
        /// 将<see cref="StringBuilder"/>组合后转化为文本，同时返回至对象池
        /// </summary>
        /// <param name="sb">要返还的<see cref="StringBuilder"/></param>
        /// <returns><see cref="StringBuilder"/>转化后的字符串</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string ToStringRelease(StringBuilder sb)
        {
            if (sb == null)
                throw new ArgumentNullException(nameof(sb));

            string result = sb.ToString();
            Release(sb);
            return result;
        }
    }
}