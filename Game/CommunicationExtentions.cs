using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    internal static class CommunicationExtensions
    {
        internal static void SendMove(this Socket socket, int location)
        {
            byte[] buffer = new byte[16];
            buffer[15] = 9;
            buffer[location - 1] = 1;
            var encrypted = Encryptor.Encrypt(buffer);

            socket.Send(encrypted);
        }

        internal static void ApplyMove(this Button[] buttons, byte[] data, String opponent)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 1)
                {
                    buttons[i].Text = opponent;
                    buttons[i].Enabled = false;
                    return;
                }
            }
        }

        internal static void SendLoaded(this Socket socket, Button[] buttons)
        {
            byte[] buffer = new byte[16];
            buffer[15] = 0;
            int xCount = 0;
            int oCount = 0;

            for (int i = 0; i < 9; i++)
            {
                if (buttons[i].Text == "X")
                {
                    buffer[i] = 1;
                    xCount++;
                }
                else if (buttons[i].Text == "O")
                {
                    buffer[i] = 2;
                    oCount++;
                }
            }

            buffer[10] = (byte)(oCount > xCount ? 1 : 2);

            var encrypted = Encryptor.Encrypt(buffer);
            socket.Send(encrypted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="data"></param>
        /// <returns>whose turn it is</returns>
        internal static String LoadBoardFromData(this Button[] buttons, byte[] data)
        {
            int xCount = 0;
            int oCount = 0;

            for (int i = 0; i < 9; i++)
            {
                switch (data[i])
                {
                    case 1:
                        buttons[i].Text = "X";
                        buttons[i].Enabled = false;
                        xCount++;
                        break;
                    case 2:
                        buttons[i].Text = "O";
                        buttons[i].Enabled = false;
                        oCount++;
                        break;
                    default:
                        buttons[i].Text = "";
                        buttons[i].Enabled = true;
                        break;
                }
            }
            return oCount > xCount ? "X" : "O";
        }

        internal static (byte[] data, bool isLoaded) ReceiveData(this Socket socket)
        {
            var received = new byte[16];
            socket.Receive(received);

            var decrypted = Encryptor.Decrypt(received);

            return (decrypted, decrypted[15] == 0);
        }
    }
}
