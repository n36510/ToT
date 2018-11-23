using System;
using SFML.Graphics;
using System.Collections.Generic;
using System.Net.Sockets;
namespace ToT
{
    class Chat
    {
        UIWindow win;
        UITextBox tb;
        TcpClient cli;
        List<string> chatLog = new List<string>();
        public Chat(RenderWindow w, TcpClient c)
        {
            cli = c;
            win = new UIWindow(w, 800, 100, 0, 500, "Chat");
            tb = new UITextBox(w, win, 0, 75, 800, false);
            
            w.KeyReleased += W_KeyReleased;
        }

        private void W_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Return)
            {
                if (tb.isFocused)
                {
                    byte[] packet = new byte[1024];
                    MessageBuffer b = new MessageBuffer(packet);
                    b.WriteInt16((short)0x0006);
                    b.WriteString(tb.text);
                    cli.Client.Send(packet);

                    tb.text = "";

                }
            }
        }
        public void Draw()
        {
            win.Draw();
            tb.Draw();
        }
    }
}
