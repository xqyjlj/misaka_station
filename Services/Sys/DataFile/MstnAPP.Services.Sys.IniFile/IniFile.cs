using IniParser;
using IniParser.Model;
using System;
using System.Globalization;
using System.IO;

namespace MstnAPP.Services.Sys.IniFile
{
    public class IniFile : IIniFile
    {
        private const string FilePath = "Misaka-Station.ini";
        private readonly FileIniDataParser _parser = new();
        private IniData _iniData;

        public IniFile()
        {
            if (!File.Exists(FilePath))
            {
                InitIniFile();
            }
            _iniData = _parser.ReadFile(FilePath);
        }

        private void InitIniFile()
        {
            IniData data = new();

            _ = data.Sections.AddSection("MainWindow");
            _ = data["MainWindow"].AddKey("FunctionListIndex", "0");

            _ = data["RT-Thread"].AddKey("Port", "");
            _ = data["RT-Thread"].AddKey("BaudRate", "0");
            _ = data["RT-Thread"].AddKey("Parity", "0");
            _ = data["RT-Thread"].AddKey("DataBits", "0");
            _ = data["RT-Thread"].AddKey("StopBits", "0");
            _ = data["RT-Thread"].AddKey("Handshake", "0");
            _ = data["RT-Thread"].AddKey("IsSaveData", "false");
            _ = data["RT-Thread"].AddKey("SaveDataPath", "");
            _ = data["RT-Thread"].AddKey("FlushTime", "500");
            _ = data["RT-Thread"].AddKey("IsExistPassword", "false");
            _ = data["RT-Thread"].AddKey("Password", "");

            _parser.WriteFile(FilePath, data);
        }

        #region MianWindow

        #region MianWindowFunctionListIndex

