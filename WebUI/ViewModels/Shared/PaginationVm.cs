using System;
using System.Collections.Generic;

namespace UserManagement.WebUI.ViewModels
{
    public class PaginationVm
    {
        public List<PageVm> Pages { get; private set; }

        public readonly int Take; // elements

        public readonly int Skip; // elements

        public PaginationVm(int elements, int perPage, int selectedPage, params string[] values)
        {
            Pages = BuildPages(elements, perPage, selectedPage, values);

            Skip = (selectedPage - 1) * perPage;

            Take = perPage * selectedPage <= elements
                ? perPage
                : elements % perPage;
        }

        private List<PageVm> BuildPages(int elements, int perPage, int selected, params string[] values)
        {
            int total = (elements / perPage) + 1;

            var pages = new List<PageVm>();

            if (total == 1)
                return pages;

            Dictionary<string, string> parameters = BuildParameters(values);

            for(var page=1; page<=total; page++)
            {
                string linkText = page.ToString();

                var pageParams = new Dictionary<string, string>(parameters);
                pageParams.Add("page", page.ToString());

                bool isClickable = page != selected;

                pages.Add(new PageVm(linkText, pageParams, isClickable));
            }

            return pages;
        }

        private Dictionary<string, string> BuildParameters(params string[] values)
        {
            var parameters = new Dictionary<string, string>();

            if (values != null)
            {
                if (values.Length % 2 != 0)
                    throw new ArgumentException(nameof(values) + " must be a multiple of 2"
                        + ", because each element is a parameter's key, then its value");

                for(int i=0; i<values.Length; i+=2)
                    parameters.Add(values[0], values[1]);
            }

            return parameters;
        }
    }
}