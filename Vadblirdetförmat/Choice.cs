using System;
using System.Collections.Generic;
using System.Text;

namespace Vadblirdetförmat
{
    class Choice
    {
        public DateTime DinnerDate { get; set; }
        public int TimeSlot { get; set; }
        public Places Place { get; set; }
        public Serv Servis { get; set; }
        public Protein Proteinsource { get; set; }
        public string Menues { get; set; }
    }
}