        /// <summary>
        /// 读取主窗口功能列表的选中节点
        /// </summary>
        /// <returns>主窗口功能列表的选中节点</returns>
        public int GetMainWindowFunctionListIndex()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["MainWindow"]["FunctionListIndex"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置主窗口功能列表的选中节点
        /// </summary>
        /// <param name="index">主窗口功能列表的选中节点</param>
        public void SetMainWindowFunctionListIndex(int index)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["MainWindow"]["FunctionListIndex"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion MianWindowFunctionListIndex

        #endregion MianWindow

        #region RT-Thread

        #region Port

        /// <summary>
        ///读取RT-Thread界面的端口
        /// </summary>
        /// <returns>RT-Thread界面的端口</returns>
        public string GetRTThreadPort()
        {
            _iniData = _parser.ReadFile(FilePath);
            return _iniData["RT-Thread"]["Port"];
        }

        /// <summary>
        /// 设置RT-Thread界面的端口
        /// </summary>
        /// <param name="port">RT-Thread界面的端口</param>
        public void SetRTThreadPort(string port)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["Port"] = port;
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion Port

        #region BaudRate

        /// <summary>
        /// 读取RT-Thread界面的波特率
        /// </summary>
        /// <returns>>RT-Thread界面的波特率</returns>
        public int GetRTThreadBaudRate()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["BaudRate"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的波特率
        /// </summary>
        /// <param name="baudRate">RT-Thread界面的波特率</param>
        public void SetRTThreadBaudRate(int baudRate)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["BaudRate"] = baudRate.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion BaudRate

        #region Parity

        /// <summary>
        /// 读取RT-Thread界面的校验方式
        /// </summary>
        /// <returns>RT-Thread界面的校验方式</returns>
        public int GetRTThreadParity()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["Parity"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的校验方式
        /// </summary>
        /// <param name="parity">RT-Thread界面的校验方式</param>
        public void SetRTThreadParity(int parity)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["Parity"] = parity.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion Parity

        #region DataBits

        /// <summary>
        /// 读取RT-Thread界面的数据位
        /// </summary>
        /// <returns>RT-Thread界面的数据位</returns>
        public int GetRTThreadDataBits()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["DataBits"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的数据位
        /// </summary>
        /// <param name="dataBits">RT-Thread界面的数据位</param>
        public void SetRTThreadDataBits(int dataBits)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["DataBits"] = dataBits.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion DataBits

        #region StopBits

        /// <summary>
        /// 读取RT-Thread界面的停止位
        /// </summary>
        /// <returns>RT-Thread界面的停止位</returns>
        public int GetRTThreadStopBits()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["StopBits"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的停止位
        /// </summary>
        /// <param name="stopBits">RT-Thread界面的停止位</param>
        public void SetRTThreadStopBits(int stopBits)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["StopBits"] = stopBits.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion StopBits

        #region Handshake

        /// <summary>
        /// 读取RT-Thread界面的握手协议
        /// </summary>
        /// <returns>RT-Thread界面的握手协议</returns>
        public int GetRTThreadHandshake()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["Handshake"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的握手协议
        /// </summary>
        /// <param name="index">RT-Thread界面的握手协议</param>
        public void SetRTThreadHandshake(int index)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["Handshake"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion Handshake

        #region IsSaveData

        /// <summary>
        /// 读取RT-Thread界面的是否保存数据
        /// </summary>
        /// <returns>RT-Thread界面的是否保存数据</returns>
        public bool GetRTThreadIsSaveData()
        {
            _iniData = _parser.ReadFile(FilePath);
            return Convert.ToBoolean(_iniData["RT-Thread"]["IsSaveData"], new CultureInfo("zh-CN", false));
        }

        /// <summary>
        /// 设置RT-Thread界面的是否保存数据
        /// </summary>
        /// <param name="isSaveData">RT-Thread界面的是否保存数据</param>
        public void SetRTThreadIsSaveData(bool isSaveData)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["IsSaveData"] = isSaveData.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion IsSaveData

        #region SaveDataPath

        /// <summary>
        /// 读取RT-Thread界面的数据保存路径
        /// </summary>
        /// <returns>RT-Thread界面的数据保存路径</returns>
        public string GetRTThreadSaveDataPath()
        {
            _iniData = _parser.ReadFile(FilePath);
            return _iniData["RT-Thread"]["SaveDataPath"];
        }

        /// <summary>
        /// 设置RT-Thread界面的数据保存路径
        /// </summary>
        /// <param name="saveDataPath">RT-Thread界面的数据保存路径</param>
        public void SetRTThreadSaveDataPath(string saveDataPath)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["SaveDataPath"] = saveDataPath;
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion SaveDataPath

        #region FlushTime

        /// <summary>
        /// 读取RT-Thread界面的刷新时间
        /// </summary>
        /// <returns>RT-Thread界面的刷新时间</returns>
        public int GetRTThreadFlushTime()
        {
            _iniData = _parser.ReadFile(FilePath);
            _ = int.TryParse(_iniData["RT-Thread"]["FlushTime"], out int ret);
            return ret;
        }

        /// <summary>
        /// 设置RT-Thread界面的刷新时间
        /// </summary>
        /// <param name="index">RT-Thread界面的刷新时间</param>
        public void SetRTThreadFlushTime(int index)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["FlushTime"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion FlushTime

        #region IsExistPassword

        /// <summary>
        /// 读取RT-Thread界面的是否存在密码
        /// </summary>
        /// <returns>RT-Thread界面的是否存在密码</returns>
        public bool GetRTThreadIsExistPassword()
        {
            _iniData = _parser.ReadFile(FilePath);
            return Convert.ToBoolean(_iniData["RT-Thread"]["IsExistPassword"], new CultureInfo("zh-CN", false));
        }

        /// <summary>
        /// 设置RT-Thread界面的是否存在密码
        /// </summary>
        /// <param name="isExistPassword">RT-Thread界面的是否存在密码</param>
        public void SetRTThreadIsExistPassword(bool isExistPassword)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["IsExistPassword"] = isExistPassword.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion IsExistPassword

        #region Password

        /// <summary>
        /// 读取RT-Thread界面的密码
        /// </summary>
        /// <returns>RT-Thread界面的密码</returns>
        public string GetRTThreadPassword()
        {
            _iniData = _parser.ReadFile(FilePath);
            return _iniData["RT-Thread"]["Password"];
        }

        /// <summary>
        /// 设置RT-Thread界面的密码
        /// </summary>
        /// <param name="password">RT-Thread界面的密码</param>
        public void SetRTThreadPassword(string password)
        {
            _iniData = _parser.ReadFile(FilePath);
            _iniData["RT-Thread"]["Password"] = password;
            _parser.WriteFile(FilePath, _iniData);
        }

        #endregion Password

        #endregion RT-Thread
    }
}