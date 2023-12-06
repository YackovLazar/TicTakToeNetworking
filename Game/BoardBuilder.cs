using System;
using System.Windows.Forms;

namespace Game
{
    public static class BoardBuilder
    {

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
