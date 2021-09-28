using System.Collections.Generic;
using System.IO.Ports;

namespace MstnAPP.Services.Driver
{
    public delegate void EPortNameChanged(List<string> portNames);

    public delegate void EConnectChanged(bool isConnect);

    public delegate void EDataReceived(string data);

    public interface ISerial
    {
        event EPortNameChanged PortNamesChanged;

        event EConnectChanged ConnectChanged;

        event EDataReceived DataReceived;

        bool SetPortName(string port);

        bool SetPortName(uint port);

        void SetBaudRate(int baud);

        void SetBaudRate(string baud);

        void SetParity(Parity parity);

        bool SetParity(string parity);

        bool SetDataBits(int bits);

        bool SetDataBits(string bits);

        void SetStopBits(StopBits bits);

        bool SetStopBits(string bits);

        bool SetStopBits(float bits);

        void SetHandshake(Handshake hand);

        bool SetHandshake(string hand);

        void Open();

        void FlushPorts();

        List<string> GetPortNames();

        void Close();

        bool Connected();

        void SetDtr(bool dtr);

        void SetRts(bool rts);

        bool GetCd();

        bool GetCts();

        bool GetDsr();
    }
}