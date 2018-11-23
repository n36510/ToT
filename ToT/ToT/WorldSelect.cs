using SFML.Graphics;
using System;
using System.Net.Sockets;
using System.Collections.Generic;
namespace ToT
{
    class WorldSelect
    {
        UIWindow win;
        RenderWindow window;
        public List<Sprite> worlds = new List<Sprite>();
        List<Sprite> worldsDraw;
        TcpClient c;
        public WorldSelect(RenderWindow w, TcpClient cli)
        {
            this.c = cli;
            this.window = w;
            win = new UIWindow(w, 800, 50, 0, 0, "World Select");
            w.MouseButtonReleased += W_MouseButtonReleased;
        }

        private void W_MouseButtonReleased(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            int i=0;
            foreach(Sprite w in worldsDraw)
            {
                FloatRect pos = new FloatRect(w.Position.X, w.Position.Y, w.GetGlobalBounds().Width, w.GetGlobalBounds().Height);
                if (pos.Contains(e.X, e.Y))
                {
                    byte[] p = new byte[1024];
                    Console.WriteLine("World " + i + " selected.");
                    MessageBuffer b = new MessageBuffer(p);
                    b.WriteInt16(0x0005);
                    b.WriteByte((byte)i);
                    c.Client.Send(p);
                }
                i++;
            }
        }

        public void Update()
        {
            int i = 0;
             worldsDraw = new List<Sprite>();
            foreach (Sprite w in worlds)
            {
                w.Position = new SFML.System.Vector2f(50 * i, 21);
                worldsDraw.Add(w);
                i++;
            }
        }

        public void Draw()
        {
            win.Draw();
            foreach(Sprite w in worldsDraw)
            {
                window.Draw(w);
            }
            
        }

    }
}
