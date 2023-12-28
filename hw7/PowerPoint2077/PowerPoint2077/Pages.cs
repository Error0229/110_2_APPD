using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
            set
            {
                _pages[index] = value;
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
        public string Encode()
        {
            string encodedPages = "";
            foreach (Page page in _pages)
            {
                encodedPages += page.Encode();
                encodedPages += "\n";
            }
            return encodedPages;
        }
        private List<Page> _pages;
    }
}
