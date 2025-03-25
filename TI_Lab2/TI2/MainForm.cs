using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace TI_Lab2
{
    public partial class MainForm : Form
    {
        private СlassCipher cipher = new СlassCipher();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Font = new Font(button.Font.FontFamily, 8.25f, FontStyle.Bold | FontStyle.Underline);
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Font = new Font(button.Font.FontFamily, 8.25f, FontStyle.Regular);
            }
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            //textBoxInput27.Text = checkData.CheckForBD(textBoxInput27.Text);
            string input27 = checkData.CheckForBD(textBoxInput27.Text);
            if (input27.Length != 27)
            {
                MessageBox.Show("Некорректные данные вполе ключа!\nДлина вашего регистра должна равняться 27 состояниям!", "Внимание");
                return;
            }

            if (string.IsNullOrEmpty(textBoxDataInput.Text))
            {
                MessageBox.Show("Выберите файл с вашим исходным текстом для шифрования/дешифрования или введите текст в поле!", "Внимание");
                return;
            }

            BitArray plainText = ArrayOfBits.StringToBitArray(textBoxDataInput.Text);
            BitArray bitRegister = cipher.FindBitRegister(input27);
            BitArray bitKey = cipher.FindBitKey(bitRegister, plainText.Length);

            textBoxKey.Text = ArrayOfBits.BitArrayToStr(bitKey);

            BitArray cipherBit = cipher.Cipher(bitKey, plainText);
            textBoxOutputData.Text = ArrayOfBits.BitArrayToStr(cipherBit);
        }

        private async Task<BitArray> ReadFileToBitArrayAsync(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                byte[] buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
                return new BitArray(buffer);
            }
        }

        private async Task SaveBitArrayToFileAsync(string filePath, BitArray bitArray)
        {
            byte[] bytes = new byte[(bitArray.Length + 7) / 8];
            bitArray.CopyTo(bytes, 0);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        private async void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    BitArray fileData = await ReadFileToBitArrayAsync(openFileDialog.FileName);
                    textBoxDataInput.Text = ArrayOfBits.BitArrayToStr(fileData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить файл";
            saveFileDialog.AddExtension = true;
            saveFileDialog.OverwritePrompt = false;

            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    await SaveBitArrayToFileAsync(saveFileDialog.FileName, ArrayOfBits.StringToBitArray(textBoxOutputData.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxInput27_TextChanged(object sender, EventArgs e)
        {
            label5.Text = $@"{checkData.CheckForBD(textBoxInput27.Text).Length}";
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            label8.Text = $@"{checkData.CheckForBD(textBoxKey.Text).Length}";
        }

        private void textBoxDataInput_TextChanged(object sender, EventArgs e)
        {
            label6.Text = $@"{checkData.CheckForBD(textBoxDataInput.Text).Length}";
            ClearForNew();
        }

        private void textBoxOutputData_TextChanged(object sender, EventArgs e)
        {
            label7.Text = $@"{checkData.CheckForBD(textBoxOutputData.Text).Length}";
            buttonSave.Enabled = textBoxOutputData.Text.Length > 0;
        }

        private void ClearAll()
        {
            textBoxInput27.Clear();
            textBoxDataInput.Clear();
            textBoxKey.Clear();
            textBoxOutputData.Clear();

            cipher = new СlassCipher();
        }

        private void ClearForNew()
        {
            textBoxKey.Clear();
            textBoxOutputData.Clear();

            cipher = new СlassCipher();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void buttonRev_Click(object sender, EventArgs e)
        {
            textBoxDataInput.Text = textBoxOutputData.Text;
            textBoxOutputData.Clear();
        }
    }
}