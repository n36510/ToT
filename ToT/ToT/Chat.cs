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
        Font font;
        RenderWindow window;
        public List<string> chatLog = new List<string>();
        public Chat(RenderWindow w, TcpClient c)
        {
            window = w;
            cli = c;
            win = new UIWindow(w, 800, 200, 0, 400, "Chat");
            tb = new UITextBox(w, win, 0, 175, 800, false);
            
            w.KeyReleased += W_KeyReleased;
        }

        private void W_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Return)
            {
                if (tb.isFocused)
                {
                    byte[] packet = new byte[1024];
                    MessageBuffer b = new MessageBuffer();
                    b.WriteShort((short)0x0006);
                    b.WriteString(tb.text);
                    packet = b.GetContent();
                    cli.Client.Send(packet);

                    tb.text = "";
                    font = new Font("Tahoma.ttf");
                }
            }
        }
        public void Update()
        {
            tb.Update();
            
            if (chatLog.Count > 8)
            {
                chatLog.RemoveAt(0);
            }
            
            
        }
        public void Draw()
        {
            int num = 0;
            int i = 0;

            win.Draw();
            tb.Draw();

            foreach (string chat in chatLog.ToArray())
            {
                Text t = new Text(chat, font);
                t.CharacterSize = 12;
                t.Position = new SFML.System.Vector2f(win.winX, (win.winY + 20) + num);
                t.Color = Color.Black;
                window.Draw(t);
                num += 15;
                i++;
            }
        }
    }
}
