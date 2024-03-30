using System.Text;
using Microsoft.VisualBasic.ApplicationServices;
using System.Configuration;
using System.Security.Cryptography;

namespace ZastitaProjekat

{
    public partial class Form1 : Form
    {
        private string sharedDirectory = @"C:\Users\nis70\OneDrive\Desktop\SharedFiles";

        private LEACipher leaCipher;
        public Form1()
        {
            InitializeComponent();
            leaCipher = new LEACipher();


        }

        private bool isProcessingChange = false;

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (isProcessingChange)
            {
                return;
            }

            try
            {
                isProcessingChange = true;

                string content = ReadFileContent(e.FullPath);

                if (comboBox1.Text == "TEA")
                {
                    string p = Crypt(content);
                    string d = Decrypt(p);
                    richTextBox2.Invoke((MethodInvoker)delegate
                    {
                        richTextBox2.Text = d;
                    });
                    WriteLine(d);
                }
                else if (comboBox1.Text == "LEA")
                {
                    byte[] IV = new byte[16];
                    IV = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

                    byte[] key = {
                    0x0f, 0x1e, 0x2d, 0x3c, 0x4b, 0x5a, 0x69, 0x78, 0x87, 0x96, 0xa5, 0xb4, 0xc3, 0xd2, 0xe1, 0xf0
                    };

                    byte[] plaintext = ConvertStringToBytes(content);

                    if (content.Length == 16)
                    {
                        uint[] roundKeys = new uint[144];
                        LEACipher.KeySchedule_128(key, roundKeys);

                        byte[] ciphertext = new byte[16];

                        LEACipher.EncryptCTR(24, roundKeys, IV, plaintext, ciphertext);

                        byte[] decryptedText = new byte[16];
                        LEACipher.DecryptCTR(24, roundKeys, IV, decryptedText, ciphertext);

                        richTextBox2.Invoke((MethodInvoker)delegate
                        {
                            richTextBox2.Text = ConvertBytesToString(decryptedText);
                        });

                        WriteLine(ConvertBytesToString(decryptedText));
                    }
                    else
                    {
                        richTextBox2.Clear();
                        MessageBox.Show("16 numbers required!!");
                    }
                }
                


                string sentinelFilePath = Path.Combine(sharedDirectory, "newFile.txt");
                if (File.Exists(sentinelFilePath))
                {
                    string newFilePath = File.ReadAllText(sentinelFilePath);

                    string sentHash = File.ReadAllText(Path.Combine(sharedDirectory, "hash.txt"));

                    string newFileContent = ReadFileContent(newFilePath);
                    richTextBox2.Invoke((MethodInvoker)delegate
                    {
                        richTextBox2.Text = newFileContent;
                    });


                    ReceiveFile(newFilePath, sentHash);


                    File.Delete(sentinelFilePath);
                }
            }
            finally
            {
                isProcessingChange = false;
            }
        }






        private byte[] ConvertStringToBytes(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        private string ConvertBytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }


        public static string ConvertBytesToString(byte[] byteArray, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string resultString = encoding.GetString(byteArray);

            return resultString;
        }

        public static byte[] ConvertStringToBytes(string input, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] byteArray = encoding.GetBytes(input);

