using System;
using System.Collections.Generic;

namespace UserManagement.WebUI.ViewModels
{
    public class PaginationVm
    {
        public List<PageVm> Pages = new List<PageVm>();

        public PaginationVm(int elements, int perPage, int selectedPage, params string[] values)
        {
            int pages = (elements / perPage) + 1;

            if (pages == 1)
                return;

            var parameters = new Dictionary<string, string>();
            if (values != null)
            for(int i=0; i<values.Length; i+=2)
            {
                parameters.Add(values[0], values[1]);
            }

            for(var page=1; page<=pages; page++)
            {
                string linkText = page.ToString();

                var pageParams = new Dictionary<string, string>(parameters);
                pageParams.Add("page", page.ToString());

                bool isClickable = page != selectedPage;

                Pages.Add(new PageVm(linkText, pageParams, isClickable));
            }
        }
    }
}