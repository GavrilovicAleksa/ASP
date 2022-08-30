using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Log
{
    public class GetLogDto
    {
        public int Id { get; set; }

        public string UseCaseName { get; set; }

        public string Date { get; set; } 
    }
}
