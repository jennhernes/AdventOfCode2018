using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class Instruction
    {
        public string opcode;
        public int A;
        public int B;
        public int C;

        public Instruction(string opcode, int A, int B, int C)
        {
            this.opcode = opcode;
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Registers addr(Registers registers)
        {
            int sum = registers[this.A] + registers[this.B];
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = sum;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers addi(Registers registers)
        {
            int sum = registers[this.A] + this.B;
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = sum;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers mulr(Registers registers)
        {
            int product = registers[this.A] * registers[this.B];
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = product;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers muli(Registers registers)
        {
            int product = registers[this.A] * this.B;
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = product;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers banr(Registers registers)
        {
            int bitAnd = registers[this.A] & registers[this.B];
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = bitAnd;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers bani(Registers registers)
        {
            int bitAnd = registers[this.A] & this.B;
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = bitAnd;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers borr(Registers registers)
        {
            int bitOr = registers[this.A] | registers[this.B];
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = bitOr;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers bori(Registers registers)
        {
            int bitOr = registers[this.A] | this.B;
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = bitOr;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers setr(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = registers[this.A];
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers seti(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            result[this.C] = this.A;
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers gtir(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (this.A > registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers gtri(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (registers[this.A] > this.B)
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers gtrr(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (registers[this.A] > registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers eqir(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (this.A == registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers eqri(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (registers[this.A] == this.B)
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }

        public Registers eqrr(Registers registers)
        {
            Registers result = new Registers(registers.ipIndex, registers[0], registers[1],
                registers[2], registers[3], registers[4], registers[5]);
            if (registers[this.A] == registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            result[registers.ipIndex]++;
            return (result);
        }
    }


    class Registers
    {
        public int[] regs;
        public int ipIndex;

        public Registers(int ipIndex, int reg0 = 0, int reg1 = 0, int reg2 = 0,
            int reg3 = 0, int reg4 = 0, int reg5 = 0)
        {
            this.ipIndex = ipIndex;
            this.regs = new int[6] { reg0, reg1, reg2, reg3, reg4, reg5 };
        }

        public int this[int index]
        {
            get
            {
                return regs[index];
            }

            set
            {
                regs[index] = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var file = new System.IO.StreamReader("..\\..\\input.txt");
            //string line;
            //Registers registers;
            //List<Instruction> instructions = new List<Instruction>();
            //Instruction currentInstruct;

            //line = file.ReadLine();
            //int ipIndex = int.Parse(line[4].ToString());
            //registers = new Registers(ipIndex, 1);

            //while ((line = file.ReadLine()) != null || registers[registers.ipIndex] < instructions.Count)
            //{
            //    if (line != null)
            //    {
            //        string[] split = line.Split();

            //        instructions.Add(new Instruction(split[0],
            //            int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3])));
            //        if (registers[registers.ipIndex] >= instructions.Count)
            //        {
            //            continue;
            //        }
            //    }

            //    currentInstruct = instructions[registers[registers.ipIndex]];

            //    if (currentInstruct.opcode == "addr")
            //    {
            //        registers = currentInstruct.addr(registers);
            //    }
            //    else if (currentInstruct.opcode == "addi")
            //    {
            //        registers = currentInstruct.addi(registers);
            //    }
            //    else if (currentInstruct.opcode == "mulr")
            //    {
            //        registers = currentInstruct.mulr(registers);
            //    }
            //    else if (currentInstruct.opcode == "muli")
            //    {
            //        registers = currentInstruct.muli(registers);
            //    }
            //    else if (currentInstruct.opcode == "banr")
            //    {
            //        registers = currentInstruct.banr(registers);
            //    }
            //    else if (currentInstruct.opcode == "bani")
            //    {
            //        registers = currentInstruct.bani(registers);
            //    }
            //    else if (currentInstruct.opcode == "borr")
            //    {
            //        registers = currentInstruct.borr(registers);
            //    }
            //    else if (currentInstruct.opcode == "bori")
            //    {
            //        registers = currentInstruct.bori(registers);
            //    }
            //    else if (currentInstruct.opcode == "setr")
            //    {
            //        registers = currentInstruct.setr(registers);
            //    }
            //    else if (currentInstruct.opcode == "seti")
            //    {
            //        registers = currentInstruct.seti(registers);
            //    }
            //    else if (currentInstruct.opcode == "gtir")
            //    {
            //        registers = currentInstruct.gtir(registers);
            //    }
            //    else if (currentInstruct.opcode == "gtri")
            //    {
            //        registers = currentInstruct.gtri(registers);
            //    }
            //    else if (currentInstruct.opcode == "gtrr")
            //    {
            //        registers = currentInstruct.gtrr(registers);
            //    }
            //    else if (currentInstruct.opcode == "eqir")
            //    {
            //        registers = currentInstruct.eqir(registers);
            //    }
            //    else if (currentInstruct.opcode == "eqri")
            //    {
            //        registers = currentInstruct.eqri(registers);
            //    }
            //    else if (currentInstruct.opcode == "eqrr")
            //    {
            //        registers = currentInstruct.eqrr(registers);
            //    }
            //}

            //System.Diagnostics.Debug.WriteLine(registers[0] + " " + registers[1] + " " +
            //    registers[2] + " " + registers[3] + " " + registers[4] + " " + registers[5]);

            int reg0 = 0;
            int reg1 = 0;
            int reg2 = 0;
            int reg3 = 0;
            int reg4 = 0;
            int reg5 = 0;

            reg1 = 1;
            reg3 = (1 * 22) + 3;
            reg4 = ((2 * 2) * 209) + reg3;
            reg3 = ((27 * 28) + 29) * 30 * 14 * 32;
            reg4 += reg3;

            reg5 = 1;
            while (!(reg5 > reg4))
            {
                if (reg4 % reg5 == 0)
                {
                    reg0 += reg5;
                }
                reg5++;
            }

            System.Diagnostics.Debug.WriteLine(reg0 + " " + reg1 + " " +
                reg2 + " " + reg3 + " " + reg4 + " " + reg5);
        }
    }
}
