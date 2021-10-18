namespace MstnAPP.Services.Driver.DriverDll.Kvaser.Models
{
    public class ModelCanWriteFrame
    {
        public int Id { get; set; }
        public byte[] Message { get; set; }
        public int Dlc { get; set; }
        public int Flag { get; set; }

        public ModelCanWriteFrame(int id, byte[] message, int dlc, int flag)
        {
            Id = id;
            Message = message;
            Dlc = dlc;
            Flag = flag;
        }
    }
}