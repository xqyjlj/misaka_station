using IniParser;
using IniParser.Model;
using System;
using System.Globalization;
using System.IO;

namespace MstnAPP.Services.Sys.DataFlie
{
    public class IniFile : IIniFile
    {
        private const string _filePath = "Misaka-Station.ini";
        private readonly FileIniDataParser _parser = new();
        private IniData _iniData;

        public IniFile()
        {
            if (!File.Exists(_filePath))
            {
                InitIniFile();
            }
            _iniData = _parser.ReadFile(_filePath);
        }

        private void InitIniFile()
        {
            IniData data = new();

            _ = data.Sections.AddSection("MianWindow");
            _ = data["MianWindow"].AddKey("FunctionListIndex", "0");

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

            _parser.WriteFile(_filePath, data);
        }

        #region MianWindow

        #region MianWindowFunctionListIndex

        public int GetMianWindowFunctionListIndex()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["MianWindow"]["FunctionListIndex"], out int ret);
            return ret;
        }

        public void SetMianWindowFunctionListIndex(int index)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["MianWindow"]["FunctionListIndex"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion MianWindowFunctionListIndex

        #endregion MianWindow

        #region RT-Thread

        #region Port

        public string GetRTThreadPort()
        {
            _iniData = _parser.ReadFile(_filePath);
            return _iniData["RT-Thread"]["Port"];
        }

        public void SetRTThreadPort(string port)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["Port"] = port;
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion Port

        #region BaudRate

        public int GetRTThreadBaudRate()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["BaudRate"], out int ret);
            return ret;
        }

        public void SetRTThreadBaudRate(int baudRate)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["BaudRate"] = baudRate.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion BaudRate

        #region Parity

        public int GetRTThreadParity()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["Parity"], out int ret);
            return ret;
        }

        public void SetRTThreadParity(int parity)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["Parity"] = parity.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion Parity

        #region DataBits

        public int GetRTThreadDataBits()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["DataBits"], out int ret);
            return ret;
        }

        public void SetRTThreadDataBits(int dataBits)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["DataBits"] = dataBits.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion DataBits

        #region StopBits

        public int GetRTThreadStopBits()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["StopBits"], out int ret);
            return ret;
        }

        public void SetRTThreadStopBits(int stopBits)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["StopBits"] = stopBits.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion StopBits

        #region Handshake

        public int GetRTThreadHandshake()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["Handshake"], out int ret);
            return ret;
        }

        public void SetRTThreadHandshake(int index)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["Handshake"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion Handshake

        #region IsSaveData

        public bool GetRTThreadIsSaveData()
        {
            _iniData = _parser.ReadFile(_filePath);
            return Convert.ToBoolean(_iniData["RT-Thread"]["IsSaveData"], new CultureInfo("zh-CN", false));
        }

        public void SetRTThreadIsSaveData(bool isSaveData)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["IsSaveData"] = isSaveData.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion IsSaveData

        #region SaveDataPath

        public string GetRTThreadSaveDataPath()
        {
            _iniData = _parser.ReadFile(_filePath);
            return _iniData["RT-Thread"]["SaveDataPath"];
        }

        public void SetRTThreadSaveDataPath(string saveDataPath)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["SaveDataPath"] = saveDataPath;
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion SaveDataPath

        #region FlushTime

        public int GetRTThreadFlushTime()
        {
            _iniData = _parser.ReadFile(_filePath);
            _ = int.TryParse(_iniData["RT-Thread"]["FlushTime"], out int ret);
            return ret;
        }

        public void SetRTThreadFlushTime(int index)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["FlushTime"] = index.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion FlushTime

        #region IsExistPassword

        public bool GetRTThreadIsExistPassword()
        {
            _iniData = _parser.ReadFile(_filePath);
            return Convert.ToBoolean(_iniData["RT-Thread"]["IsExistPassword"], new CultureInfo("zh-CN", false));
        }

        public void SetRTThreadIsExistPassword(bool isExistPassword)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["IsExistPassword"] = isExistPassword.ToString(new CultureInfo("zh-CN", false));
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion IsExistPassword

        #region Password

        public string GetRTThreadPassword()
        {
            _iniData = _parser.ReadFile(_filePath);
            return _iniData["RT-Thread"]["Password"];
        }

        public void SetRTThreadPassword(string password)
        {
            _iniData = _parser.ReadFile(_filePath);
            _iniData["RT-Thread"]["Password"] = password;
            _parser.WriteFile(_filePath, _iniData);
        }

        #endregion Password

        #endregion RT-Thread
    }
}