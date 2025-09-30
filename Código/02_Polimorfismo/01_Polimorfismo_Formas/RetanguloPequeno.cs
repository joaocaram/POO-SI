using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras
{
    public class RetanguloPequeno : Retangulo
    {
        public RetanguloPequeno() : base(3, 4, 1, 1) { }

        public override double Area()
        {
            return 7;
        }

        public override string ToString()
        {
            return "Retanglinho de área 12";
        }
    }
}
