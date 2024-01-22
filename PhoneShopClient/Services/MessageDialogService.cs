using PhoneShopClient.Pages.OtherPages;

namespace PhoneShopClient.Services
{
    public class MessageDialogService
    {
        public MessageDialog? messageDialog { get; set; }
        public bool ShowBusyButton { get; set; }
        public bool ShowSaveButton { get; set; } = true;
        public Action? Action { get; set; }

        public async void SetMessageDialog()
        {
            await messageDialog!.ShowMessage();
            ShowBusyButton = false;
            ShowSaveButton = true;
            Action?.Invoke();
        }
    }
}
