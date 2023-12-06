using System;
using System.Windows.Forms;

namespace Game
{
    public static class BoardBuilder
    {
        public static void ResetButton(this Button button, byte num, char playerChar, char opponentChar)
        {
            String text = "";
            if (num == 1)
                text = opponentChar.ToString();
            else if (num == 2)
                text = playerChar.ToString();

            button.Text = text;
        }

        public static bool UpdateButton(this Button button, string text, string playerChar, string opponentChar) 
        {
            button.Text = text;
            button.Update();

            var isPlayerChar = true;
            if (text == playerChar)
            {
                button.Enabled = false;
                isPlayerChar = true;
            }
            else if (text == opponentChar)
            {
                button.Enabled = false;
                isPlayerChar = false;
            }
            else
            {
                isPlayerChar = false;
                button.Enabled = true;
            }

            return isPlayerChar;
        }
    }
}
