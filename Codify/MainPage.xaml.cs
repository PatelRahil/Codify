using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Codify
{
    public partial class MainPage : ContentPage
    {
        //Editor MessageBox = (Editor)this.FindName("MessageBox");
        string text = "";
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            text = MessageBox.Text;
            string eStr = (String)encode(text)[0];
            int seed = (int)encode(text)[1];
            ((Button)sender).Text = eStr + " " + seed;
            MessageBox.Text = "Original Text: " + decode(eStr, seed);
        }

        object[] encode(string str) {
            char[] chars = str.ToCharArray();
            Random rnd = new Random();
            int randNum = rnd.Next(0, 100000);

            for (int i = 0; i < str.Length; i++) {
                char c = chars[i];
                if (c > 31 && c < 127) {
                    int newASCII = ((c - 32 + randNum) % (126 - 32)) + 32;
                    chars[i] = (char)newASCII;
                }
            }
            string encodedStr = new string(chars);
            int seed = randNum;
            object[] info = { encodedStr, seed };
            return info;
        }

        string decode(string str, int seed) {
            char[] chars = str.ToCharArray();

            for (int i = 0; i < str.Length; i++)
            {
                char c = chars[i];
                if (c > 31 && c < 127)
                {
                    int newASCII = ((c - 32 - seed) % (126 - 32)) + 32;
                    chars[i] = (char)newASCII;
                    Console.WriteLine(newASCII + " and " + (char)newASCII);
                }
            }
            string decodedStr = new string(chars);
            return decodedStr;
        }

    }
}
