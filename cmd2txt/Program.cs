using System;

namespace cmd2txt
{
    class Program
    {
        static void Main(string[] args)
        {

            var stdout = "";

            try
            {
                using (var p = new System.Diagnostics.Process())
                {

                    p.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                    p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                    p.StartInfo.Arguments = "/c " + args[1];

                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;

                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.ErrorDialog = false;
                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;

                    p.Start();
                    p.WaitForExit();

                    stdout = p.StandardOutput.ReadToEnd();

                }
            }
            catch (Exception e)
            {
                stdout = "[error]:" + e.Message;
            }

            try
            {
                using (var sw = new System.IO.StreamWriter(args[0], false, System.Text.Encoding.GetEncoding("shift_jis")))
                {

                    sw.Write(stdout);

                }
            }
            catch (Exception e)
            {
                stdout = "[error]:" + e.Message;
            }

            System.Console.WriteLine(stdout);

            //System.Console.ReadLine();

        }
    }

}

