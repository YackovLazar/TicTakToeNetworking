﻿using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.ComponentModel;

namespace Game
{
    public partial class Game : Form
    {
        public Game(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            if (isHost) //Meaning we are the host and the opponent is the guest on our server
            {
                PlayerChar = 'X';
                OpponentChar = 'O';
                server = new TcpListener(System.Net.IPAddress.Any, 5732);
                server.Start();
                sock = server.AcceptSocket(); //Accept the incoming connection and then asign the socket to the sock object which then can be used to recieve messages on this channel
            }
            else
            {
                PlayerChar = 'O';
                OpponentChar = 'X';
                try
                {
                    client = new TcpClient(ip, 5732); //We are the 'guest'
                    sock = client.Client;
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
            if (CheckState()) //If true, it means the game is over
                return;
            FreezeBoard(); //To allow the opponent to move
            label1.Text = "Opponent's Turn!";
            ReceiveMove();
            label1.Text = "Your Trun!";
            if (!CheckState())
                UnfreezeBoard();
        }

        private char PlayerChar;
        private char OpponentChar;
        private Socket sock;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;

        private bool CheckState()
        {
            //Horizontals
            if (button1.Text == button2.Text && button2.Text == button3.Text && button3.Text != "")
            {
                if (button1.Text[0] == PlayerChar) //Text[0] is the first char of the button
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button4.Text == button5.Text && button5.Text == button6.Text && button6.Text != "")
            {
                if (button4.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button7.Text == button8.Text && button8.Text == button9.Text && button9.Text != "")
            {
                if (button7.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            //Verticals
            else if (button1.Text == button4.Text && button4.Text == button7.Text && button7.Text != "")
            {
                if (button1.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button2.Text == button5.Text && button5.Text == button8.Text && button8.Text != "")
            {
                if (button2.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button3.Text == button6.Text && button6.Text == button9.Text && button9.Text != "")
            {
                if (button3.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button1.Text == button5.Text && button5.Text == button9.Text && button9.Text != "")
            {
                if (button1.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
                return true;
            }

            else if (button3.Text == button5.Text && button5.Text == button7.Text && button7.Text != "")
            {
                if (button3.Text[0] == PlayerChar)
                {
                    label1.Text = "You Won!";
                    MessageBox.Show("You Won!");
                }
                else
                {
                    label1.Text = "You Lost!";
                    MessageBox.Show("You Lost!");
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] num = { 1 };
            sock.Send(num);
            button1.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] num = { 2 };
            sock.Send(num);
            button2.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] num = { 3 };
            sock.Send(num);
            button3.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] num = { 4 };
            sock.Send(num);
            button4.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] num = { 5 };
            sock.Send(num);
            button5.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] num = { 6 };
            sock.Send(num);
            button6.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] num = { 7 };
            sock.Send(num);
            button7.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            byte[] num = { 8 };
            sock.Send(num);
            button8.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] num = { 9 };
            sock.Send(num);
            button9.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageReceiver.WorkerSupportsCancellation = true;
            MessageReceiver.CancelAsync();
            if (server != null) //ie we are hosting the server
                server.Stop();
        }
    }
}
