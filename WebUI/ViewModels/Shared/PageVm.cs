using System.Collections.Generic;

namespace UserManagement.WebUI.ViewModels
{
    public class PageVm
    {
        public readonly string LinkText;

        public readonly IDictionary<string, string> UrlParameters;

        public readonly bool IsClickable;

        public PageVm(string linkText, IDictionary<string, string> urlParameters, bool isClickable)
        {
            LinkText = linkText;
            UrlParameters = urlParameters;
            IsClickable = isClickable;
        }
    }
}