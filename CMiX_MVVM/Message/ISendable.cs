namespace CMiX.MVVM.Services
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        Messenger Messenger { get; set; }
    }
}