using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;

namespace SaveEditorTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //byte array that will hold the save data after loading it
        private byte[] savedata = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadSave(object sender, RoutedEventArgs e)
        {
            Stream inputStream = null;
            //setup open file box, filter to our save file's extension
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Save files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //if dialog returns true (= if we pressed OK, not cancel)
            if (openFileDialog1.ShowDialog() == true)
            {
                if ((inputStream = openFileDialog1.OpenFile()) != null)
                {
                    using (inputStream)
                    {
                        //create a binary reader to read data from save file
                        BinaryReader breader = new BinaryReader(inputStream);
                        //make sure the reader starts at 0x0, beginning of the file
                        breader.BaseStream.Position = 0x0;
                        //copy save file into our global savedata byte array
                        savedata = breader.ReadBytes((int)inputStream.Length);

                        //example - to read 4 bytes at 0x1234, and put value into a textbox
                        //and then read 2 bytes (at 0x1238), and put into another textbox
                        //breader.BaseStream.Position = 0x1234;
                        //textbox.Text = breader.ReadInt32().ToString();
                        //** breader advances the position as you read **
                        //textbox2.Text = breader.ReadInt16().ToString();
                    }
                }
            }
        }

        private void exportSave(object sender, RoutedEventArgs e)
        {
            Stream outputStream = null;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Save files (*.dat)|*.dat|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            //if users clicks ok
            if (saveFileDialog1.ShowDialog() == true)
            {
                if ((outputStream = saveFileDialog1.OpenFile()) != null)
                {
                    using (outputStream)
                    {
                        //copy input save data to output stream
                        outputStream.Write(savedata, 0, savedata.Length);
                        //create a binarywriter to edit the data in the output stream
                        BinaryWriter bwriter = new BinaryWriter(outputStream);
                        //now, start editing :)

                        //example - to write 4 bytes at 0x1234, from value in a textbox
                        //and then write 2 bytes (at 0x1238), from another textbox
                        //bwriter.BaseStream.Position = 0x1234;
                        //bwriter.Write((Int32)textbox.Value);
                        //** bwriter advances the position as you write **
                        //bwriter.Write((Int16)textbox2.Value);
                    }
                }
            }
        }
    }
}
