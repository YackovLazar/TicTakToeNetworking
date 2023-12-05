using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text;

namespace Game
{
    public partial class Game : Form
    {
        private string savedGame;
        private readonly char PlayerChar;
        private readonly char OpponentChar;
        private readonly Socket sock;
        private readonly BackgroundWorker MessageReceiver = new BackgroundWorker();
        private readonly TcpListener server = null;
        private readonly TcpClient client;
        private readonly NetworkStream stream;

        public Game(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            if (isHost) //Meaning we are the host and the opponent is the guest on our server
            {
                PlayerChar = 'X';
                OpponentChar = 'O';
                try
                {
                    server = new TcpListener(System.Net.IPAddress.Any, 8080);
                    server.Start();
                    sock = server.AcceptSocket(); //Accept the incoming connection and then asign the socket to the sock object which then can be used to recieve messages on this channel


                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
            else
            {
                PlayerChar = 'O';
                OpponentChar = 'X';
                try
                {
                    client = new TcpClient(ip, 8080); //We are the 'guest'
                    sock = client.Client;

                    // Get the network stream
                    NetworkStream stream = client.GetStream();

                    MessageReceiver.RunWorkerAsync(); //To recieve the move of the opponent. This will call the DoWork method
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CheckState()) // If true, it means the game is over
                return;
            FreezeBoard(); // To allow the opponent to move
            label1.Text = "Opponent's Turn!";
            ReceiveMove();
            label1.Text = "Your Turn!";
            if (!CheckState())
                UnfreezeBoard();

            // Receive a string from the server
            byte[] receiveBuffer = new byte[1024];
            int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
            string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, bytesRead);
            Console.WriteLine($"Received from server: {receivedMessage}");

        }

        private void checkWinner(char input)
        {
            if (input.Equals(PlayerChar))
            {
                label1.Text = "You Won!";
                MessageBox.Show("You Won!");
            }
            else
            {
                label1.Text = "You Lost!";
                MessageBox.Show("You Lost!");
            }
        }

        private bool CheckState()
        {
            //Horizontals
            if (button1.Text == button2.Text && button2.Text == button3.Text && button3.Text != "")
            {
                checkWinner(button1.Text[0]);
                return true;
            }

            else if (button4.Text == button5.Text && button5.Text == button6.Text && button6.Text != "")
            {
                checkWinner(button4.Text[0]);
                return true;
            }

            else if (button7.Text == button8.Text && button8.Text == button9.Text && button9.Text != "")
            {
                checkWinner(button7.Text[0]);
                return true;
            }

            //Verticals
            else if (button1.Text == button4.Text && button4.Text == button7.Text && button7.Text != "")
            {
                checkWinner(button1.Text[0]);
                return true;
            }

            else if (button2.Text == button5.Text && button5.Text == button8.Text && button8.Text != "")
            {
                checkWinner(button2.Text[0]);
                ;
                return true;
            }

            else if (button3.Text == button6.Text && button6.Text == button9.Text && button9.Text != "")
            {
                checkWinner(button3.Text[0]);
                return true;
            }

            else if (button1.Text == button5.Text && button5.Text == button9.Text && button9.Text != "")
            {
                checkWinner(button1.Text[0]);
                return true;
            }

            else if (button3.Text == button5.Text && button5.Text == button7.Text && button7.Text != "")
            {
                checkWinner(button3.Text[0]);
                return true;
            }

            //Draw
            else if (button1.Text != "" && button2.Text != "" && button3.Text != "" && button4.Text != "" && button5.Text != "" && button6.Text != "" && button7.Text != "" && button8.Text != "" && button9.Text != "")
            {
                label1.Text = "It's a draw!";
                MessageBox.Show("It's a draw!");
                return true;
            }
            return false;
        }

        private void FreezeBoard()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        private void UnfreezeBoard()
        {
            if (button1.Text == "")
                button1.Enabled = true;
            if (button2.Text == "")
                button2.Enabled = true;
            if (button3.Text == "")
                button3.Enabled = true;
            if (button4.Text == "")
                button4.Enabled = true;
            if (button5.Text == "")
                button5.Enabled = true;
            if (button6.Text == "")
                button6.Enabled = true;
            if (button7.Text == "")
                button7.Enabled = true;
            if (button8.Text == "")
                button8.Enabled = true;
            if (button9.Text == "")
                button9.Enabled = true;
        }


        private void ReceiveMove()
        {
            byte[] buffer = new byte[1];
            sock.Receive(buffer);
            if (buffer[0] == 1)
                button1.Text = OpponentChar.ToString();
            if (buffer[0] == 2)
                button2.Text = OpponentChar.ToString();
            if (buffer[0] == 3)
                button3.Text = OpponentChar.ToString();
            if (buffer[0] == 4)
                button4.Text = OpponentChar.ToString();
            if (buffer[0] == 5)
                button5.Text = OpponentChar.ToString();
            if (buffer[0] == 6)
                button6.Text = OpponentChar.ToString();
            if (buffer[0] == 7)
                button7.Text = OpponentChar.ToString();
            if (buffer[0] == 8)
                button8.Text = OpponentChar.ToString();
            if (buffer[0] == 9)
                button9.Text = OpponentChar.ToString();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] num = { 1 };
            sock.Send(num);
            button1.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] num = { 2 };
            sock.Send(num);
            button2.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            byte[] num = { 3 };
            sock.Send(num);
            button3.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            byte[] num = { 4 };
            sock.Send(num);
            button4.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            byte[] num = { 5 };
            sock.Send(num);
            button5.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            byte[] num = { 6 };
            sock.Send(num);
            button6.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            byte[] num = { 7 };
            sock.Send(num);
            button7.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            byte[] num = { 8 };
            sock.Send(num);
            button8.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            byte[] num = { 9 };
            sock.Send(num);
            button9.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageReceiver.WorkerSupportsCancellation = true;
            MessageReceiver.CancelAsync();
            if (server != null) //ie we are hosting the server
                server.Stop();
        }

