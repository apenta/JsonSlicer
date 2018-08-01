using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using JsonSlicer.Models;

namespace JsonSlicer.ViewModels.Home
{
    public class IndexViewModel
    {
        [DisplayName("Choose the file to upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase InputFile { get; set; }

        [DisplayName("Paste the file's contents.")]
        public string Contents { get; set; }

        public InputFile File { get; set; }
    }
}