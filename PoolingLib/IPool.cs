namespace PoolingLib
{
    /// <summary>
    /// 一个对象池接口，用于实现对象池
    /// </summary>
    /// <typeparam name="TObject">对象的类型</typeparam>
    public interface IPool<TObject>
    {
        /// <summary>
        /// 获取一个对象，如果池内没有则新建这个对象
        /// </summary>
        /// <returns>返回的对象</returns>
        public TObject Get();
        /// <summary>
        /// 归还对象到对象池内
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        public void Release(TObject obj);
    }
}