        private void LoadGame_Clicked(object sender, EventArgs e)
        {
            if (savedGame == null)
            {
                MessageBox.Show("No saved game found");
                return;
            }
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

            // Deserialize the JSON string to a dictionary using System.Text.Json.JsonSerializer
            List<ButtonInfo> deserializedList = JsonSerializer.Deserialize<List<ButtonInfo>>(savedGame);
            string name;
            foreach (ButtonInfo b in deserializedList)
            {
                name = b.Name;
                if (name == "button1")
                {
                    button1.Text = b.Text;
                    button1.Enabled = false;
                }
                else if (name == "button2")
                {
                    button2.Text = b.Text;
                    button2.Enabled = false;
                }
                else if (name == "button3")
                {
                    button3.Text = b.Text;
                    button3.Enabled = false;
                }
                else if (name == "button4")
                {
                    button4.Text = b.Text;
                    button4.Enabled = false;
                }
                else if (name == "button5")
                {
                    button5.Text = b.Text;
                    button5.Enabled = false;
                }
                else if (name == "button6")
                {
                    button6.Text = b.Text;
                    button6.Enabled = false;
                }
                else if (name == "button7")
                {
                    button7.Text = b.Text;
                    button7.Enabled = false;
                }
                else if (name == "button8")
                {
                    button8.Text = b.Text;
                    button8.Enabled = false;
                }
                else if (name == "button9")
                {
                    button9.Text = b.Text;
                    button9.Enabled = false;
                }
                else
                {
                    // code to be executed if none of the conditions match
                }
            }

            // Send a string to the client
            string messageToSend = "Hello, client!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageToSend);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Console.WriteLine($"Received from server: {messageToSend}");

        }

        private void SaveGame_Clicked(Object sender, EventArgs e)
        {
            List<ButtonInfo> buttonInfoList = new List<ButtonInfo>();
           // IContainer c = this.components;
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button b = (Button)control;
                    buttonInfoList.Add(new ButtonInfo { Name = b.Name, Text = b.Text });
                }
            }

            // Serialize the list to JSON
            string json = JsonSerializer.Serialize(buttonInfoList);

            savedGame = json;
            // Deserialize the JSON back to the list
            MessageBox.Show(savedGame);
        }
        // Define a class to represent the information you want to serialize
        public class ButtonInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
        }
      
    }
}
