using MstnAPP.Services.Driver.ICanBus;
using System;
using System.Collections.Generic;
using System.IO;

namespace MstnAPP.Services.Driver.CanProtocol
{
    public class CanSlip
    {
        private readonly ICan _can;

        private const int END = 0xC0;
        private const int ESC = 0xDB;
        private const int ESC_END = 0xDC;
        private const int ESC_ESC = 0xDD;

        private int _id;
        private CanBusEnum _flag;

        public CanSlip(ICan can, int id, CanBusEnum flag)
        {
            _can = can;
            _id = id;
            _flag = flag;
        }

        public void SendFile(string path)
        {
            const int frameSize = 1024;
            if (!File.Exists(path)) return;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var binaryReader = new BinaryReader(fileStream);

            var fileSize = fileStream.Length;
            while (fileSize > 0)
            {
                // 读取可能会返回从0到frameSize的任何数
                var readSize = fileSize > frameSize ? frameSize : Convert.ToInt32(fileSize);

                var readBytes = binaryReader.ReadBytes(readSize);

                fileSize -= readSize;
                SendFileByte(readBytes, readSize);
            }
        }

        public void SendFile(string path, int id, CanBusEnum flag)
        {
            _id = id;
            _flag = flag;
            SendFile(path);
        }

        private void SendFileByte(IReadOnlyList<byte> bytes, int length)
        {
            var index = 0;
            var list = new List<byte>();
            while (index < length)
            {
                switch (bytes[index])
                {
                    /*如果需要转意，则进行相应的处理*/
                    case END:
                        list.Add(ESC);
                        list.Add(ESC_END);
                        break;

                    case ESC:
                        list.Add(ESC);
                        list.Add(ESC_ESC);
                        break;
                    /*如果不需要转意，则直接发送*/
                    default:
                        list.Add(bytes[index]);
                        break;
                }

                if (list.Count >= 8)
                {
                    var range = list.GetRange(0, 8);
                    list.RemoveRange(0, 8);
                    var message = range.ToArray();
                    _can.Write(message, _id, 8, _flag);
                }

                index++;
            }

            // ReSharper disable once InvertIf
            if (list.Count > 0)
            {
                var message = list.ToArray();
                list.Clear();
                _can.Write(message, _id, message.Length, _flag);
            }
        }
    }
}