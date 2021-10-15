using MstnAPP.Services.Driver.ICanBus;

namespace MstnAPP.Services.Driver.CanBus.Models
{
    public class ModelCan
    {
        public string Name { get; set; }
        public ICan Driver { get; set; }

        public ModelCan(string name, ICan driver)
        {
            Name = name;
            Driver = driver;
        }
    }
}