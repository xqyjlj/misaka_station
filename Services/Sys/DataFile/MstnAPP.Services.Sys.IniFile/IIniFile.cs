namespace MstnAPP.Services.Sys.DataFile
{
    public interface IIniFile
    {
        /// <summary>
        /// 读取主窗口功能列表的选中节点
        /// </summary>
        /// <returns>主窗口功能列表的选中节点</returns>
        int GetMianWindowFunctionListIndex();

        /// <summary>
        /// 设置主窗口功能列表的选中节点
        /// </summary>
        /// <param name="index">主窗口功能列表的选中节点</param>
        void SetMianWindowFunctionListIndex(int index);

        /// <summary>
        ///读取RT-Thread界面的端口
        /// </summary>
        /// <returns>RT-Thread界面的端口</returns>
        string GetRTThreadPort();

        /// <summary>
        /// 设置RT-Thread界面的端口
        /// </summary>
        /// <param name="port">RT-Thread界面的端口</param>
        void SetRTThreadPort(string port);

        /// <summary>
        /// 读取RT-Thread界面的波特率
        /// </summary>
        /// <returns>>RT-Thread界面的波特率</returns>
        int GetRTThreadBaudRate();

        /// <summary>
        /// 设置RT-Thread界面的波特率
        /// </summary>
        /// <param name="baudRate">RT-Thread界面的波特率</param>
        void SetRTThreadBaudRate(int baudRate);

        /// <summary>
        /// 读取RT-Thread界面的校验方式
        /// </summary>
        /// <returns>RT-Thread界面的校验方式</returns>
        int GetRTThreadParity();

        /// <summary>
        /// 设置RT-Thread界面的校验方式
        /// </summary>
        /// <param name="parity">RT-Thread界面的校验方式</param>
        void SetRTThreadParity(int parity);

        /// <summary>
        /// 读取RT-Thread界面的数据位
        /// </summary>
        /// <returns>RT-Thread界面的数据位</returns>
        int GetRTThreadDataBits();

        /// <summary>
        /// 设置RT-Thread界面的数据位
        /// </summary>
        /// <param name="dataBits">RT-Thread界面的数据位</param>
        void SetRTThreadDataBits(int dataBits);

        /// <summary>
        /// 读取RT-Thread界面的停止位
        /// </summary>
        /// <returns>RT-Thread界面的停止位</returns>
        int GetRTThreadStopBits();

        /// <summary>
        /// 设置RT-Thread界面的停止位
        /// </summary>
        /// <param name="stopBits">RT-Thread界面的停止位</param>
        void SetRTThreadStopBits(int stopBits);

        /// <summary>
        /// 读取RT-Thread界面的握手协议
        /// </summary>
        /// <returns>RT-Thread界面的握手协议</returns>
        int GetRTThreadHandshake();

        /// <summary>
        /// 设置RT-Thread界面的握手协议
        /// </summary>
        /// <param name="index">RT-Thread界面的握手协议</param>
        void SetRTThreadHandshake(int index);

        /// <summary>
        /// 读取RT-Thread界面的是否保存数据
        /// </summary>
        /// <returns>RT-Thread界面的是否保存数据</returns>
        bool GetRTThreadIsSaveData();

        /// <summary>
        /// 设置RT-Thread界面的是否保存数据
        /// </summary>
        /// <param name="isSaveData">RT-Thread界面的是否保存数据</param>
        void SetRTThreadIsSaveData(bool isSaveData);

        /// <summary>
        /// 读取RT-Thread界面的数据保存路径
        /// </summary>
        /// <returns>RT-Thread界面的数据保存路径</returns>
        string GetRTThreadSaveDataPath();

        /// <summary>
        /// 设置RT-Thread界面的数据保存路径
        /// </summary>
        /// <param name="saveDataPath">RT-Thread界面的数据保存路径</param>
        void SetRTThreadSaveDataPath(string saveDataPath);

        /// <summary>
        /// 读取RT-Thread界面的刷新时间
        /// </summary>
        /// <returns>RT-Thread界面的刷新时间</returns>
        int GetRTThreadFlushTime();

        /// <summary>
        /// 设置RT-Thread界面的刷新时间
        /// </summary>
        /// <param name="index">RT-Thread界面的刷新时间</param>
        void SetRTThreadFlushTime(int index);

        /// <summary>
        /// 读取RT-Thread界面的是否存在密码
        /// </summary>
        /// <returns>RT-Thread界面的是否存在密码</returns>
        bool GetRTThreadIsExistPassword();

        /// <summary>
        /// 设置RT-Thread界面的是否存在密码
        /// </summary>
        /// <param name="isExistPassword">RT-Thread界面的是否存在密码</param>
        void SetRTThreadIsExistPassword(bool isExistPassword);

        /// <summary>
        /// 读取RT-Thread界面的密码
        /// </summary>
        /// <returns>RT-Thread界面的密码</returns>
        string GetRTThreadPassword();

        /// <summary>
        /// 设置RT-Thread界面的密码
        /// </summary>
        /// <param name="password">RT-Thread界面的密码</param>
        void SetRTThreadPassword(string password);
    }
}