using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            UInt64 reg0 = 4682012;
            UInt64 reg1 = 0;
            UInt64 reg2 = 0;
            UInt64 reg3 = 0;
            UInt64 reg4 = 0;
            UInt64 reg5 = 0;
            UInt64 leastInstructions = 10000;
            UInt64 maxReg = 0;
            int numRepeat = 0;
            UInt64 smallestReg0 = reg0;
            List<UInt64> reg1Values = new List<UInt64>();

        // labels needed: 1, 6, 8, 18, 24, 26, 28
        IP0:
            reg2 = 0;
            reg3 = 0;
            reg4 = 0;
            reg5 = 0;
            reg1 = 123;
        IP1:
            reg1 = reg1 & 456;
            if (reg1 == 72)
            {
                reg1 = 0;
                goto IP6;
            }
            else
            {
                reg1 = 0;
                goto IP1;
            }


        IP6:
            reg2 = reg1 | 65536; // sets reg2 to reg1 and bit 17 == 1
            reg1 = 8725355;

        IP8:
            reg5 = reg2 & 255; // set reg5 to the lower 8 bits of reg2, 0 the first time through
            reg1 = (((reg1 + reg5) & 16777215) * 65899) & 16777215; // += reg2 & 255
            // shorten reg1 to the lower 24 bits
            // shorten the product to the lower 24 bits
            if (reg2 < 256) // false the first time through
            {
                reg5 = 1;
                if (reg1Values.Contains(reg1))
                {
                    numRepeat = 0;
                    foreach (UInt64 n in reg1Values)
                    {
                        if (n == reg1)
                        {
                            numRepeat++;
                        }
                        maxReg = Math.Max(maxReg, n);
                    }
                    if (numRepeat == 2)
                    {
                        goto IPEnd;
                    }
                    reg0 = reg1;

                }
                reg1Values.Add(reg1);
                reg5 = 0;
                goto IP6;
            }
            else
            {
                reg5 = 0;

            IP18:
                reg3 = reg5 + 1; // 1 the first time through
                reg3 *= 256;
                if (reg3 > reg2)
                {
                    reg3 = 1;
                    reg2 = reg5;
                    goto IP8;
                }
                else
                {
                    reg3 = 0;
                    reg5++;
                    goto IP18;
                }
            }

            IPEnd:

            System.Diagnostics.Debug.WriteLine(reg0);
        }
    }
}
