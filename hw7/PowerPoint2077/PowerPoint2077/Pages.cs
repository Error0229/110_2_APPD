using System.Collections.Generic;

namespace WindowPowerPoint
{
    public class Pages
    {
        // constructor 
        public Pages()
        {
            _pages = new List<Page>();
        }
        // insert page
        public void Insert(int index, Page page)
        {
            _pages.Insert(index, page);
        }

        // index property
        public Page this[int index]
        {
            get
            {
                return _pages[index];
            }
        }

        // remove shape
        public void Remove(Page page)
        {
            _pages.Remove(page);
        }

        // count
        public int Count
        {
            get
            {
                return _pages.Count;
            }
        }

        // add page
        public void Add(Page page)
        {
            _pages.Add(page);
        }

        // encode
        public virtual string Convert()
        {
            string data = string.Empty;
            foreach (Page page in _pages)
            {
                data += page.Convert();
                data += Constant.NEW_LINE;
            }
            return data;
        }
        private List<Page> _pages;
    }
}