            return byteArray;
        }

        string key;

        private static UInt32[] K = new UInt32[4];

        public static uint ConvertStringToUInt(string Input)
        {
            uint output;
            output = 0;
            for (int i = 0; i < Input.Length; i++)

                output += ((uint)Input[i] << i * 8);

            return output;
        }



        private static void WriteLine(string line)
        {
            try
            {
                File.WriteAllText(@"C:\Users\nis70\OneDrive\Desktop\Promene\promene.txt", line + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to the file: " + ex.Message);
            }
        }


        private static string ReadFileContent(string filePath)
        {
            try
            {
                string content;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                return content;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
                return null;
            }
        }


        public static string ConvertUIntToString(uint Input)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            output.Append((char)((Input & 0xFF)));
            output.Append((char)((Input >> 8) & 0xFF));
            output.Append((char)((Input >> 16) & 0xFF));
            output.Append((char)((Input >> 24) & 0xFF));
            return output.ToString();
        }


        byte[] GetAsciiBytes(string source)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding enc_default = Encoding.Default;

            return Encoding.Convert(enc_default, ascii, enc_default.GetBytes(source));

        }

        public char[] GetAsciiChars(string source)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding enc_default = Encoding.Default;

            byte[] asciiBytes = Encoding.Convert(enc_default, ascii, enc_default.GetBytes(this.key));
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];

            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);

            return asciiChars;
        }

        public string FromCharArrayToString(char[] start)
        {
            string ret = "";
            for (int i = 0; i < start.Length; i++)
            {
                ret += start[i];
            }

            return ret;
        }



        public static string Crypt(string source)
        {
            string delta1 = ConfigurationManager.AppSettings["key"];
            uint delta = Convert.ToUInt32(delta1, 16);
            string cipherText = "";
            byte[] dataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(source);
            uint[] tempData = new uint[2];

            for (int j = 0; j < dataBytes.Length; j += 2)
            {


                uint L = dataBytes[j];
                uint R = 0;
                if (dataBytes.Length >= j + 2)
                {

                    R = dataBytes[j + 1];
                }



                UInt32 sum = 0;

                for (int i = 0; i < 32; i++)
                {
                    sum += delta;
                    L += (((R << 4) + K[0]) ^ (R + sum) ^ ((R >> 5) + K[1]));
                    R += (((L << 4) + K[2]) ^ (L + sum) ^ ((L >> 5) + K[3]));
                }


                cipherText += ConvertUIntToString(L) + ConvertUIntToString(R);
            }

            return cipherText;
        }


        public static string Decrypt(string source)
        {
            string delta1 = ConfigurationManager.AppSettings["key"];
            uint delta = Convert.ToUInt32(delta1, 16);

            byte[] dataBytes = new byte[source.Length / 8 * 2];
            int x = 0;
            for (int i = 0; i < source.Length; i += 8)
            {
                uint L = ConvertStringToUInt(source.Substring(i, 4));
                uint R = ConvertStringToUInt(source.Substring(i + 4, 4));

                UInt32 sum = delta << 5;

                for (int j = 0; j < 32; j++)
                {
                    R -= (((L << 4) + K[2]) ^ (L + sum) ^ ((L >> 5) + K[3]));
                    L -= (((R << 4) + K[0]) ^ (R + sum) ^ ((R >> 5) + K[1]));

                    sum -= delta;
                }

                dataBytes[x++] = (byte)L;
                dataBytes[x++] = (byte)R;
            }
            string decipheredString = System.Text.ASCIIEncoding.ASCII.GetString(dataBytes, 0, dataBytes.Length);
            if (decipheredString[decipheredString.Length - 1] == '\0')
                decipheredString = decipheredString.Substring(0, decipheredString.Length - 1);
            return decipheredString;

        }




        private void Create_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\nis70\OneDrive\Desktop\FileWatcher\" + textBox1.Text + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter file = new StreamWriter(path))
                {
                    file.WriteLine(richTextBox1.Text);
                }
                MessageBox.Show("Successfully created a new file" + ":" + textBox1.Text);


                
            }
            else
            {
                MessageBox.Show("This file already exists!");
            }
        }



        private void SendFileNotification(string filePath)
        {
            try
            {

                string sentinelFilePath = Path.Combine(sharedDirectory, "newFile.txt");
                File.WriteAllText(sentinelFilePath, filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to the sentinel file: " + ex.Message);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string content = File.ReadAllText(@"C:\Users\nis70\OneDrive\Desktop\Promene\promene.txt");
            string p = Crypt(content);
            richTextBox3.Text = p;
            richTextBox3.Invoke((MethodInvoker)delegate
            {
                richTextBox3.Text = p;
            });


        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            textBox1.Text = dlg.SelectedPath;
        }





        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                var files = System.IO.Directory.GetFiles(textBox1.Text, "*.txt*", System.IO.SearchOption.AllDirectories);

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("FileNameColumn", "File Name");

                foreach (var file in files)
                {
                    dataGridView1.Rows.Add(System.IO.Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Select a file!");

            }

        }







        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
            {
                string fileName = dataGridView1.Rows[rowIndex].Cells["FileNameColumn"].Value.ToString();

                string filePath = System.IO.Path.Combine(textBox1.Text, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    string fileContent = System.IO.File.ReadAllText(filePath);
                    richTextBox1.Text = fileContent;
                }

            }
            richTextBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                
                MessageBox.Show("Pick a cypher!!");
                richTextBox2.Clear();

            }
           

            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {

                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;


                    string fileName = dataGridView1.Rows[rowIndex].Cells["FileNameColumn"].Value.ToString();


                    string filePath = System.IO.Path.Combine(textBox1.Text, fileName);


                    if (System.IO.File.Exists(filePath))
                    {

                        System.IO.File.WriteAllText(filePath, richTextBox1.Text);

                        string content = File.ReadAllText(@"C:\Users\nis70\OneDrive\Desktop\Promene\promene.txt");
                        richTextBox2.Text = content;
                    }
                }
                else
                {
                    MessageBox.Show("A file was not selected", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Select a file!");

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (comboBox1.Text == "TEA")
                {
                    string content = File.ReadAllText(@"C:\Users\nis70\OneDrive\Desktop\Promene\promene.txt");
                    string p = Crypt(content);
                    richTextBox3.Text = p;
                    richTextBox3.Invoke((MethodInvoker)delegate
                    {
                        richTextBox3.Text = p;
                    });
                }
                else
                {
                    byte[] key = {
            0x0f, 0x1e, 0x2d, 0x3c, 0x4b, 0x5a, 0x69, 0x78, 0x87, 0x96, 0xa5, 0xb4, 0xc3, 0xd2, 0xe1, 0xf0
        };
                    byte[] IV = new byte[16];
                    IV = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };


                    byte[] plaintext = ConvertStringToBytes(File.ReadAllText(@"C:\Users\nis70\OneDrive\Desktop\Promene\promene.txt"));


                    uint[] roundKeys = new uint[144];
                    LEACipher.KeySchedule_128(key, roundKeys);

                    byte[] ciphertext = new byte[16];


                    LEACipher.EncryptCTR(24, roundKeys, IV, plaintext, ciphertext);
                    richTextBox3.Invoke((MethodInvoker)delegate
                    {
                        ConvertBytesToString(ciphertext);
                        richTextBox3.Text = BitConverter.ToString(ciphertext);
                    });
                }

            }
            else
            {
                richTextBox3.Clear();
            }




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "LEA")
            {
                MessageBox.Show("Put in a 16 digit number!!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                string fileName = dataGridView1.Rows[rowIndex].Cells["FileNameColumn"].Value.ToString();
                string filePath = Path.Combine(textBox1.Text, fileName);

                if (File.Exists(filePath))
                {
                    string sharedFilePath = Path.Combine(sharedDirectory, fileName);
                    if (!File.Exists(sharedFilePath))
                    {
                        byte[] fileContent = File.ReadAllBytes(filePath);


                        SendFileContent(fileContent, fileName);


                        MessageBox.Show($"File '{fileName}' sent successfully.");
                    }
                    else
                    {
                        MessageBox.Show($"File '{fileName}' has already been sent.");
                    }
                }
            }
            else
            {
                MessageBox.Show("A file was not selected", "Error");
            }
        }

        private void SendFileContent(byte[] content, string name)
        {
            try
            {
                string fileName = name;
                string filePath = Path.Combine(sharedDirectory, fileName);
                File.WriteAllBytes(filePath, content);

                // Calculate SHA-1 hash of the file content
                string sha1Hash = CalculateSHA1Hash(content);

                // Append the hash to the filename
                string hashAppendedFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{sha1Hash}{Path.GetExtension(fileName)}";
                string hashAppendedFilePath = Path.Combine(sharedDirectory, hashAppendedFileName);

                // Rename the file with the hash appended
                File.Move(filePath, hashAppendedFilePath);

                MessageBox.Show($"File '{fileName}' sent successfully with SHA-1 hash: {sha1Hash}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending file: {ex.Message}");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var files = System.IO.Directory.GetFiles("C:\\Users\\nis70\\OneDrive\\Desktop\\SharedFiles", "*.txt*", System.IO.SearchOption.AllDirectories);

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("FileNameColumn", "File Name");

            foreach (var file in files)
            {
                dataGridView2.Rows.Add(System.IO.Path.GetFileName(file));
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0 && rowIndex < dataGridView2.Rows.Count)
            {
                string fileName = dataGridView2.Rows[rowIndex].Cells["FileNameColumn"].Value.ToString();

                string filePath = System.IO.Path.Combine("C:\\Users\\nis70\\OneDrive\\Desktop\\SharedFiles", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    string fileContent = System.IO.File.ReadAllText(filePath);
                    richTextBox1.Text = fileContent;
                }

            }
            richTextBox2.Text = "";
        }

        private string CalculateSHA1Hash(byte[] content)
        {

            return SHA1Al.CalculateSHA1(Encoding.UTF8.GetString(content));
        }



        private bool VerifyFileIntegrity(string filePath, string sentHash)
        {
            try
            {
                byte[] receivedContent = File.ReadAllBytes(filePath);
                string calculatedHash = CalculateSHA1Hash(receivedContent);
                return string.Equals(calculatedHash, sentHash, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error verifying file integrity: {ex.Message}");
                return false;
            }
        }






        private void ReceiveFile(string filePath, string sentHash)
        {
            if (VerifyFileIntegrity(filePath, sentHash))
            {
                MessageBox.Show("File integrity verified. The file was received successfully.");

            }
            else
            {
                MessageBox.Show("File integrity check failed. The received file may be corrupted.");

            }
        }






    }
}







