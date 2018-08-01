using System.ComponentModel.DataAnnotations;
using System.Web;

namespace JsonSlicer.ViewModels.Home
{
    public class IndexViewModel
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase InputFile { get; set; }
    }
}