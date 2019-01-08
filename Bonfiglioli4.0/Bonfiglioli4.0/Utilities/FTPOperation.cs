using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Bonfiglioli4p0.Utilities
{
    class FTPOperation
    {
        //#### Sito personale online
        public static string FTPSite = "ftp://rabat.altervista.org";
        public static string Usr = "rabat";
        public static string Psw = "z4nYC4bbAqSM";
        public static string SourceFileLocalToFTP = "SampleFile.txt";
        public static string oPathToFTP = @"/";
        public static string DestFileToFTP = "TestFile0.txt";
        public static string PathLocalFTP = @"E:\YourLoaction\";
        public static string FileLocalFTP = "dTestFile0.txt";

        //#### Sito prova ufficio 
        public static string uFTPSite = "ftp://192.168.0.30";
        public static string uUsr = "pipo";
        public static string uPsw = "pipo";
        public static string uSourceFileLocalToFTP = "SampleFile.txt";
        public static string uPathToFTP = @"Poi//Noi";
        public static string uDestFileToFTP = "TestFile0.txt";
        public static string uPathLocalFTP = @"E:\YourLoaction\";
        public static string uFileLocalFTP = "dTestFile0.txt";

        public static void GetallContents()
        {
            try
            {
                FTPSite = uFTPSite;
                Usr = uUsr;
                Psw = uPsw;
                SourceFileLocalToFTP = uSourceFileLocalToFTP;
                DestFileToFTP = uDestFileToFTP;
                PathLocalFTP = uPathLocalFTP;
                FileLocalFTP = uFileLocalFTP;
                oPathToFTP = uPathToFTP;

                //#### nUOVA FUNZIONE c#6 : $ più nome variabile diretto
                Console.WriteLine($"Connect to FTP site : {FTPSite} ");

                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://YourHostname.com/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPSite);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                // This example assumes the FTP site uses anonymous logon.
                //request.Credentials = new NetworkCredential("maruthi", "******");
                request.Credentials = new NetworkCredential(Usr, Psw);
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

                reader.Close();
                response.Close();
                Console.WriteLine("Premi enter");
                Console.Read();

                Console.WriteLine($"Lettura dati da FTP : {FTPSite}");
                Console.WriteLine("\r\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.Read();
            }
        }

        public static void PostDatatoFTP(string p_File)
        {
            try
            {
                //################################################################ 192.168.0.30 ###  "" ################ c3147prg.txt
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("FTP://" + FTPSite + "//" + oPathToFTP + "//" + p_File);
                //ftp://rabat@rabat.altervista.org/TestFile0.txt
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;
                //request.Credentials = new NetworkCredential("maruthi", "*******");
                request.Credentials = new NetworkCredential(Usr, Psw);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(PathLocalFTP + SourceFileLocalToFTP);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();

                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message.ToString());
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(status);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadLine();
            }
        }

        public static void GetDataFromFTP()
        {
            try
            {
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://YourHostname.com/TestFile0.txt");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPSite + "//" + uPathToFTP + "//" + DestFileToFTP);

                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);

                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //request.Credentials = new NetworkCredential("maruthi", "********");
                request.Credentials = new NetworkCredential(Usr, Psw);

                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(responseStream);

                //#### Serve a scrivere a video il contenuto del file
                //byte[] fileContents = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                //Console.WriteLine(reader.ReadToEnd());

                Console.Read();

                String eqfew = reader.ReadToEnd();
                //using (StreamWriter sr = File.AppendText(@"E:\YourLoaction\dTestFile0.txt"))
                using (StreamWriter sr = File.AppendText(PathLocalFTP + FileLocalFTP))
                {
                    sr.Write(eqfew);
                    sr.Close();

                }
                reader.Close();

                //Stream s = new  ("application.log");

                Console.WriteLine("Download Complete", response.StatusDescription);
                Console.WriteLine("File su FTP aggiornato", response.StatusDescription);

                reader.Close();

                response.Close();
                Console.WriteLine("Premi enter per terminare");
                Console.ReadLine();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message.ToString());
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(status);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadLine();
            }

        }

        //static void Main(string[] args)
        //{
        //    Program.GetallContents();
        //    //POST SampleFile in textFile 
        //    Program.PostDatatoFTP(0);
        //    //GET TestFile0 new 
        //    Program.GetDataFromFTP();
        //}
    }

}
