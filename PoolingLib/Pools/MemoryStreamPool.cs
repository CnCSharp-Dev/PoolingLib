namespace PoolingLib.Pools
{
    /// <summary>
    /// <see cref="MemoryStream"/>对象池
    /// </summary>
    public class MemoryStreamPool : BasePool<MemoryStreamPool,MemoryStream>, ICapacityPool<MemoryStream>
    {
        private const int DefaultCapacity = 1024;
        /// <inheritdoc/>
        public override MemoryStream Get()
        {
            return Get(DefaultCapacity);
        }
        /// <inheritdoc/>
        public MemoryStream Get(int capacity)
        {
            if (_pool.TryDequeue(out MemoryStream ms))
            {
                if (ms.Capacity < capacity)
                {
                    ms.Capacity = capacity;
                }
                if (ms.Length != 0)
                {
                    ms.SetLength(0);
                }
                return ms;
            }
            return new MemoryStream(Math.Max(DefaultCapacity, capacity));
        }
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Return(MemoryStream ms)
        {
            if (ms == null)
                throw new ArgumentNullException(nameof(ms));
            ms.SetLength(0);
            _pool.Enqueue(ms);
        }
        /// <summary>
        /// 将 <see cref="MemoryStream"/> 转换为字节数组，同时返回至对象池
        /// </summary>
        /// <param name="ms">要返还的流</param>
        /// <returns>包含当前流数据的字节数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] ToByteArrayReturn(MemoryStream ms)
        {
            if (ms == null)
                throw new ArgumentNullException(nameof(ms));

            byte[] array = ms.ToArray();
            Return(ms);
            return array;
        }
    }
}