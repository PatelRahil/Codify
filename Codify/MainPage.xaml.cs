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
            object[] info = encode(text);
            string eStr = (string)info[0];
            int seed = (int)info[1];
            ((Button)sender).Text = eStr + " " + seed;
            MessageBox.Text = "Original Text: " + decode(eStr, seed);
        }

        object[] encode(string str) {
            char[] chars = str.ToCharArray();
            Random rnd = new Random();
            int randNum = rnd.Next(0, 100000);
            for (int i = 0; i < str.Length; i++) {
                char c = chars[i];
                if ((int)c == 8217) {
                    c = (char)39;
                }
                if (c > 31 && c < 127) {
                    int newASCII = ((c - 32 + randNum) % (126 - 32 + 1)) + 32;
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
                int newASCII = c - seed + (seed/94) * (126 - 32 + 1);
                int count = 0;
                while (newASCII < 32) {
                    newASCII += (126 - 32 + 1);
                    count++;
                }
                count = 0;
                while (newASCII > 126) {
                    newASCII -= (126 - 32 + 1);
                    count++;
                }

                chars[i] = (char)newASCII;
            }
            string decodedStr = new string(chars);
            return decodedStr;
        }

    }
}
