using API.NETcore2.Models;

namespace API.NETcore2.Controllers
{
    public class ResultViewModel
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}