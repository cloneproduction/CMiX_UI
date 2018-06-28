namespace CMiX.Services
{
    public interface IMessengerData
    {
        string MessageAddress { get; set; }

        bool MessageEnabled { get; set; }
    }
}
