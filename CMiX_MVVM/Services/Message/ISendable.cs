namespace CMiX.MVVM.Services
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        Sender Sender { get; set; }
    }
}