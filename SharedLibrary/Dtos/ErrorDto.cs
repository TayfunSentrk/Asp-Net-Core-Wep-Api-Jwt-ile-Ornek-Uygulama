using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; } //hataların list şekilde tutulması //set yaparak sadece constructorde set edilmesi sağladım
        public bool IsShow { get; private set; } //hataların kullancıya gösterme durumu


        public ErrorDto()
        {
            Errors = new List<string>();  
        }

        public ErrorDto(string error,bool isShow)
        {
            Errors.Add(error); // gelen hatanın eklenmesi
            IsShow = isShow;
        }

        public ErrorDto(List<string> error, bool isShow)
        {
            Errors=error; // birden fazla hata gelme durumunda setlenme işlemi
            IsShow = isShow;
        }
    }
}
