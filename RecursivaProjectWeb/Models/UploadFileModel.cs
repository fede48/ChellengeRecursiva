using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaProjectWeb.Models
{
    public class UploadFileModel
    {
        public UploadFileModel()
        {
            Files = new List<HttpPostedFileBase>();
        }

        public List<HttpPostedFileBase> Files { get; set; }
    }
}