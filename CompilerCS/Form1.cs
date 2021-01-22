using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
namespace CompilerCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CodeBox.Text= @"using System;
namespace Test
{
class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine(""Hello, world!"");
                Console.ReadKey();
            }
        }
    }
";
            nameTextBox.Text = "App.exe";
            FrameworkTBox.Text = "v4.0";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider
                (new Dictionary<string, string>() { { "CompilerVersion", FrameworkTBox.Text } });

            CompilerParameters parameters = new CompilerParameters
                (new string[] { "mscorlib.dll", "System.Core.dll" }, nameTextBox.Text, true);
            parameters.GenerateExecutable = true;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, CodeBox.Text);
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors.Cast<CompilerError>())
                {
                    statusBox.Text += $"Строка{error.Line}: {error.ErrorText}";
                }
            }
            else
            {
                statusBox.Text = "---Сборка завершена---";
                Process.Start($"{Application.StartupPath}/{nameTextBox.Text}");
            }

            }
        }
    }

