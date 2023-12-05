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
    }
}
