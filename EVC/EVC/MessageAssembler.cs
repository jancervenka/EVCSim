using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVC
{

    class MessageAssembler
    {

        private static Dictionary<string, byte> TrackCondDict;

        public MessageAssembler()
        {
            TrackCondDict = new Dictionary<string, byte>();
            TrackCondDict.Add("PL17", 0x00);
            TrackCondDict.Add("PL19", 0x01);
            TrackCondDict.Add("PL18", 0x02);
            TrackCondDict.Add("PL20", 0x03);
            TrackCondDict.Add("PL01", 0x04);
            TrackCondDict.Add("PL02", 0x05);
            TrackCondDict.Add("PL03", 0x06);
            TrackCondDict.Add("PL04", 0x07);
            TrackCondDict.Add("PL05", 0x08);
            TrackCondDict.Add("PL06", 0x09);
            TrackCondDict.Add("PL07", 0x0A);
            TrackCondDict.Add("PL08", 0x0B);
            TrackCondDict.Add("PL09", 0x0C);
            TrackCondDict.Add("PL10", 0x0D);
            TrackCondDict.Add("PL11", 0x0E);
            TrackCondDict.Add("PL12", 0x0F);
            TrackCondDict.Add("PL13", 0x10);
            TrackCondDict.Add("PL14", 0x11);
            TrackCondDict.Add("PL15", 0x12);
            TrackCondDict.Add("PL16", 0x13);
            TrackCondDict.Add("PL24", 0x14);
            TrackCondDict.Add("PL25", 0x15);
            TrackCondDict.Add("PL26", 0x16);
            TrackCondDict.Add("PL27", 0x17);
            TrackCondDict.Add("PL28", 0x18);
            TrackCondDict.Add("PL29", 0x19);
            TrackCondDict.Add("PL30", 0x1A);
            TrackCondDict.Add("PL31", 0x1B);
            TrackCondDict.Add("PL32", 0x1C);
            TrackCondDict.Add("PL33", 0x1D);
        }


        private static MessageContainer AssembleSMMessage(string[] Words)
        {
            List<byte> Payload = new List<byte>();
            List<byte> Message = new List<byte>();

            for (int i = 0; i < Words.Length - 1; i += 2)
            {
                switch (Words[i])
                {
                    case "Vtrain":
                        Payload.Add(0x01);
                        Payload.AddRange(BitConverter.GetBytes((ushort)4));
                        Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        break;

                    case "Vperm":
                        Payload.Add(0x02);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)4));
                            Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        }

                        break;

                    case "Vtarget":
                        Payload.Add(0x03);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)4));
                            Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        }

                        break;

                    case "VSBI":
                        Payload.Add(0x04);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)4));
                            Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        }

                        break;

                    case "Vrel":
                        Payload.Add(0x05);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)4));
                            Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        }

                        break;

                    case "Supervision1":
                        Payload.Add(0x07);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)3));
                            Payload.AddRange(Encoding.ASCII.GetBytes(Words[i + 1]));
                        }

                        break;

                    case "Supervision2":
                        Payload.Add(0x08);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            byte[] Value = Encoding.ASCII.GetBytes(Words[i + 1]);

                            Payload.AddRange(BitConverter.GetBytes((ushort)Value.Length));
                            Payload.AddRange(Value);
                        }

                        break;

                    case "Mode":
                        Payload.Add(0x09);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)2));
                            Payload.AddRange(Encoding.ASCII.GetBytes(Words[i + 1]));
                        }

                        break;

                    case "Dtarget":
                        Payload.Add(0x0A);

                        if (Words[i + 1] == "null")
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)0));
                        }
                        else
                        {
                            Payload.AddRange(BitConverter.GetBytes((ushort)4));
                            Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                        }

                        break;

                    default:
                        break;
                }
            }

            Message.Add(0x01);
            Message.AddRange(BitConverter.GetBytes((ushort)Payload.Count));
            Message.AddRange(Payload);

            return new MessageContainer(Message);
        }

        private static MessageContainer AssemblePAMessage(string[] Words)
        {
            List<byte> Payload = new List<byte>();
            List<byte> Message = new List<byte>();

            Message.Add(0x00);

            switch (Words[0])
            {
                case "GradientProfile":
                    Message.Add(0x18);
                    for (int i = 1; i < Words.Length - 1; i += 2)
                    {
                        Payload.AddRange(BitConverter.GetBytes(int.Parse(Words[i])));
                        Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                    }

                    break;

                case "SpeedProfile":
                    Message.Add(0x19);
                    for (int i = 1; i < Words.Length - 1; i += 2)
                    {
                        Payload.AddRange(BitConverter.GetBytes(int.Parse(Words[i])));
                        Payload.AddRange(BitConverter.GetBytes(float.Parse(Words[i + 1])));
                    }

                    break;

                case "TrackCondProfile":
                    Message.Add(0x1A);
                    for (int i = 1; i < Words.Length - 1; i += 2)
                    {
                        Payload.AddRange(BitConverter.GetBytes(int.Parse(Words[i])));
                        Payload.Add(TrackCondDict[Words[i + 1]]);
                    }

                    break;


                default:
                    break;
            }

            
            Message.AddRange(BitConverter.GetBytes((ushort)Payload.Count));
            Message.AddRange(Payload);

            return new MessageContainer(Message);
        }

        public static List<MessageContainer> Assemble(string[] Data, bool flag)
        {
            List<MessageContainer> DataAssembled = new List<MessageContainer>();

            foreach (string Line in Data)
            {
                if (flag)
                    DataAssembled.Add(AssembleSMMessage(Line.Split(' ')));
                else
                    DataAssembled.Add(AssemblePAMessage(Line.Split(' ')));
            }

            return DataAssembled;
        }
    }
}
