using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Org.BouncyCastle.Crypto.Digests;



namespace Functii_hash_criptografice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                FilePathText.Text = dlg.FileName;
                ResultText.Text = "Calculating hashes...";

                string result = await Task.Run(() => ComputeHashes(dlg.FileName));
                ResultText.Text = result;
            }
        }
        private string ComputeHashes(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            using FileStream fs = File.OpenRead(filePath);

            sb.AppendLine("MD5:");
            sb.AppendLine(ToHex(MD5.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nRIPEMD160:");

            fs.Position = 0;
            var ripemd = new RipeMD160Digest();
            byte[] buffer = new byte[8192];
            int read;
            while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                ripemd.BlockUpdate(buffer, 0, read);
            }
            byte[] ripemdHash = new byte[ripemd.GetDigestSize()];
            ripemd.DoFinal(ripemdHash, 0);
            sb.AppendLine(ToHex(ripemdHash));

            fs.Position = 0;

            sb.AppendLine("\nSHA1:");
            sb.AppendLine(ToHex(SHA1.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nSHA256:");
            sb.AppendLine(ToHex(SHA256.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nSHA384:");
            sb.AppendLine(ToHex(SHA384.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nSHA512:");
            sb.AppendLine(ToHex(SHA512.HashData(fs)));
            fs.Position = 0;

#if NET8_0_OR_GREATER
            sb.AppendLine("\nSHA3-256:");
            sb.AppendLine(ToHex(SHA3_256.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nSHA3-384:");
            sb.AppendLine(ToHex(SHA3_384.HashData(fs)));
            fs.Position = 0;

            sb.AppendLine("\nSHA3-512:");
            sb.AppendLine(ToHex(SHA3_512.HashData(fs)));
#endif

            return sb.ToString();
        }

        private string ToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }
    }
}