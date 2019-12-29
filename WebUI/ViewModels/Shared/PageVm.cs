namespace UserManagement.WebUI.ViewModels
{
    public class PageVm
    {
        public readonly string LinkText;

        public readonly object UrlParameters;

        public readonly bool IsClickable;

        public PageVm(string linkText, object urlParameters, bool isClickable)
        {
            LinkText = linkText;
            UrlParameters = urlParameters;
            IsClickable = isClickable;
        }
    }
}