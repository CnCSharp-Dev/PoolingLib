namespace PoolingLib
{
    /// <summary>
    /// 一个对象池的附属接口，用于实现对象池内创建初始容量对象
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public interface ICapacityPool<TObject>
    {
        /// <summary>
        /// 获取一个有初始容量的对象，如果池内没有则新建这个对象
        /// </summary>
        /// <param name="capacity">默认的初始容量</param>
        /// <returns>返回的对象</returns>
        public TObject Get(int capacity);
    }
}
