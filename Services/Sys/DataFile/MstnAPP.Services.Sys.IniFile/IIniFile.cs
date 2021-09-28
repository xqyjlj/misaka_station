namespace MstnAPP.Services.Sys.DataFlie
{
    public interface IIniFile
    {
        int GetMianWindowFunctionListIndex();

        void SetMianWindowFunctionListIndex(int index);

        string GetRTThreadPort();

        void SetRTThreadPort(string port);

        int GetRTThreadBaudRate();

        void SetRTThreadBaudRate(int baudRate);

        int GetRTThreadParity();

        void SetRTThreadParity(int parity);

        int GetRTThreadDataBits();

        void SetRTThreadDataBits(int dataBits);

        int GetRTThreadStopBits();

        void SetRTThreadStopBits(int stopBits);

        int GetRTThreadHandshake();

        void SetRTThreadHandshake(int index);

        bool GetRTThreadIsSaveData();

        void SetRTThreadIsSaveData(bool isSaveData);

        string GetRTThreadSaveDataPath();

        void SetRTThreadSaveDataPath(string saveDataPath);

        int GetRTThreadFlushTime();

        void SetRTThreadFlushTime(int index);

        bool GetRTThreadIsExistPassword();

        void SetRTThreadIsExistPassword(bool isExistPassword);

        string GetRTThreadPassword();

        void SetRTThreadPassword(string password);
    }
}