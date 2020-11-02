using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ET;

namespace ETTools
{
    public  class Proto2CS
    {
        private static string protoPath = "";
        private static string clientMessagePath = "Hotfix/Logic/Message/";
        private const string messageOpcodeFile = "./MessageOpcode.proto";
        private static readonly char[] splitChars = { ' ', '\t' };
        private static readonly List<OpcodeInfo> msgOpcode = new List<OpcodeInfo>();
        private static readonly StringBuilder msgStrings = new StringBuilder();
        private static Dictionary<string, string> allMessageOpcode = new Dictionary<string, string>();
        public static void Run(string folderPath,string messagePath,string Opcode)
        {
            protoPath = folderPath;
            clientMessagePath = "../Unity/Assets/"+messagePath;
            string protoc = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                protoc = "protoc.exe";
            }
            else
            {
                protoc = "protoc";
            }
            allMessageOpcode.Clear();
            allMessageOpcode = GetAllOpcodeByFile();
            DirectoryInfo theProtoFolder = new DirectoryInfo(@"../Proto/");
            DirectoryInfo theFolder = new DirectoryInfo(@"../Proto/"+ protoPath + "/");
            protoc = Path.Combine(theFolder.FullName + "../", protoc);
            FileInfo[] fileInfo = theFolder.GetFiles();
            msgOpcode.Clear();
            msgStrings.Clear();
            StringBuilder sb = msgStrings;
            sb.Append("using ET;\n");
            sb.Append($"namespace ET\n");
            sb.Append("{\n");
            string path = theProtoFolder.FullName;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件
            {
                if (NextFile.Extension.Equals(".proto"))
                {
                    Process process = ProcessHelper.Run(protoc, "--csharp_out=\""+ clientMessagePath + "\" "
                        + "--proto_path=\"./"+ protoPath + "\" "
                        + "--proto_path=" + path + " "
                        + NextFile.Name, waitExit: true);
                    ProtoToCS("ET", NextFile.Name, "LogicOpcode");
                }
            }
            sb.Append("}\n");
            GenerateOpcode("ET", Opcode, clientMessagePath, msgStrings);
        }
        public static void ProtoToCS(string ns, string protoName, string opcodeClassName)
        {

            string proto = Path.Combine(protoPath, protoName);

            string s = File.ReadAllText(proto);

            StringBuilder sb = msgStrings;

            bool isMsgStart = false;

            foreach (string line in s.Split('\n'))
            {
                string newline = line.Trim();

                if (newline == "")
                {
                    continue;
                }

                if (newline.StartsWith("//"))
                {
                    sb.Append($"{newline}\n");
                }

                if (newline.StartsWith("message"))
                {
                    string parentClass = "";
                    isMsgStart = true;
                    string msgName = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)[1];
                    string[] ss = newline.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

                    if (ss.Length >= 2)
                    {
                        parentClass = ss[1].Trim();
                    }
                    if (allMessageOpcode.ContainsKey(msgName))
                    {
                        int startOpcode = int.Parse(allMessageOpcode[msgName].Trim());
                        msgOpcode.Add(new OpcodeInfo() { Name = msgName, Opcode = startOpcode });
                        sb.Append($"\t[Message({opcodeClassName}.{msgName})]\n");
                    }


                    sb.Append($"\tpublic partial class {msgName} ");
                    if (parentClass != "")
                    {
                        sb.Append($": {parentClass} ");
                    }

                    sb.Append("{}\n\n");
                }

                if (isMsgStart && newline == "}")
                {
                    isMsgStart = false;
                }
            }



            //GenerateOpcode(ns, opcodeClassName, outputPath, sb);
        }

        private static void GenerateOpcode(string ns, string outputFileName, string outputPath, StringBuilder sb)
        {
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic static partial class {outputFileName}");
            sb.AppendLine("\t{");
            foreach (OpcodeInfo info in msgOpcode)
            {
                sb.AppendLine($"\t\t public const ushort {info.Name} = {info.Opcode};");
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            string csPath = Path.Combine(outputPath, outputFileName + ".cs");
            File.WriteAllText(csPath, sb.ToString());
        }
        //获取所有定义的协议
        private static Dictionary<string, string> GetAllOpcodeByFile()
        {
            string opcodeDefineFile = messageOpcodeFile;
            string opcodeDefineStr = File.ReadAllText(opcodeDefineFile);
            string enumstr = "enum MessageOpcode";
            int index = opcodeDefineStr.IndexOf(enumstr);
            if (index == -1)
            {
                return null;
            }
            opcodeDefineStr = opcodeDefineStr.Substring(index + enumstr.Length);
            opcodeDefineStr = opcodeDefineStr.Replace('{', ' ');
            opcodeDefineStr = opcodeDefineStr.Replace('}', ' ');
            // opcodeDefineStr = opcodeDefineStr.Replace('\n', ' ');
            // opcodeDefineStr = opcodeDefineStr.Replace('\r', ' ');
            opcodeDefineStr = opcodeDefineStr.Replace('\t', ' ');
            opcodeDefineStr = opcodeDefineStr.Replace(" ", "");
            Dictionary<string, string> opcodedic = new Dictionary<string, string>();
            string[] _Split = new string[] { ";", "\r\n", };
            string[] opcodeDefineStrs = opcodeDefineStr.Split(_Split, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in opcodeDefineStrs)
            {
                int index2 = line.IndexOf("//");
                if (index2 == 0) continue;
                if (index2 > 0)
                {
                    line.Substring(0, index2);
                }
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] opcodestr = line.Split('=');
                if (opcodestr.Length != 2)
                    continue;
                string key = opcodestr[0];
                key = key.Substring(1);
                string value = opcodestr[1];
                if(opcodedic.ContainsKey(key))
                {
                    Console.WriteLine("have same opcode key! " + key + " value = " + value); 
                }
                else if (opcodedic.ContainsValue(value))
                {
                    Console.WriteLine("have same opcode value! " + value + " key = " + key);
                }
                else
                {
                    opcodedic.Add(key, value);
                }
            }
            return opcodedic;
        }
    }
}
