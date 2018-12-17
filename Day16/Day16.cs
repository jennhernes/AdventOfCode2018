using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Instruction
    {
        public int opcode;
        public int A;
        public int B;
        public int C;

        public Instruction(int opcode, int A, int B, int C)
        {
            this.opcode = opcode;
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Registers addr(Registers registers)
        {
            int sum = registers[this.A] + registers[this.B];
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = sum;
            return (result);
        }

        public Registers addi(Registers registers)
        {
            int sum = registers[this.A] + this.B;
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = sum;
            return (result);
        }

        public Registers mulr(Registers registers)
        {
            int product = registers[this.A] * registers[this.B];
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = product;
            return (result);
        }

        public Registers muli(Registers registers)
        {
            int product = registers[this.A] * this.B;
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = product;
            return (result);
        }

        public Registers banr(Registers registers)
        {
            int bitAnd = registers[this.A] & registers[this.B];
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = bitAnd;
            return (result);
        }

        public Registers bani(Registers registers)
        {
            int bitAnd = registers[this.A] & this.B;
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = bitAnd;
            return (result);
        }

        public Registers borr(Registers registers)
        {
            int bitOr = registers[this.A] | registers[this.B];
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = bitOr;
            return (result);
        }

        public Registers bori(Registers registers)
        {
            int bitOr = registers[this.A] | this.B;
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = bitOr;
            return (result);
        }

        public Registers setr(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = registers[this.A];
            return (result);
        }

        public Registers seti(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            result[this.C] = this.A;
            return (result);
        }

        public Registers gtir(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (this.A > registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }

        public Registers gtri(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (registers[this.A] > this.B)
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }

        public Registers gtrr(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (registers[this.A] > registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }

        public Registers eqir(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (this.A == registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }

        public Registers eqri(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (registers[this.A] == this.B)
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }

        public Registers eqrr(Registers registers)
        {
            Registers result = new Registers(registers[0], registers[1], registers[2], registers[3]);
            if (registers[this.A] == registers[this.B])
            {
                result[this.C] = 1;
            }
            else
            {
                result[this.C] = 0;
            }
            return (result);
        }
    }


    class Registers
    {
        public int[] regs;

        public Registers(int reg0 = 0, int reg1 = 0, int reg2 = 0, int reg3 = 0)
        {
            this.regs = new int[4] { reg0, reg1, reg2, reg3 };
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

    class Day16
    {
        static void Part1()
        {
            var file = new System.IO.StreamReader("..\\..\\input1.txt");
            string line;
            string[] delims = { ":", "[", ",", "]", " " };
            int numMatchingSamples = 0;
            int numMatchingOpcodes = 0;
            Registers before;
            Registers after;
            Registers result;
            Instruction instruction;

            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0 && split[0] == "Before")
                {
                    numMatchingOpcodes = 0;
                    before = new Registers(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]));
                    line = file.ReadLine();
                    split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                    instruction = new Instruction(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
                    line = file.ReadLine();
                    split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                    after = new Registers(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]));

                    result = instruction.addr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.addi(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.mulr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.muli(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.banr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.bani(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.borr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.bori(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.setr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.seti(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.gtir(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.gtri(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.gtrr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.eqir(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.eqri(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }

                    result = instruction.eqrr(before);
                    if (result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3])
                    {
                        numMatchingOpcodes++;
                        if (numMatchingOpcodes >= 3)
                        {
                            numMatchingSamples++;
                            continue;
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(numMatchingSamples);
        }

        static void Part2()
        {
            var file = new System.IO.StreamReader("..\\..\\input1.txt");
            string line;
            string[] delims = { ":", "[", ",", "]", " " };
            List<int> opcodes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            Dictionary<string, List<int>> possibleOpcode = new Dictionary<string, List<int>>
            { {"addr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 } }, {"addi", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 } },
                { "mulr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "muli", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "banr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "bani", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "borr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "bori", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "setr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "seti", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "gtir", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "gtri", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "gtrr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "eqir", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }},
                { "eqri", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }}, { "eqrr", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }} };
            Registers before;
            Registers after;
            Registers result;
            Instruction instruction;

            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0 && split[0] == "Before")
                {
                    before = new Registers(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]));
                    line = file.ReadLine();
                    split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                    instruction = new Instruction(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
                    line = file.ReadLine();
                    split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                    after = new Registers(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]));

                    if (possibleOpcode["addr"].Count > 1)
                    {
                        result = instruction.addr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["addr"].Remove(instruction.opcode);
                            if (possibleOpcode["addr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "addr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["addr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["addi"].Count > 1)
                    {
                        result = instruction.addi(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["addi"].Remove(instruction.opcode);
                            if (possibleOpcode["addi"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "addi")
                                    {
                                        kvp.Value.Remove(possibleOpcode["addi"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["mulr"].Count > 1)
                    {
                        result = instruction.mulr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["mulr"].Remove(instruction.opcode);
                            if (possibleOpcode["mulr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "mulr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["mulr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["muli"].Count > 1)
                    {
                        result = instruction.muli(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["muli"].Remove(instruction.opcode);
                            if (possibleOpcode["muli"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "muli")
                                    {
                                        kvp.Value.Remove(possibleOpcode["muli"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["banr"].Count > 1)
                    {
                        result = instruction.banr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["banr"].Remove(instruction.opcode);
                            if (possibleOpcode["banr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "banr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["banr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["bani"].Count > 1)
                    {
                        result = instruction.bani(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["bani"].Remove(instruction.opcode);
                            if (possibleOpcode["bani"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "bani")
                                    {
                                        kvp.Value.Remove(possibleOpcode["bani"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["borr"].Count > 1)
                    {
                        result = instruction.borr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["borr"].Remove(instruction.opcode);
                            if (possibleOpcode["borr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "borr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["borr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["bori"].Count > 1)
                    {
                        result = instruction.bori(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["bori"].Remove(instruction.opcode);
                            if (possibleOpcode["bori"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "bori")
                                    {
                                        kvp.Value.Remove(possibleOpcode["bori"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["setr"].Count > 1)
                    {
                        result = instruction.setr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["setr"].Remove(instruction.opcode);
                            if (possibleOpcode["setr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "setr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["setr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["seti"].Count > 1)
                    {
                        result = instruction.seti(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["seti"].Remove(instruction.opcode);
                            if (possibleOpcode["seti"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "seti")
                                    {
                                        kvp.Value.Remove(possibleOpcode["seti"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["gtir"].Count > 1)
                    {
                        result = instruction.gtir(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["gtir"].Remove(instruction.opcode);
                            if (possibleOpcode["gtir"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "gtir")
                                    {
                                        kvp.Value.Remove(possibleOpcode["gtir"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["gtri"].Count > 1)
                    {
                        result = instruction.gtri(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["gtri"].Remove(instruction.opcode);
                            if (possibleOpcode["gtri"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "gtri")
                                    {
                                        kvp.Value.Remove(possibleOpcode["gtri"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["gtrr"].Count > 1)
                    {
                        result = instruction.gtrr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["gtrr"].Remove(instruction.opcode);
                            if (possibleOpcode["gtrr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "gtrr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["gtrr"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["eqir"].Count > 1)
                    {
                        result = instruction.eqir(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["eqir"].Remove(instruction.opcode);
                            if (possibleOpcode["eqir"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "eqir")
                                    {
                                        kvp.Value.Remove(possibleOpcode["eqir"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["eqri"].Count > 1)
                    {
                        result = instruction.eqri(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["eqri"].Remove(instruction.opcode);
                            if (possibleOpcode["eqri"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "eqri")
                                    {
                                        kvp.Value.Remove(possibleOpcode["eqri"][0]);
                                    }
                                }
                            }
                        }
                    }

                    if (possibleOpcode["eqrr"].Count > 1)
                    {
                        result = instruction.eqrr(before);
                        if (!(result[0] == after[0] && result[1] == after[1] && result[2] == after[2] && result[3] == after[3]))
                        {
                            possibleOpcode["eqrr"].Remove(instruction.opcode);
                            if (possibleOpcode["eqrr"].Count == 1)
                            {
                                for (int i = 0; i < possibleOpcode.Count; i++)
                                {
                                    KeyValuePair<string, List<int>> kvp = possibleOpcode.ElementAt(i);
                                    if (kvp.Key != "eqrr")
                                    {
                                        kvp.Value.Remove(possibleOpcode["eqrr"][0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            HashSet<string> discovered = new HashSet<string>();
            while (discovered.Count < possibleOpcode.Count)
            {
                for (int i = 0; i < possibleOpcode.Count; i++)
                {
                    KeyValuePair<string, List<int>> kvpi = possibleOpcode.ElementAt(i);
                    if (!discovered.Contains(kvpi.Key) && kvpi.Value.Count == 1)
                    {
                        for (int j = 0; j < possibleOpcode.Count; j++)
                        {
                            KeyValuePair<string, List<int>> kvpj = possibleOpcode.ElementAt(j);
                            if (kvpj.Key != kvpi.Key)
                            {
                                kvpj.Value.Remove(kvpi.Value[0]);
                            }
                        }
                        discovered.Add(kvpi.Key);
                    }
                }
            }

            Registers registers = new Registers();
            var file2 = new System.IO.StreamReader("..\\..\\input2.txt");
            while ((line = file2.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                instruction = new Instruction(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
                if (possibleOpcode["addr"].Contains(instruction.opcode))
                {
                    registers = instruction.addr(registers);
                }
                else if (possibleOpcode["addi"].Contains(instruction.opcode))
                {
                    registers = instruction.addi(registers);
                }
                else if (possibleOpcode["mulr"].Contains(instruction.opcode))
                {
                    registers = instruction.mulr(registers);
                }
                else if (possibleOpcode["muli"].Contains(instruction.opcode))
                {
                    registers = instruction.muli(registers);
                }
                else if (possibleOpcode["banr"].Contains(instruction.opcode))
                {
                    registers = instruction.banr(registers);
                }
                else if (possibleOpcode["bani"].Contains(instruction.opcode))
                {
                    registers = instruction.bani(registers);
                }
                else if (possibleOpcode["borr"].Contains(instruction.opcode))
                {
                    registers = instruction.borr(registers);
                }
                else if (possibleOpcode["bori"].Contains(instruction.opcode))
                {
                    registers = instruction.bori(registers);
                }
                else if (possibleOpcode["setr"].Contains(instruction.opcode))
                {
                    registers = instruction.setr(registers);
                }
                else if (possibleOpcode["seti"].Contains(instruction.opcode))
                {
                    registers = instruction.seti(registers);
                }
                else if (possibleOpcode["gtir"].Contains(instruction.opcode))
                {
                    registers = instruction.gtir(registers);
                }
                else if (possibleOpcode["gtri"].Contains(instruction.opcode))
                {
                    registers = instruction.gtri(registers);
                }
                else if (possibleOpcode["gtrr"].Contains(instruction.opcode))
                {
                    registers = instruction.gtrr(registers);
                }
                else if (possibleOpcode["eqir"].Contains(instruction.opcode))
                {
                    registers = instruction.eqir(registers);
                }
                else if (possibleOpcode["eqri"].Contains(instruction.opcode))
                {
                    registers = instruction.eqri(registers);
                }
                else if (possibleOpcode["eqrr"].Contains(instruction.opcode))
                {
                    registers = instruction.eqrr(registers);
                }
            }

            System.Diagnostics.Debug.WriteLine(registers[0] + " " + registers[1] + " " + registers[2] + " " + registers[3]);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
