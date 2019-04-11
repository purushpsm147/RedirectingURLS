using System;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ListOfRedirectingURL
{
    class Program
    {
        static void GenerateURL()
        {
            string path = @"C: \Users\purushottam.prasad\Desktop\";

            string[] files = Directory.GetFiles(path, "links.txt", SearchOption.AllDirectories).Select(p => Path.GetFullPath(p)).ToArray(); //seraching for rewritingMap.config in directory and storing file path in files variable
            int count = 0;

            using (FileStream fs = File.Open(files[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs, Encoding.UTF8, true, 4096))
            {
                string line = string.Empty;
                using (TextWriter tw = new StreamWriter(@"C: \Users\purushottam.prasad\Desktop\homepage.txt"))
                {

                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(line);
                            myHttpWebRequest.Timeout = 20000;
                            myHttpWebRequest.MaximumAutomaticRedirections = 1;
                            myHttpWebRequest.AllowAutoRedirect = true;
                            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                            if (myHttpWebResponse.ResponseUri.ToString() == "https://www.techsoup.org/")
                            {

                                tw.WriteLine(line);
                            }




                            myHttpWebResponse.Close();
                        }
                        catch (WebException)
                        {

                            Console.WriteLine(++count);
                        }
                    }
                }


            }
            


        }



        static void Main(string[] args)
        {
            //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://www-dv.techsoup.org/Community/Pages/CommunityFAQ.aspx");
            //myHttpWebRequest.MaximumAutomaticRedirections = 1;
            //myHttpWebRequest.AllowAutoRedirect = true;
            //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            GenerateURL();


        }
    }
}

