#region Header
/**
 * JsonWriter.cs
 *   Stream-like facility to output JSON text.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace LitJson
{
    internal enum Condition
    {
        InArray,
        InObject,
        NotAProperty,
        Property,
        Value
    }

    internal class WriterContext
    {
        public int  Count;
        public bool InArray;
        public bool InObject;
        public bool ExpectingValue;
        public int  Padding;
    }

    public class JsonWriter
    {
        #region Fields
        private static readonly NumberFormatInfo number_format;

        private WriterContext        context;
        private Stack<WriterContext> ctx_stack;
        private bool                 has_reached_end;
        private char[]               hex_seq;
        private int                  indentation;
        private int                  indent_value;
        private StringBuilder        inst_string_builder;
        private bool                 pretty_print;
        private bool                 validate;
        private bool                 lower_case_properties;
        private TextWriter           writer;
        #endregion


        #region Properties
        public int IndentValue {
            get { return indent_value; }
            set {
                indentation = (indentation / indent_value) * value;
                indent_value = value;
            }
        }

        public bool PrettyPrint {
            get { return pretty_print; }
            set { pretty_print = value; }
        }

        public TextWriter TextWriter {
            get { return writer; }
        }

        public bool Validate {
            get { return validate; }
            set { validate = value; }
        }

        public bool LowerCaseProperties {
            get { return lower_case_properties; }
            set { lower_case_properties = value; }
        }
        #endregion


        #region Constructors
        static JsonWriter ()
        {
            number_format = NumberFormatInfo.InvariantInfo;
        }

        public JsonWriter ()
        {
            inst_string_builder = new StringBuilder ();
            writer = new StringWriter (inst_string_builder);

            Init ();
        }

        public JsonWriter (StringBuilder sb) :
            this (new StringWriter (sb))
        {
        }

        public JsonWriter (TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException ("writer");

            this.writer = writer;

            Init ();
        }
        #endregion


        #region Private Methods
        private void DoValidation (Condition cond)
        {
            if (! context.ExpectingValue)
                context.Count++;

            if (! validate)
                return;

            if (has_reached_end)
                throw new JsonException (
                    "A complete JSON symbol has already been written");

            switch (cond) {
            case Condition.InArray:
                if (! context.InArray)
                    throw new JsonException (
                        "Can't close an array here");
                break;

            case Condition.InObject:
                if (! context.InObject || context.ExpectingValue)
                    throw new JsonException (
                        "Can't close an object here");
                break;

            case Condition.NotAProperty:
                if (context.InObject && ! context.ExpectingValue)
                    throw new JsonException (
                        "Expected a property");
                break;

            case Condition.Property:
                if (! context.InObject || context.ExpectingValue)
                    throw new JsonException (
                        "Can't add a property here");
                break;

            case Condition.Value:
                if (! context.InArray &&
                    (! context.InObject || ! context.ExpectingValue))
                    throw new JsonException (
                        "Can't add a value here");

                break;
            }
        }

        private void Init ()
        {
            has_reached_end = false;
            hex_seq = new char[4];
            indentation = 0;
            indent_value = 4;
            pretty_print = false;
            validate = true;
            lower_case_properties = false;

            ctx_stack = new Stack<WriterContext> ();
            context = new WriterContext ();
            ctx_stack.Push (context);
        }

        private static void IntToHex (int n, char[] hex)
        {
            int num;

            for (int i = 0; i < 4; i++) {
                num = n % 16;

                if (num < 10)
                    hex[3 - i] = (char) ('0' + num);
                else
                    hex[3 - i] = (char) ('A' + (num - 10));

                n >>= 4;
            }
        }

        private void Indent ()
        {
            if (pretty_print)
                indentation += indent_value;
        }


        private void Put (string str)
        {
            if (pretty_print && ! context.ExpectingValue)
                for (int i = 0; i < indentation; i++)
                    writer.Write (' ');

            writer.Write (str);
        }

        private void PutNewline ()
        {
            PutNewline (true);
        }

        private void PutNewline (bool add_comma)
        {
            if (add_comma && ! context.ExpectingValue &&
                context.Count > 1)
                writer.Write (',');

            if (pretty_print && ! context.ExpectingValue)
                writer.Write (Environment.NewLine);
        }

        private void PutString (string str)
        {
            Put (String.Empty);

            writer.Write ('"');

            int n = str.Length;
            for (int i = 0; i < n; i++) {
                switch (str[i]) {
                case '\n':
                    writer.Write ("\\n");
                    continue;

                case '\r':
                    writer.Write ("\\r");
                    continue;

                case '\t':
                    writer.Write ("\\t");
                    continue;

                case '"':
                case '\\':
                    writer.Write ('\\');
                    writer.Write (str[i]);
                    continue;

                case '\f':
                    writer.Write ("\\f");
                    continue;

                case '\b':
                    writer.Write ("\\b");
                    continue;
                }

                if ((int) str[i] >= 32 && (int) str[i] <= 126) {
                    writer.Write (str[i]);
                    continue;
                }

                // Default, turn into a \uXXXX sequence
                IntToHex ((int) str[i], hex_seq);
                writer.Write ("\\u");
                writer.Write (hex_seq);
            }

            writer.Write ('"');
        }

        private void Unindent ()
        {
            if (pretty_print)
                indentation -= indent_value;
        }
        #endregion


        public override string ToString ()
        {
            if (inst_string_builder == null)
                return String.Empty;

            return inst_string_builder.ToString ();
        }

        public void Reset ()
        {
            has_reached_end = false;

            ctx_stack.Clear ();
            context = new WriterContext ();
            ctx_stack.Push (context);

            if (inst_string_builder != null)
                inst_string_builder.Remove (0, inst_string_builder.Length);
        }

        public void Write (bool boolean)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            Put (boolean ? "true" : "false");

            context.ExpectingValue = false;
        }

        public void Write (decimal number)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            Put (Convert.ToString (number, number_format));

            context.ExpectingValue = false;
        }

        public void Write (double number)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            string str = Convert.ToString (number, number_format);
            Put (str);

            if (str.IndexOf ('.') == -1 &&
                str.IndexOf ('E') == -1)
                writer.Write (".0");

            context.ExpectingValue = false;
        }

        public void Write(float number)
        {
            DoValidation(Condition.Value);
            PutNewline();

            string str = Convert.ToString(number, number_format);
            Put(str);

            context.ExpectingValue = false;
        }

        public void Write (int number)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            Put (Convert.ToString (number, number_format));

            context.ExpectingValue = false;
        }

        public void Write (long number)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            Put (Convert.ToString (number, number_format));

            context.ExpectingValue = false;
        }

        public void Write (string str)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            if (str == null)
                Put ("null");
            else
                PutString (str);

            context.ExpectingValue = false;
        }

        [CLSCompliant(false)]
        public void Write (ulong number)
        {
            DoValidation (Condition.Value);
            PutNewline ();

            Put (Convert.ToString (number, number_format));

            context.ExpectingValue = false;
        }

        public void WriteArrayEnd ()
        {
            DoValidation (Condition.InArray);
            PutNewline (false);

            ctx_stack.Pop ();
            if (ctx_stack.Count == 1)
                has_reached_end = true;
            else {
                context = ctx_stack.Peek ();
                context.ExpectingValue = false;
            }

            Unindent ();
            Put ("]");
        }

        public void WriteArrayStart ()
        {
            DoValidation (Condition.NotAProperty);
            PutNewline ();

            Put ("[");

            context = new WriterContext ();
            context.InArray = true;
            ctx_stack.Push (context);

            Indent ();
        }

        public void WriteObjectEnd ()
        {
            DoValidation (Condition.InObject);
            PutNewline (false);

            ctx_stack.Pop ();
            if (ctx_stack.Count == 1)
                has_reached_end = true;
            else {
                context = ctx_stack.Peek ();
                context.ExpectingValue = false;
            }

            Unindent ();
            Put ("}");
        }

        public void WriteObjectStart ()
        {
            DoValidation (Condition.NotAProperty);
            PutNewline ();

            Put ("{");

            context = new WriterContext ();
            context.InObject = true;
            ctx_stack.Push (context);

            Indent ();
        }

        public void WritePropertyName (string property_name)
        {
            DoValidation (Condition.Property);
            PutNewline ();
            string propertyName = (property_name == null || !lower_case_properties)
                ? property_name
                : property_name.ToLowerInvariant();

            PutString (propertyName);

            if (pretty_print) {
                if (propertyName.Length > context.Padding)
                    context.Padding = propertyName.Length;

                for (int i = context.Padding - propertyName.Length;
                     i >= 0; i--)
                    writer.Write (' ');

                writer.Write (": ");
            } else
                writer.Write (':');

            context.ExpectingValue = true;
        }

        // Token: 0x0600014C RID: 332 RVA: 0x00005FCA File Offset: 0x00004FCA
        public void Write(UnityEngine.Vector2 v2)
        {
            this.WriteObjectStart();
            this.WritePropertyName("x");
            this.Write((double)v2.x);
            this.WritePropertyName("y");
            this.Write((double)v2.y);
            this.WriteObjectEnd();
        }

        // Token: 0x0600014D RID: 333 RVA: 0x0000600C File Offset: 0x0000500C
        public void Write(UnityEngine.Vector3 v3)
        {
            this.WriteObjectStart();
            this.WritePropertyName("x");
            this.Write((double)v3.x);
            this.WritePropertyName("y");
            this.Write((double)v3.y);
            this.WritePropertyName("z");
            this.Write((double)v3.z);
            this.WriteObjectEnd();
        }

        // Token: 0x0600014E RID: 334 RVA: 0x00006070 File Offset: 0x00005070
        public void Write(UnityEngine.Vector4 v4)
        {
            this.WriteObjectStart();
            this.WritePropertyName("x");
            this.Write((double)v4.x);
            this.WritePropertyName("y");
            this.Write((double)v4.y);
            this.WritePropertyName("z");
            this.Write((double)v4.z);
            this.WritePropertyName("z");
            this.Write((double)v4.w);
            this.WriteObjectEnd();
        }

        // Token: 0x0600014F RID: 335 RVA: 0x000060F0 File Offset: 0x000050F0
        public void Write(UnityEngine.Quaternion q)
        {
            this.WriteObjectStart();
            this.WritePropertyName("x");
            this.Write((double)q.x);
            this.WritePropertyName("y");
            this.Write((double)q.y);
            this.WritePropertyName("z");
            this.Write((double)q.z);
            this.WritePropertyName("z");
            this.Write((double)q.w);
            this.WriteObjectEnd();
        }

        // Token: 0x06000150 RID: 336 RVA: 0x00006170 File Offset: 0x00005170
        public void Write(UnityEngine.Matrix4x4 m)
        {
            this.WriteObjectStart();
            this.WritePropertyName("m00");
            this.Write((double)m.m00);
            this.WritePropertyName("m33");
            this.Write((double)m.m33);
            this.WritePropertyName("m23");
            this.Write((double)m.m23);
            this.WritePropertyName("m13");
            this.Write((double)m.m13);
            this.WritePropertyName("m03");
            this.Write((double)m.m03);
            this.WritePropertyName("m32");
            this.Write((double)m.m32);
            this.WritePropertyName("m12");
            this.Write((double)m.m12);
            this.WritePropertyName("m02");
            this.Write((double)m.m02);
            this.WritePropertyName("m22");
            this.Write((double)m.m22);
            this.WritePropertyName("m21");
            this.Write((double)m.m21);
            this.WritePropertyName("m11");
            this.Write((double)m.m11);
            this.WritePropertyName("m01");
            this.Write((double)m.m01);
            this.WritePropertyName("m30");
            this.Write((double)m.m30);
            this.WritePropertyName("m20");
            this.Write((double)m.m20);
            this.WritePropertyName("m10");
            this.Write((double)m.m10);
            this.WritePropertyName("m31");
            this.Write((double)m.m31);
            this.WriteObjectEnd();
        }

        // Token: 0x06000151 RID: 337 RVA: 0x00006319 File Offset: 0x00005319
        public void Write(UnityEngine.Ray r)
        {
            this.WriteObjectStart();
            this.WritePropertyName("origin");
            this.Write(r.origin);
            this.WritePropertyName("direction");
            this.Write(r.direction);
            this.WriteObjectEnd();
        }

        // Token: 0x06000152 RID: 338 RVA: 0x00006358 File Offset: 0x00005358
        public void Write(UnityEngine.RaycastHit r)
        {
            this.WriteObjectStart();
            this.WritePropertyName("barycentricCoordinate");
            this.Write(r.barycentricCoordinate);
            this.WritePropertyName("distance");
            this.Write((double)r.distance);
            this.WritePropertyName("normal");
            this.Write(r.normal);
            this.WritePropertyName("point");
            this.Write(r.point);
            this.WriteObjectEnd();
        }

        // Token: 0x06000153 RID: 339 RVA: 0x000063D4 File Offset: 0x000053D4
        public void Write(UnityEngine.Color c)
        {
            this.WriteObjectStart();
            this.Put(string.Format("\"r\":{0},\"g\":{1},\"b\":{2},\"a\":{3}", new object[]
            {
                c.r,
                c.g,
                c.b,
                c.a
            }));
            this.WriteObjectEnd();
        }
    }

	
	
}
