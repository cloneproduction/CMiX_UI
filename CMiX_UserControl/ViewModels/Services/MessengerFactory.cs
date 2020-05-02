namespace CMiX.Studio.ViewModels
{
    public static class MessengerFactory
    {
        static int ID = 0;

        public static Messenger CreateMessenger()
        {
            var messenger = new Messenger(ID);
            ID++;
            return messenger;
        }
    }
}