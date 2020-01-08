namespace CMiX.MVVM.Services
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        MessageService MessageService { get; set; }
    }
}