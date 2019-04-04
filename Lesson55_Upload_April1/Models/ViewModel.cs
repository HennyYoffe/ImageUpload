using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;

namespace Lesson55_Upload_April1.Models
{
    public class ImageViewModel
    {
        public Image Image { get; set; }
        public string Password { get; set; }
        public bool Session { get; set; }
        public bool First { get; set; }

    }
}