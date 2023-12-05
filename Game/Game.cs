using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.ComponentModel;
using System.Collections.Generic;
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
        private Button[] buttons = new Button[9];

        public Game(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;
            buttons = new [] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            if (isHost) //Meaning we are the host and the opponent is the guest on our server
            {
                PlayerChar = 'X';
                OpponentChar = 'O';
                try
                {
                    server = new TcpListener(System.Net.IPAddress.Any, 8080);
                    server.Start();
                    sock = server.AcceptSocket(); //Accept the incoming connection and then asign the socket to the sock object which then can be used to recieve messages on this channel

                    stream = new NetworkStream(sock);
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
                    stream = client.GetStream();

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
            ActivateBoard(false);

            if (CheckState()) // If true, it means the game is over
            {
                return;
            }
            label1.Text = "Opponent's Turn!";



            ManageInput();

            label1.Text = "Your Turn!";
            if (!CheckState())
                ActivateBoard(true);
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

        public bool Build(byte[] board)
        {
            var incoming = Encryptor.Decrypt(board);
            for (int i = 0; i < board.Length; i++)
            {
                buttons[i].Text = incoming[i].ToString();
            }

            label1.Text = board[9] == 2 ? "Your Turn" : "Opponent's Turn";
            this.Refresh();
            return board[9] == 1;
        }

        public string Deconstruct()
        {
            var ret = new StringBuilder();
            foreach (var button in buttons) 
            {
                ret.Append(button.Text);
            }

            return ret.ToString();
        }

        private void ActivateBoard(bool toActivate)
        {
            foreach (var button in buttons)
            {
                if (button.Text == "")
                {
                    button.Enabled = toActivate;
                }
            }

            LoadGame.Enabled = toActivate;
            SaveGame.Enabled = toActivate;
        }

        public void ManageInput()
        {
            var result = sock.ReceiveData();

            if (result.isLoaded) 
            {
                var turn = PlayerChar.ToString() == buttons.LoadBoardFromData(result.data);
                if (turn)
                {
                    ActivateBoard(true);
                }
                else
                {
                    ActivateBoard(false);
                    MessageReceiver.RunWorkerAsync();
                }   
            }
            else
            {
                ReceiveMove(result.data[0]);
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void ReceiveMove(byte receive)
        {
            buttons[receive - 1].Text = OpponentChar.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] num = { 1 };
            sock.Send(num);
            button1.Text = PlayerChar.ToString();
            button1.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] num = { 2 };
            sock.Send(num);
            button2.Text = PlayerChar.ToString();
            button2.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            byte[] num = { 3 };
            sock.Send(num);
            button3.Text = PlayerChar.ToString();
            button3.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            byte[] num = { 4 };
            sock.Send(num);
            button4.Text = PlayerChar.ToString();
            button4.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            byte[] num = { 5 };
            sock.Send(num);
            button5.Text = PlayerChar.ToString();
            button5.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            byte[] num = { 6 };
            sock.Send(num);
            button6.Text = PlayerChar.ToString();
            button6.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            byte[] num = { 7 };
            sock.Send(num);
            button7.Text = PlayerChar.ToString();
            button7.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            byte[] num = { 8 };
            sock.Send(num);
            button8.Text = PlayerChar.ToString();
            button8.Enabled = false;
            MessageReceiver.RunWorkerAsync();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            byte[] num = { 9 };
            sock.Send(num);
            button9.Text = PlayerChar.ToString();
            button9.Enabled = false;
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
            Button[] buttons = {button1, button2, button3, button4, button5, button6, button7, button8, button9};
            foreach (Button b in buttons)
            {
                b.Text = "";
                b.Enabled = true;
            }

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
            sock.SendLoaded(buttons);
            //byte[] messageBytes = Encryptor.Encrypt(Deconstruct());
            //stream.Write(messageBytes, 0, messageBytes.Length);
            //Console.WriteLine($"Received from server: {messageBytes}");
        }

        private void SaveGame_Clicked(Object sender, EventArgs e)
        {
            List<ButtonInfo> buttonInfoList = new List<ButtonInfo>();
 

           // IContainer c = this.components;
            foreach (Control control in this.Controls)
            {
                if (control is Button b)
                {
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
