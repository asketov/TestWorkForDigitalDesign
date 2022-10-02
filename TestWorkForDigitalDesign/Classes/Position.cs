using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWorkForDigitalDesign.Classes
{
    public class Position
    {
        public int i;
        public int j;
        public override int GetHashCode()
        {
            string value = String.Concat((i+1).ToString(), (j+1).ToString());
            return Int32.Parse(value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Position pos) return this.GetHashCode() == pos.GetHashCode();
            return false;
        }
    }
}
